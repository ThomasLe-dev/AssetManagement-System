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

        /// <summary>
        /// Lấy code mơi cho chứng từ
        /// </summary>
        /// <returns></returns>
        /// Created b: LB.Thành (10/09/2023)
        public Task<string> GetNewTransferAssetCode();
        /// <summary>
        /// Lấy chứng từ mới nhất trong những chứng từ mà tài sản có
        /// </summary>
        /// <param name="assetIds">Danh sách id của tài sản</param>
        /// <returns>Chứng từ mới nhất trong đống tài sản</returns>
        /// Created by: LB.Thành (07/09/2023)
        public Task<List<TransferAsset>> GetNewestTransferAssetByAssetId(List<Guid> assetIds);

        /// <summary>
        /// Truyền vào 1 danh sách chứng từ, tìm các tài sản trong chứng từ đó, rồi tìm tất cả các chứng từ có chứa các tài sản này
        /// </summary>
        /// <param name="transferAssetIds">Danh sách id chứng từ</param>
        /// <returns>Danh sách chứng từ của các tài sản trong các chứng từ truyền vào</returns>
        /// Created by: LB.Thành (09/09/2023)
        public Task<List<TransferAssetDeleteModel>> GetAllTransferAssetOfAsset(List<Guid> transferAssetIds);

        /// <summary>
        /// Lấy các chứng từ của các tài sản truyền đến
        /// </summary>
        /// <param name="assetIds">Danh sách id tài sản</param>
        /// <returns>Danh sách chứng từ, có chứa id tài sản</returns>
        /// Created by: LB.Thành (17/09/2023)
        public Task<List<TransferAssetDeleteModel>> GetAllTransferAssetByAssetId(List<Guid> assetIds);
    }
}
