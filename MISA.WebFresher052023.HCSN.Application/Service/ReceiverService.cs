using AutoMapper;
using MISA.WebFresher052023.HCSN.Application.DTO.Receiver;
using MISA.WebFresher052023.HCSN.Application.Interface;
using MISA.WebFresher052023.HCSN.Domain.Entity;
using MISA.WebFresher052023.HCSN.Domain.Interface;
using MISA.WebFresher052023.HCSN.Domain.Interface.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher052023.HCSN.Application.Service
{
    public class ReceiverService : IReceiverService
    {
        #region Fields
        private readonly IMapper _mapper; 
        private readonly IReceiverRepository _receiverRepository;
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region Constructors
        public ReceiverService(IMapper mapper, IReceiverRepository receiverRepository, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _receiverRepository = receiverRepository;
            _unitOfWork = unitOfWork;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Lấy toàn bộ bản ghi
        /// </summary>
        /// <returns>toàn bộ bản ghi</returns>
        /// Created by: LB.Thành (06/09/2023)
        public async Task<IEnumerable<ReceiverDto>> GetAllAsync()
        {
            var entities = await _receiverRepository.GetAllAsync();

            var result = _mapper.Map<IEnumerable<ReceiverDto>>(entities);
            return result;
        }

        /// <summary>
        /// Chèn nhiều đối tượng ReceiverDto vào cơ sở dữ liệu.
        /// </summary>
        /// <param name="dtos">Danh sách các đối tượng ReceiverDto cần chèn.</param>
        /// <created>
        /// Created by: LB.Thành (06/09/2023)
        /// </created>
        public async Task InsertManyAsync(List<ReceiverDto> dtos)
        {
            var listEntities = _mapper.Map<List<Receiver>>(dtos);
            List<Receiver> newList = new();
            foreach (var entity in listEntities)
            {
                entity.ModifiedDate = DateTime.Now;
                entity.ReceiverId = Guid.NewGuid();

                newList.Add(entity);
            }
            await _receiverRepository.InsertMultiAsync(newList);
        }

        /// <summary>
        /// Cập nhật nhiều đối tượng ReceiverDto trong cơ sở dữ liệu.
        /// </summary>
        /// <param name="dtos">Danh sách các đối tượng ReceiverDto cần cập nhật.</param>
        /// <created>
        /// Created by: LB.Thành (06/09/2023)
        /// </created>
        public async Task UpdateManyAsync(List<ReceiverDto> dtos)
        {
            var listEntities = _mapper.Map<List<Receiver>>(dtos);
            List<Receiver> newList = new();
            foreach (var entity in listEntities)
            {
                entity.ModifiedDate = DateTime.Now;

                newList.Add(entity);
            }
            await _receiverRepository.UpdateManyAsync(listEntities);
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
                var entities = await _receiverRepository.GetListByIdsAsync(ids);
                await _receiverRepository.DeleteManyAsync(entities);

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

        public async Task<IEnumerable<ReceiverDto>> GetAllByTransferAsset(Guid id)
        {
            var entities = await _receiverRepository.GetAllByTransferAsset(id);
            var result = _mapper.Map<IEnumerable<ReceiverDto>>(entities);
            return result;
        }

        #endregion
    }
}
