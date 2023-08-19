using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher052023.HCSN.Domain.Interface.Base
{
    public interface IBaseRepository<TEntity> : IReadOnlyRepository<TEntity>
    {
        /// <summary>
        /// Thêm 1 tài sản vào db
        /// </summary>
        /// <param name="entity">chi tiết của tài sản mới</param>
        /// created by: LB.Thành (16/07/2023)
        Task InsertAsync(TEntity entity);

        /// <summary>
        /// Chỉnh sửa 1 tài sản
        /// </summary>
        /// <param name="entity">chi tiết của tài cần sửa</param>
        /// created by: LB.Thành (16/07/2023)
        Task UpdateAsync(Guid id ,TEntity entity);

        /// <summary>
        /// Xóa 1 tài sản
        /// </summary>
        /// <param name="entity">tài sản cần xóa</param>
        /// created by: LB.Thành (16/07/2023)
        Task DeleteAsync(TEntity entity);
        /// <summary>
        /// Xóa nhiều tài sản
        /// </summary>
        /// <param name="entities">ids những tài sản cần xóa</param>
        /// created by: LB.Thành (06/08/2023)
        Task<bool> DeleteManyAsync(List<TEntity> entities);
    }
}
