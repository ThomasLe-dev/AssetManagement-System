using AutoMapper;
using MISA.WebFresher052023.HCSN.Application.DTO.Receiver;
using MISA.WebFresher052023.HCSN.Application.DTO.TransferAssetDetail;
using MISA.WebFresher052023.HCSN.Application.Interface;
using MISA.WebFresher052023.HCSN.Domain.Entity;
using MISA.WebFresher052023.HCSN.Domain.Interface;
using MISA.WebFresher052023.HCSN.Domain.Model.Transfer_Asset_Detail_Model;
using MISA.WebFresher052023.HCSN.Domain.Model.Transfer_Asset_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher052023.HCSN.Application.Service
{
    public class TransferAssetDetailService : ITransferAssetDetailService
    {
        #region Fields
        private readonly IMapper _mapper;
        private readonly ITransferAssetDetailRepository _transferAssetDetailRepository;
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region Constructors
        public TransferAssetDetailService(IMapper mapper, ITransferAssetDetailRepository transferAssetDetailRepository, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _transferAssetDetailRepository = transferAssetDetailRepository;
            _unitOfWork = unitOfWork;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Trả về toàn bộ bản ghi theo chứng từ
        /// </summary>
        /// <param name="id"></param>
        /// Created by: LB.Thành (06/09/2023)
        public async Task<IEnumerable<TransferAssetDetailDto>> GetAllByTransferAsset(Guid id)
        {
            var entities = await _transferAssetDetailRepository.GetAllByTransferAsset(id);
            var result = _mapper.Map<IEnumerable<TransferAssetDetailDto>>(entities);
            return result;
        }

        /// <summary>
        /// Chèn nhiều đối tượng TransferAssetDetailDto vào cơ sở dữ liệu.
        /// </summary>
        /// <param name="dtos">Danh sách các đối tượng TransferAssetDetailDto cần chèn.</param>
        /// <created>
        /// Created by: LB.Thành (06/09/2023)
        /// </created>
        public async Task InsertManyAsync(List<TransferAssetDetailDto> dtos)
        {
            var listEntities = _mapper.Map<List<TransferAssetDetail>>(dtos);
            List<TransferAssetDetail> newList = new();
            foreach (var entity in listEntities)
            {
                entity.ModifiedDate = DateTime.Now;
                entity.TransferAssetDetailId = Guid.NewGuid();

                newList.Add(entity);
            }
            await _transferAssetDetailRepository.InsertMultiAsync(newList);
        }

        /// <summary>
        /// Cập nhật nhiều đối tượng ransferAssetDetailDto trong cơ sở dữ liệu.
        /// </summary>
        /// <param name="dtos">Danh sách các đối tượng ransferAssetDetailDto cần cập nhật.</param>
        /// <created>
        /// Created by: LB.Thành (06/09/2023)
        /// </created>
        public async Task UpdateManyAsync(List<TransferAssetDetailDto> dtos)
        {
            var listEntities = _mapper.Map<List<TransferAssetDetail>>(dtos);
            List<TransferAssetDetail> newList = new();
            foreach (var entity in listEntities)
            {
                entity.ModifiedDate = DateTime.Now;

                newList.Add(entity);
            }
            await _transferAssetDetailRepository.UpdateManyAsync(listEntities);
        }

        /// <summary>
        /// Hàm xử lý service Xóa nhiều bản ghi
        /// </summary>
        /// <param name="ids">Danh sách Id của các bản ghi cần xóa</param>
        /// <returns>Task</returns>
        /// Created by: LB.Thành (06/09/2023)
        public async Task DeleteManyAsync(List<Guid> ids)
        {
            await _unitOfWork.BeginTransactionAsync();

            try
            {
                var entities = await _transferAssetDetailRepository.GetListByIdsAsync(ids);
                await _transferAssetDetailRepository.DeleteManyAsync(entities);

                if (entities.Count < ids.Count)
                {
                    throw new Exception("Không thể xóa");
                }

                if (ids.Count == 0)
                {
                    throw new Exception("Không được truyền danh sách rỗng");
                }
                await _unitOfWork.CommitAsync();
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }
        }

        /// <summary>
        /// Lấy các chi tiết chứng từ theo Id chuwsng từ phân trang
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// Created by: LB.Thành (14/09/2023)
        public async Task<TransferAssetDetailPagingDto> GetAllByTransferAssetAsync(TransferAssetDetailFilterDto dto)
        {
            var transferAssetFilterEntity = _mapper.Map<TransferAssetDetailFilterModel>(dto);
            var transferAssetPagingEntity = await _transferAssetDetailRepository.GetAllByTransferAsset(transferAssetFilterEntity);

            var transferAssetDetailPagingDto = _mapper.Map<TransferAssetDetailPagingDto>(transferAssetPagingEntity);
            return transferAssetDetailPagingDto;
        }
        #endregion
    }
}
