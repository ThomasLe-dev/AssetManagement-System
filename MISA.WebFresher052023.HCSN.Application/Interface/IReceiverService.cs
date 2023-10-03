using MISA.WebFresher052023.HCSN.Application.DTO.Receiver;
using MISA.WebFresher052023.HCSN.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher052023.HCSN.Application.Interface
{
    public interface IReceiverService 
    {
        Task<IEnumerable<ReceiverDto>> GetAllByTransferAsset(Guid id);
        /// <summary>
        /// Lấy toàn bộ bản ghi
        /// </summary>
        /// <returns>toàn bộ bản ghi</returns>
        /// Created by: LB.Thành (16/07/2023)
        Task<IEnumerable<ReceiverDto>> GetAllAsync();
        /// <summary>
        /// Thêm 1 chứng từ vào db
        /// </summary>
        /// <param name="entity">chi tiết của bản ghi mới</param>
        /// created by: LB.Thành (16/07/2023)
        Task InsertManyAsync(List<ReceiverDto> entityCreateDto);

        /// <summary>
        /// Chỉnh sửa nhiều người nhận
        /// </summary>
        /// <param name="entity">chi tiết của bản ghi cần sửa</param>
        /// created by: LB.Thành (16/07/2023)
        Task UpdateManyAsync(List<ReceiverDto> dtos);

        /// <summary>
        /// Xóa nhiều người nhận
        /// </summary>
        /// <param name="id">List Ids người nhận cần xóa</param>
        /// created by: LB.Thành (16/07/2023)
        Task DeleteManyAsync(List<Guid> ids);

        /// <summary>
        /// Lấy phòng ban giao nhận mới nhất
        /// </summary>
        /// <returns></returns>
        /// Created by: LB.Thành (14/09/2023)
        public Task<List<Receiver>> GetNewestReceiverAsync();
    }
}
