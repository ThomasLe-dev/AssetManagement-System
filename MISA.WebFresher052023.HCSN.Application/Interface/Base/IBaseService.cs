using MISA.WebFresher052023.HCSN.Application.DTO.AssetDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher052023.HCSN.Application.Interface.Base
{
    public interface IBaseService<TEntity, TEntityCreateDto, TEntityUpdateDto> : IReadOnlyService<TEntity>
    {
        /// <summary>
        /// Thêm 1 tài sản vào db
        /// </summary>
        /// <param name="entity">chi tiết của tài sản mới</param>
        /// created by: LB.Thành (16/07/2023)
        Task CreateAsync(TEntityCreateDto entityCreateDto);

        /// <summary>
        /// Chỉnh sửa 1 tài sản
        /// </summary>
        /// <param name="entity">chi tiết của tài cần sửa</param>
        /// created by: LB.Thành (16/07/2023)
        Task UpdateAsync(Guid id,TEntityUpdateDto entityUpdateDto);

        /// <summary>
        /// Xóa 1 tài sản
        /// </summary>
        /// <param name="id">Id tài sản cần xóa</param>
        /// created by: LB.Thành (16/07/2023)
        Task DeleteAsync(Guid id);

        /// <summary>
        /// Xóa nhiều tài sản
        /// </summary>
        /// <param name="id">List Ids tài sản cần xóa</param>
        /// created by: LB.Thành (16/07/2023)
        Task DeleteManyAsync(List<Guid> ids);
    }
}
