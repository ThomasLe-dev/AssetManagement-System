using MISA.WebFresher052023.HCSN.Application.Interface;
using MISA.WebFresher052023.HCSN.Application.DTO.TransferAssetDto;
using MISA.WebFresher052023.HCSN.Domain.Interface;
using AutoMapper;
using MISA.WebFresher052023.HCSN.Domain.Model.Transfer_Asset_Model;

namespace MISA.WebFresher052023.HCSN.Application.Service
{
    public class TransferAssetService : ITransferAssetService
    {
        #region Fields
        private readonly ITransferAssetRepository _transferAssetRepository;
        protected readonly IMapper _mapper;
        #endregion

        #region Constructors
        public TransferAssetService(ITransferAssetRepository transferAssetRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _transferAssetRepository = transferAssetRepository;
            _mapper = mapper;
        }
        #endregion

        #region Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<TransferAssetPagingDto> GetFilterPagingAssetAsync(TransferAssetFilterDto dto)
        {
            var transferAssetFilterEntity = _mapper.Map<TransferAssetFilterModel>(dto);

            var transferAssetPagingEntity = await _transferAssetRepository.GetPagingTransferAsset(transferAssetFilterEntity);

            var transferAssetPagingDto = _mapper.Map<TransferAssetPagingDto>(transferAssetPagingEntity);

            return transferAssetPagingDto;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entityCreateDto"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task CreateAsync(TransferAssetCreateDto entityCreateDto)
        {
            throw new NotImplementedException();
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
