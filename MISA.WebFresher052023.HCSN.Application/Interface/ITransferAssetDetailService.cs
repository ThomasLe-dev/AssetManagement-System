using MISA.WebFresher052023.HCSN.Application.DTO.Receiver;
using MISA.WebFresher052023.HCSN.Application.DTO.TransferAssetDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher052023.HCSN.Application.Interface
{
    public interface ITransferAssetDetailService
    {
        /// <summary>
        /// Lấy toàn bộ bản ghi theo chứng từ
        /// </summary>
        /// <returns>toàn bộ bản ghi</returns>
        /// Created by: LB.Thành (16/07/2023)
        Task<IEnumerable<TransferAssetDetailDto>> GetAllByTransferAsset(Guid id);
        /// <summary>
        /// Thêm 1 chứng từ vào db
        /// </summary>
        /// <param name="entity">chi tiết của bản ghi mới</param>
        /// created by: LB.Thành (16/07/2023)
        Task InsertManyAsync(List<TransferAssetDetailDto> dtos);

        /// <summary>
        /// Chỉnh sửa nhiều chi tiết chứng từ
        /// </summary>
        /// <param name="entity">chi tiết của bản ghi cần sửa</param>
        /// created by: LB.Thành (16/07/2023)
        Task UpdateManyAsync(List<TransferAssetDetailDto> dtos);

        /// <summary>
        /// Xóa nhiều chi tiết chứng từ
        /// </summary>
        /// <param name="id">List Ids chi tiết chứng từ cần xóa</param>
        /// created by: LB.Thành (16/07/2023)
        Task DeleteManyAsync(List<Guid> ids);
    }
}
