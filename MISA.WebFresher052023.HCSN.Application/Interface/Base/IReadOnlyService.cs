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
        /// Lấy tất cả tài sản từ db
        /// </summary>
        /// <returns>tất cả tài sản</returns>
        /// created by: LB.Thành (16/07/2023)
        Task<IEnumerable<TEntityDto>> GetAllAsync();

        /// <summary>
        /// Lấy về 1 tài sản theo Id
        /// </summary>
        /// <param name="id">Id của tài sản muốn lấy</param>
        /// <returns>1 tài sản</returns>
        /// created by: LB.Thành (16/07/2023)
        Task<TEntityDto> GetAsync(Guid id);
    }
}
