using MISA.WebFresher052023.HCSN.Application.Interface;
using MISA.WebFresher052023.HCSN.Application.DTO.TransferAssetDto;
using MISA.WebFresher052023.HCSN.Domain.Interface;
using AutoMapper;
using MISA.WebFresher052023.HCSN.Domain.Model.Transfer_Asset_Model;
using MISA.WebFresher052023.HCSN.Domain.Entity;
using System.Data;
using MISA.WebFresher052023.HCSN.Domain.Resource;
using MISA.WebFresher052023.HCSN.Domain.Enum;
using MISA.WebFresher052023.HCSN.Application.DTO.TransferAssetDetail;
using MISA.WebFresher052023.HCSN.Application.DTO.Receiver;
using MISA.WebFresher052023.HCSN.Domain;

namespace MISA.WebFresher052023.HCSN.Application.Service
{
    public class TransferAssetService : ITransferAssetService
    {
        #region Fields
        private readonly ITransferAssetRepository _transferAssetRepository;
        private readonly IMapper _mapper;
        private readonly IEntityManager _entityManager;
        private readonly ITransferAssetDetailManager _transferAssetDetailManager;
        private readonly ITransferAssetManager _transferAssetManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IReceiverRepository _receiverRepository;
        private readonly ITransferAssetDetailRepository _transferAssetDetailRepository;
        private readonly IFixedAssetRepository _fixedAssetRepository;
        #endregion

        #region Constructors
        public TransferAssetService(ITransferAssetRepository transferAssetRepository, IMapper mapper,
                                    IEntityManager entityManager, ITransferAssetManager transferAssetManager,
                                    IUnitOfWork unitOfWork, IReceiverRepository receiverRepository,
                                    ITransferAssetDetailRepository transferAssetDetailRepository, 
                                    ITransferAssetDetailManager transferAssetDetailManager, 
                                    IFixedAssetRepository fixedAssetRepository)
        {
            _transferAssetRepository = transferAssetRepository;
            _mapper = mapper;
            _entityManager = entityManager;
            _transferAssetManager = transferAssetManager;
            _unitOfWork = unitOfWork;
            _receiverRepository = receiverRepository;
            _transferAssetDetailRepository = transferAssetDetailRepository;
            _transferAssetDetailManager = transferAssetDetailManager;
            _fixedAssetRepository = fixedAssetRepository;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Xử lý ánh xạ 
        /// </summary>
        /// <param name="transferAssetCreateDto"></param>
        /// Created by: LB.Thành (26/08/2023)
        public async Task<TransferAsset> MapDtoToEntity(TransferAsset transferAssetEntity)
        {
            await _entityManager.CheckExistedEntityByCode(transferAssetEntity.TransferAssetCode);

            var transferAsset = _mapper.Map<TransferAsset>(transferAssetEntity);

            return transferAsset;
        }

        /// <summary>
        /// Lấy TransferAssetCode mới
        /// </summary>
        /// <returns>FixedAssetCode mới</returns>
        /// Created by: LB.Thành (10/09/2023)
        public async Task<string> GetNewTransferAssetCodeAsync()
        {
            var transferAssetCode = await _transferAssetRepository.GetNewTransferAssetCode();
            return transferAssetCode;
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


            // Insert 
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                //Thêm chứng từ
                var newTransferAsset = await MapDtoToEntity(transferAssetCreateDto.TransferAsset);
                newTransferAsset.TransferAssetId = Guid.NewGuid();
                await _transferAssetRepository.InsertAsync(newTransferAsset);
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
        /// Cập nhật thông tin về tài sản chuyển đổi và chi tiết của nó.
        /// </summary>
        /// <param name="transferAssetId">ID của tài sản chuyển đổi cần cập nhật.</param>
        /// <param name="transferAssetUpdateDto">Đối tượng chứa thông tin cần cập nhật.</param>
        /// <returns>Task hoàn thành việc cập nhật.</returns>
        /// Created by: LB.Thành (14/09/2023)
        public async Task UpdateAsync(Guid transferAssetId, TransferAssetUpdateDto transferAssetUpdateDto)
        {
            // Lấy thông tin tài sản chuyển đổi cũ từ cơ sở dữ liệu
            var oldTransferAsset = await _transferAssetRepository.GetAsync(transferAssetId);
            if (oldTransferAsset == null)
            {
                throw new InvalidDataException(ErrorMessages.InvalidData);
            }

            // Kiểm tra thông tin đầu vào
            if (transferAssetUpdateDto == null) throw new DataException(ErrorMessages.InvalidData);
            _transferAssetManager.CheckNullRequest(transferAssetUpdateDto.TransferAsset, transferAssetUpdateDto.ListTransferAssetDetail);

            // Chuyển đổi dữ liệu DTO thành các đối tượng thực thể
            var transferAssetDto = _mapper.Map<TransferAsset>(transferAssetUpdateDto.TransferAsset);
            var listTransferAssetDeatailDtos = transferAssetUpdateDto.ListTransferAssetDetail.ToList();
            var listReceiverDtos = transferAssetUpdateDto.ListReceiver.ToList();

            // Kiểm tra danh sách chi tiết chứng từ có tồn tại trong cơ sở dữ liệu
            var listTransferAssetDetails = _mapper.Map<List<TransferAssetDetail>>(listTransferAssetDeatailDtos);

            var listAssetIds = listTransferAssetDetails.Select(transfer => transfer.FixedAssetId).Distinct().ToList();
            await _transferAssetManager.CheckDateAsync(transferAssetDto, listAssetIds, ActionMode.UPDATE);

            // Kiểm tra xem danh sách chi tiết chứng từ có tồn tại trong db để tiến hành thêm sauwr xóa
            var listTransferAssetDetailIds = listTransferAssetDeatailDtos
                .Where(transfer => transfer.Flag == ActionMode.UPDATE || transfer.Flag == ActionMode.DELETE)
                .Select(transfer => transfer.TransferAssetDetailId)
                .Distinct()
                .ToList();
            var listTransferAssetDetailInDB = await _transferAssetDetailRepository.GetListByIdsAsync(listTransferAssetDetailIds);
            if (listTransferAssetDetailInDB.Count != listTransferAssetDetailIds.Count)
            {
                throw new InvalidDataException(ErrorMessages.InvalidData);
            }

            // Kiểm tra các chi tiết chứng từ này có chứng từ phát sinh trước đó không 
            await CheckIfCanUpdateOrDelete(ActionMode.DELETE, listTransferAssetDeatailDtos, (Guid)oldTransferAsset.TransferAssetId);
            await CheckIfCanUpdateOrDelete(ActionMode.UPDATE, listTransferAssetDeatailDtos, (Guid)oldTransferAsset.TransferAssetId);

            // Danh sách chi tiết chứng từ chia theo Flag
            var transferDtosCreate = listTransferAssetDeatailDtos.Where(transfer => transfer.Flag == ActionMode.CREATE).ToList();
            var transferDtosUpdate = listTransferAssetDeatailDtos.Where(transfer => transfer.Flag == ActionMode.UPDATE).ToList();
            var transferDtosDelete = listTransferAssetDeatailDtos.Where(transfer => transfer.Flag == ActionMode.DELETE).ToList();

            // Danh sách chi tiết người nhận chia theo Flag
            List<ReceiverUpdateDto> receiverDtosCreate = new();
            List<ReceiverUpdateDto> receiverDtosUpdate = new();
            List<ReceiverUpdateDto> receiverDtosDelete = new();
            if (listReceiverDtos != null)
            {
                receiverDtosCreate = listReceiverDtos.Where(receiver => receiver.Flag == ActionMode.CREATE).ToList();
                receiverDtosUpdate = listReceiverDtos.Where(receiver => receiver.Flag == ActionMode.UPDATE).ToList();
                receiverDtosDelete = listReceiverDtos.Where(receiver => receiver.Flag == ActionMode.DELETE).ToList();
            }

            // Mở transaction
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                // Cập nhật chứng từ
                DateTime? createdDate = oldTransferAsset.CreatedDate;
                var transferAsset = _mapper.Map(transferAssetDto, oldTransferAsset);
                transferAsset.SetKey(transferAssetId);
                transferAsset.ModifiedDate = DateTime.Now;
                transferAsset.CreatedDate = createdDate;

                await _transferAssetRepository.UpdateAsync((Guid)transferAsset.TransferAssetId,transferAsset);

                if (transferDtosCreate != null)
                {
                    List<TransferAssetDetail> transferAssetDetailsRaw = _mapper.Map<List<TransferAssetDetail>>(transferDtosCreate);
                    List<TransferAssetDetail> transferAssetDetails = InitializeEntities(transferAssetDetailsRaw, transferAsset);

                    await _transferAssetDetailRepository.InsertMultiAsync(transferAssetDetails);
                }

                if (receiverDtosCreate != null)
                {
                    List<Receiver> receiversRaw = _mapper.Map<List<Receiver>>(receiverDtosCreate);
                    List<Receiver> receivers = InitializeEntities(receiversRaw, transferAsset);

                    await _receiverRepository.InsertMultiAsync(receivers);
                }

                if (transferDtosUpdate != null)
                {
                    transferDtosUpdate = transferDtosUpdate.Select(item =>
                    {
                        item.TransferAssetId = (Guid)transferAsset.TransferAssetId;
                        return item;
                    }).ToList();

                    List<TransferAssetDetail> transferAssetDetailsRaw = _mapper.Map<List<TransferAssetDetail>>(transferDtosUpdate);
                    List<TransferAssetDetail> transferAssetDetails = InitializeEntities(transferAssetDetailsRaw, createdDate);

                    await _transferAssetDetailRepository.UpdateManyAsync(transferAssetDetails);
                }

                if (receiverDtosUpdate != null)
                {
                    receiverDtosUpdate = receiverDtosUpdate.Select(item =>
                    {
                        item.TransferAssetId = (Guid)transferAsset.TransferAssetId;
                        return item;
                    }).ToList();

                    List<Receiver> receiversRaw = _mapper.Map<List<Receiver>>(receiverDtosUpdate);
                    List<Receiver> receivers = InitializeEntities(receiversRaw, createdDate);

                    await _receiverRepository.UpdateManyAsync(receivers);
                }

                if (transferDtosDelete != null)
                {
                    List<TransferAssetDetail> transferAssetDetails = _mapper.Map<List<TransferAssetDetail>>(transferDtosDelete);

                    await _transferAssetDetailRepository.DeleteManyAsync(transferAssetDetails);
                }

                if (receiverDtosDelete != null)
                {
                    List<Receiver> receivers = _mapper.Map<List<Receiver>>(receiverDtosDelete);

                    await _receiverRepository.DeleteManyAsync(receivers);
                }

                await _unitOfWork.CommitAsync();
            }
            catch
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }
        }
        /// <summary>
        /// Thêm ngày tạo, ngày sửa, id cho danh sách bản ghi truyền vào
        /// </summary>
        /// <param name="entitiesRaw">Danh sách bản ghi</param>
        /// <param name="transferAsset">Chứng từ tài sản</param>
        /// <returns>Danh sách sau khi sửa 1 vài thuộc tính</returns>
        /// Created by: LB.Thành (10/09/2023)
        private static List<T> InitializeEntities<T>(List<T> entitiesRaw, TransferAsset? transferAsset) where T : BaseAudit, IHasKey
        {
            List<T> entities = new();

            foreach (var entity in entitiesRaw)
            {
                entity.CreatedDate = DateTime.Now;
                entity.ModifiedDate = DateTime.Now;
                entity.SetKey(Guid.NewGuid());

                if (transferAsset != null)
                {
                    if (entity is Receiver receiver)
                    {
                        receiver.TransferAssetId = (Guid)transferAsset.TransferAssetId;
                    }
                    else if (entity is TransferAssetDetail transferAssetDetail)
                    {
                        transferAssetDetail.TransferAssetId = (Guid)transferAsset.TransferAssetId;
                    }
                }
                entities.Add(entity);
            }

            return entities;
        }

        /// <summary>
        /// Thêm ngày sửa cho danh sách bản ghi truyền vào (trường hợp update)
        /// </summary>
        /// <param name="entitiesRaw">Danh sách bản ghi</param>
        /// <returns>Danh sách sau khi sửa 1 vài thuộc tính</returns>
        /// Created by: ldtuan (31/08/2023)
        private static List<T> InitializeEntities<T>(List<T> entitiesRaw, DateTime? createdDate) where T : BaseAudit
        {
            List<T> entities = new();

            foreach (var entity in entitiesRaw)
            {
                entity.ModifiedDate = DateTime.Now;
                entity.CreatedDate = createdDate;

                entities.Add(entity);
            }

            return entities;
        }
        /// <summary>
        /// Kiểm tra các chi tiết chứng từ này có chứng từ phát sinh trước đó không
        /// </summary>
        /// <param name="actionMode">Xét theo hành động để lấy bản ghi tương ứng</param>
        /// <param name="list">Danh sách bản ghi cần lọc</param>
        /// <param name="transferAssetId">Id của chứng từ</param>
        /// <returns>Ném ra exception nếu có lỗi</returns>
        private async Task CheckIfCanUpdateOrDelete(ActionMode actionMode, List<TransferAssetDetailUpdateDto> list, Guid transferAssetId)
        {
            List<Guid> ids = new();
            if (actionMode == ActionMode.UPDATE)
            {
                ids = list
                .Where(transfer => transfer.Flag == actionMode || transfer.Flag == ActionMode.UNCHANGE)
                .Select(transfer => transfer.TransferAssetDetailId)
                .Distinct()
                .ToList();
            }
            else
            {
                ids = list
                .Where(transfer => transfer.Flag == actionMode)
                .Select(transfer => transfer.TransferAssetDetailId)
                .Distinct()
                .ToList();
            }
            if (ids != null && ids.Count > 0)
            {
                await _transferAssetDetailManager.CheckExistTransferBefore(ids, transferAssetId);
            }
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
        /// Xóa nhiều chứng từ và liên quan của chúng từ DB.
        /// </summary>
        /// <param name="ids">Danh sách các ID của chứng từ cần xóa</param>
        /// <returns>Task</returns>
        /// Created by: LB.Thành (14/09/2023)
        public async Task DeleteManyAsync(List<Guid> ids)
        {
            // Check chứng từ có tồn tại trong db không, nếu tồn tại thì sắp xếp theo ngày tạo với thứ tự giảm dần
            var transferAssets = await _transferAssetRepository.GetListByIdsAsync(ids);
            if (transferAssets == null || !transferAssets.Any() || transferAssets.Count != ids.Count)
            {
                throw new InvalidDataException(ErrorMessages.InvalidData);
            }
            transferAssets = transferAssets.OrderByDescending(transfer => transfer.CreatedDate).ToList();


            // Từ các chứng từ trên, lấy được danh sách tài sản bên trong, từ đó tìm được toàn bộ chứng từ của các tài sản đó
            var allTransferAssets = await _transferAssetRepository.GetAllTransferAssetOfAsset(ids);

            // Tạo các list receiver và detail để xóa
            List<TransferAssetDetail> listDetail = new();
            List<Receiver> listReceiver = new();

            // kiểm tra xem chứng từ nhận được từ FE có phải là các chứng từ mới nhất trong DB không
            for (int i = 0; i < transferAssets.Count; i++)
            {
                // Danh sách tài sản trong từng chứng từ 1 của danh sách truyền từ FE về
                var detailFE = allTransferAssets
                    .Where(transfer => transfer.TransferAssetId == transferAssets[i].TransferAssetId)
                    .OrderByDescending(transfer => transfer.CreatedDate)
                    .ToList();

                // So sánh created date của từng tài sản trong FE với trong DB
                for (int j = 0; j < detailFE.Count; j++)
                {
                    var DB = allTransferAssets
                        .Where(transfer => transfer.FixedAssetId == detailFE[j].FixedAssetId)
                        .OrderByDescending(transfer => transfer.CreatedDate)
                        .FirstOrDefault();
                    if (DB != null && detailFE[j].CreatedDate != DB.CreatedDate)
                    {
                        // 3.2.1.Lấy mã code của tài sản để hiển thị lên thông báo lỗi
                        var fixedAsset = await _fixedAssetRepository.GetAsync(detailFE[j].FixedAssetId);

                        // 3.2.2.Lấy các chứng từ phát sinh ra
                        var generatedDocument = allTransferAssets
                            .Where(transfer => transfer.FixedAssetId == detailFE[j].FixedAssetId && transfer.CreatedDate > detailFE[j].CreatedDate)
                            .OrderByDescending(transfer => transfer.CreatedDate)
                            .ToList();

                        throw new ValidateException(TransferAssetErrorMessages.Arise(fixedAsset.FixedAssetCode), TransferAssetErrorMessages.Infor(generatedDocument));
                    }
                }
                // Xóa các mục của FE ra khỏi allTransferAssets dựa trên transferAssetId
                allTransferAssets.RemoveAll(transfer => transfer.TransferAssetId == transferAssets[i].TransferAssetId);

                // Lấy danh sách người nhận để xóa
                List<Guid> transferAssetIds = new()
                {
                    transferAssets[i].TransferAssetId.Value
                };
                var receivers = await _receiverRepository.GetListReceiverByTransferAsset(transferAssetIds);
                listReceiver.AddRange(receivers);

                // Lấy danh sách chi tiết chứng từ để xóa
                List<Guid> assetIds = detailFE.Select(detail => detail.FixedAssetId).ToList();
                var transferDetailsModel = await _transferAssetDetailRepository.GetListDetailByIdsAsync(assetIds);
                List<Guid> detailIds = transferDetailsModel
                    .Where(transfer => transfer.TransferAssetId == transferAssets[i].TransferAssetId)
                    .Select(transfer => transfer.TransferAssetDetailId)
                    .ToList();
                var transferDetails = await _transferAssetDetailRepository.GetListByIdsAsync(detailIds);
                listDetail.AddRange(transferDetails);
            }

            // Bắt đầu xóa
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                await _receiverRepository.DeleteManyAsync(listReceiver);

                await _transferAssetDetailRepository.DeleteManyAsync(listDetail);

                await _transferAssetRepository.DeleteManyAsync(transferAssets);

                await _unitOfWork.CommitAsync();
            }
            catch
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }
        }
        #endregion
    }
}
