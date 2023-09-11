using MISA.WebFresher052023.HCSN.Domain.Entity;
using MISA.WebFresher052023.HCSN.Domain.Interface.Base;
using MISA.WebFresher052023.HCSN.Domain.Model;
using MISA.WebFresher052023.HCSN.Domain.Model.Transfer_Asset_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher052023.HCSN.Domain.Interface
{
    public interface ITransferAssetRepository : IBaseRepository<TransferAsset>
    {
        /// <summary>
        /// Phân trang chứng từ và trả về tổng số bản ghi
        /// </summary>
        /// <param name="model"></param>
        /// <returns>danh sách chứng từ phân trang và tổng số bản ghi</returns>
        /// Created by: LB.Thành (28/08/2023)
        Task<TransferAssetPagingModel> GetPagingTransferAsset(TransferAssetFilterModel model);

        /// <summary>
        /// Tìm kiếm một chứng từ dựa trên code.
        /// </summary>
        /// <param name="code">code của tài sản cần tìm.</param>
        /// <returns>Thông tin tài sản hoặc null nếu không tìm thấy.</returns>
        /// Created by: LB.Thành (26/08/2023)
        Task<TransferAsset?> FindByCode(string code);
    }
}
