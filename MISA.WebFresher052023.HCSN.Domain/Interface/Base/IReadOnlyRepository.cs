using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher052023.HCSN.Domain.Interface.Base
{
    public interface IReadOnlyRepository<TEntity>
    {
        /// <summary>
        /// Lấy tất cả bản ghi từ db
        /// </summary>
        /// <returns>tất cả bản ghi</returns>
        /// created by: LB.Thành (16/07/2023)
        Task<IEnumerable<TEntity>> GetAllAsync();

        /// <summary>
        /// Lấy về 1 bản ghi theo Id
        /// </summary>
        /// <param name="id">Id của bản ghi muốn lấy</param>
        /// <returns>1 bản ghi</returns>
        /// created by: LB.Thành (16/07/2023)
        Task<TEntity> GetAsync(Guid id);

        /// <summary>
        /// Tìm kiếm 1 bản ghi theo Id 
        /// </summary>
        /// <param name="id">Id của bản ghi muốn tìm kiếm</param>
        /// <returns>Bản ghi muốn tìm kiếm (có thể null)</returns>
        /// created by: LB.Thành (16/07/2023)
        Task<TEntity?> FindAsync(Guid id);

        /// <summary>
        /// Lấy 1 danh sách bản ghi theo id
        /// </summary>
        /// <param name="ids">id của những bản ghi cần lấy</param>
        /// <returns>danh sách các bản ghi</returns>
        /// Created by: LB.Thành (06/08/2023)
        Task<List<TEntity>> GetListByIdsAsync(List<Guid> ids);
    }
}
