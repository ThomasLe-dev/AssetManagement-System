using MISA.WebFresher052023.HCSN.Domain.Interface.Base;
using MISA.WebFresher052023.HCSN.Domain.Model;
using MISA.WebFresher052023.HCSN.Domain.Model.Fixed_Asset_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher052023.HCSN.Domain.Interface
{
    public interface IFixedAssetRepository : IBaseRepository<FixedAsset>
    {
        /// <summary>
        /// Tìm kiếm 1 tài sản theo code
        /// </summary>
        /// <param name="id">code của tài sản muốn tìm kiếm</param>
        /// <returns>Tài sản muốn tìm kiếm (có thể null)</returns>
        /// created by: LB.Thành (16/07/2023)
        Task<FixedAsset?> FindByCodeAsync(string code);
        /// <summary>
        /// Lấy ra danh sách tài sản theo điều kiện lọc, phân trang và trả về tổng số bản ghi
        /// </summary>
        /// <param name="model"></param>
        /// <returns>danh sách tài sản hợp lệ và tổng số bản ghi</returns>
        /// Created by: LB.Thành (08/08/2023)
        Task<FixedAssetPagingModel> GetFilterPagingAsset(FixedAssetFilterModel model);
        /// <summary>
        /// Lấy FixedAssetCode mới
        /// </summary>
        /// <returns>FixedAssetCode mới</returns>
        /// Created by: LB.Thành (1/08/2023)
        public Task<string> GetNewFixedAssetCode();

        /// <summary>
        /// Thực hiện chức năng xuất danh sách tài sản ra file Excel.
        /// </summary>
        /// <param name="fixedAssetExcelModels">Danh sách tài sản để xuất ra Excel.</param>
        /// <returns>Mảng byte chứa dữ liệu của file Excel.</returns>
        /// Created by: LB.Thành (06/08/2023)
        public byte[] ExportFixedAssetListToExcel(IEnumerable<FixedAssetExcelModel> fixedAssetExcelModels);

        /// <summary>
        /// Truy vấn cơ sở dữ liệu để lọc dữ liệu tài sản cố định cho việc chuyển giao.
        /// </summary>
        /// <param name="pageNumber">Số trang hiện tại.</param>
        /// <param name="pageLimit">Số lượng bản ghi trên mỗi trang.</param>
        /// <param name="ids">Danh sách các ID cần lọc (có thể là một chuỗi danh sách).</param>
        /// <returns>Đối tượng chứa danh sách tài sản cố định và tổng số bản ghi.</returns>
        /// Created by: LB.Thành (09/09/2023)
        public Task<FixedAssetForTransferModel> FilterFixedAssetForTransfer(int? pageNumber, int? pageLimit, string ids, string detailIds);
    }
}
