using AutoMapper;
using MISA.WebFresher052023.HCSN.Application.Interface.Base;
using MISA.WebFresher052023.HCSN.Application.Service.Base;
using MISA.WebFresher052023.HCSN.Domain.Interface;
using MISA.WebFresher052023.HCSN.Domain.Interface.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher052023.HCSN.Application
{
    public abstract class BaseService<TEntity, TEntityDto, TEntityCreateDto, TEntityUpdateDto> : BaseReadOnlyService<TEntity, TEntityDto>,
        IBaseService<TEntityDto, TEntityCreateDto, TEntityUpdateDto> where TEntity : class
    {
        #region Fields
        protected readonly IBaseRepository<TEntity> _baseRepository;
        protected readonly IUnitOfWork _unitOfWork;
        #endregion

        #region Constructor
        protected BaseService(IBaseRepository<TEntity> baseRepository, IMapper mapper, IUnitOfWork unitOfWork) : base(baseRepository, mapper)
        {
            _baseRepository = baseRepository;
            _unitOfWork = unitOfWork;
        }
        #endregion

        #region Methods

        /// <summary>
        /// Hàm xử lý service Thêm 1 bản ghi
        /// </summary>
        /// <param name="entityCreateDto">DTO chứa thông tin cần thêm mới</param>
        /// <returns>Task</returns>
        /// Created by: LB.Thành (19/07/2023)
        public virtual async Task CreateAsync(TEntityCreateDto entityCreateDto)
        {
            // Validate nghiệp vụ trước khi tạo mới đối tượng TEntity
            var entity = await MapCreateDtoToEntity(entityCreateDto);

            // Thực hiện thêm mới đối tượng TEntity vào cơ sở dữ liệu
            await _baseRepository.InsertAsync(entity);
        }

        /// <summary>
        /// Hàm xử lý service Cập nhật 1 bản ghi
        /// </summary>
        /// <param name="id">Id của bản ghi cần cập nhật</param>
        /// <param name="entityUpdateDto">DTO chứa thông tin cần cập nhật</param>
        /// <returns>Task</returns>
        /// Created by: LB.Thành (19/07/2023)
        public virtual async Task UpdateAsync(Guid id, TEntityUpdateDto entityUpdateDto)
        {
            // Validate nghiệp vụ trước khi cập nhật đối tượng TEntity
            var entity = await MapUpdateDtoToEntity(id, entityUpdateDto);

            // Thực hiện cập nhật đối tượng TEntity vào cơ sở dữ liệu
            await _baseRepository.UpdateAsync(id, entity);
        }

        /// <summary>
        /// Hàm xử lý service Xóa 1 bản ghi
        /// </summary>
        /// <param name="id">Id của bản ghi cần xóa</param>
        /// <returns>Task</returns>
        /// Created by: LB.Thành (19/07/2023)
        public async Task DeleteAsync(Guid id)
        {
            // Lấy entity cần xóa dựa vào Id
            var entity = await _baseRepository.GetAsync(id);

            // Thực hiện xóa đối tượng TEntity từ cơ sở dữ liệu
            await _baseRepository.DeleteAsync(entity);
        }

        /// <summary>
        /// Hàm xử lý service Xóa nhiều bản ghi
        /// </summary>
        /// <param name="ids">Danh sách Id của các bản ghi cần xóa</param>
        /// <returns>Task</returns>
        /// Created by: LB.Thành (19/07/2023)
        public virtual async Task DeleteManyAsync(List<Guid> ids)
        {
            await _unitOfWork.BeginTransactionAsync();

            try
            {
                var entities = await _baseRepository.GetListByIdsAsync(ids);
                await _baseRepository.DeleteManyAsync(entities);

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


        #endregion
        #region Mapper
        public abstract Task<TEntity> MapCreateDtoToEntity(TEntityCreateDto entityCreateDto);
        public abstract Task<TEntity> MapUpdateDtoToEntity(Guid id, TEntityUpdateDto entityUpdateDto);
        #endregion
    }
}
