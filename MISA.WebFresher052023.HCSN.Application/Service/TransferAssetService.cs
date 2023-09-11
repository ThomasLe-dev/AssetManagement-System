using MISA.WebFresher052023.HCSN.Application.Interface;
using MISA.WebFresher052023.HCSN.Application.DTO.TransferAssetDto;
using MISA.WebFresher052023.HCSN.Domain.Interface;
using AutoMapper;
using MISA.WebFresher052023.HCSN.Domain.Model.Transfer_Asset_Model;
using MISA.WebFresher052023.HCSN.Domain.Entity;

namespace MISA.WebFresher052023.HCSN.Application.Service
{
    public class TransferAssetService : ITransferAssetService
    {
        #region Fields
        private readonly ITransferAssetRepository _transferAssetRepository;
        private readonly IMapper _mapper;
        private readonly IEntityManager _entityManager;
        private readonly ITransferAssetManager _transferAssetManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IReceiverRepository _receiverRepository;
        private readonly ITransferAssetDetailRepository _transferAssetDetailRepository;
        #endregion

        #region Constructors
        public TransferAssetService(ITransferAssetRepository transferAssetRepository, IMapper mapper,
                                    IEntityManager entityManager, ITransferAssetManager transferAssetManager,
                                    IUnitOfWork unitOfWork, IReceiverRepository receiverRepository,
                                    ITransferAssetDetailRepository transferAssetDetailRepository)
        {
            _transferAssetRepository = transferAssetRepository;
            _mapper = mapper;
            _entityManager = entityManager;
            _transferAssetManager = transferAssetManager;
            _unitOfWork = unitOfWork;
            _receiverRepository = receiverRepository;
            _transferAssetDetailRepository = transferAssetDetailRepository;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Xử lý ánh xạ 
        /// </summary>
        /// <param name="transferAssetCreateDto"></param>
        /// Created by: LB.Thành (26/08/2023)
        public async Task<TransferAsset> MapCreateDtoToEntity(TransferAsset transferAssetEntity)
        {
            await _entityManager.CheckExistedEntityByCode(transferAssetEntity.TransferAssetCode);

            var transferAsset = _mapper.Map<TransferAsset>(transferAssetEntity);

            return transferAsset;
        }

        /// <summary>
        /// Lấy danh sách các tài sản chuyển đổi phân trang dựa trên các tiêu chí tìm kiếm.
        /// </summary>
        /// <param name="dto">Đối tượng chứa các tiêu chí tìm kiếm tài sản chuyển đổi.</param>
        /// <returns>Đối tượng TransferAssetPagingDto chứa danh sách tài sản chuyển đổi phân trang.</returns>
        /// <remarks>
        /// Phương thức này thực hiện các bước sau:
        /// 1. Sử dụng AutoMapper để chuyển đổi đối tượng TransferAssetFilterDto thành đối tượng TransferAssetFilterModel.
        /// 2. Gọi phương thức GetPagingTransferAsset từ _transferAssetRepository để lấy danh sách tài sản chuyển đổi phân trang.
        /// 3. Sử dụng AutoMapper để chuyển đổi kết quả từ TransferAssetPagingEntity thành TransferAssetPagingDto.
        /// 4. Trả về đối tượng TransferAssetPagingDto chứa danh sách tài sản chuyển đổi phân trang.
        /// </remarks>
        /// <created>
        /// Created by: LB.Thành (26/08/2023)
        /// </created>
        public async Task<TransferAssetPagingDto> GetFilterPagingAssetAsync(TransferAssetFilterDto dto)
        {
            var transferAssetFilterEntity = _mapper.Map<TransferAssetFilterModel>(dto);

            var transferAssetPagingEntity = await _transferAssetRepository.GetPagingTransferAsset(transferAssetFilterEntity);

            var transferAssetPagingDto = _mapper.Map<TransferAssetPagingDto>(transferAssetPagingEntity);

            return transferAssetPagingDto;
        }

        /// <summary>
        /// Thêm 1 chứng từ mới
        /// </summary>
        /// <param name="transferAssetCreateDto"></param>
        /// Created by: LB.Thành (26/08/2023)
        public async Task<bool> CreateAsync(TransferAssetCreateDto transferAssetCreateDto)
        {
            // Validate data
            _transferAssetManager.CheckNullRequest(transferAssetCreateDto.TransferAsset, transferAssetCreateDto.ListTransferAssetDetail);
            _transferAssetManager.CheckTransferDate(transferAssetCreateDto.TransferAsset);

            //Thêm chứng từ
            var newTransferAsset = await MapCreateDtoToEntity(transferAssetCreateDto.TransferAsset);
            newTransferAsset.TransferAssetId = Guid.NewGuid();
            await _transferAssetRepository.InsertAsync(newTransferAsset);
            // Insert 
            await _unitOfWork.BeginTransactionAsync();
            try
            {

                // Thêm ban giao nhận
                var listReceiverDtos = transferAssetCreateDto.ListReceiver;
                if (listReceiverDtos != null)
                {
                    List<Receiver> listReceivers = _mapper.Map<List<Receiver>>(listReceiverDtos);
                    List<Receiver> receivers = new();
                    foreach (var receiver in listReceivers)
                    {
                        receiver.ModifiedDate = DateTime.Now;
                        receiver.ReceiverId = Guid.NewGuid();
                        receiver.TransferAssetId = (Guid)newTransferAsset.TransferAssetId;
                        receivers.Add(receiver);
                    }
                    await _receiverRepository.InsertMultiAsync(receivers);
                }

                // Thêm chi tiết chứng từ
                var listTransferAssetDetails = transferAssetCreateDto.ListTransferAssetDetail;
                if (listTransferAssetDetails != null)
                {
                    var listDetails = _mapper.Map<List<TransferAssetDetail>>(listTransferAssetDetails);
                    List<TransferAssetDetail> details = new();
                    foreach (var detail in listDetails)
                    {
                        detail.ModifiedDate = DateTime.Now;
                        detail.TransferAssetId = (Guid)newTransferAsset.TransferAssetId;
                        detail.TransferAssetDetailId = Guid.NewGuid();
                        details.Add(detail);
                    }
                    await _transferAssetDetailRepository.InsertMultiAsync(details);
                }
                await _unitOfWork.CommitAsync();
                return true;
            }
            catch
            {
                await _unitOfWork.RollbackAsync();
                return false;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="entityUpdateDto"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task UpdateAsync(Guid id, TransferAssetUpdateDto entityUpdateDto)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task DeleteManyAsync(List<Guid> ids)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
