using MISA.WebFresher052023.HCSN.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher052023.HCSN.Application.Interface.Base
{
    public interface IReadOnlyService<TEntityDto>
    {
        /// <summary>
        /// Lấy tất cả bản ghi từ db
        /// </summary>
        /// <returns>tất cả bản ghi</returns>
        /// created by: LB.Thành (16/07/2023)
        Task<IEnumerable<TEntityDto>> GetAllAsync();

        /// <summary>
        /// Lấy về 1 bản ghi theo Id
        /// </summary>
        /// <param name="id">Id của bản ghi muốn lấy</param>
        /// <returns>1 bản ghi</returns>
        /// created by: LB.Thành (16/07/2023)
        Task<TEntityDto> GetAsync(Guid id);
    }
}
