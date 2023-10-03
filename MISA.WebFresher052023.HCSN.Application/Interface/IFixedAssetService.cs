using MISA.WebFresher052023.HCSN.Application.DTO;
using MISA.WebFresher052023.HCSN.Application.DTO.AssetDTO;
using MISA.WebFresher052023.HCSN.Application.Interface.Base;
using MISA.WebFresher052023.HCSN.Application.Response;
using MISA.WebFresher052023.HCSN.Domain;
using MISA.WebFresher052023.HCSN.Domain.Enum;
using MISA.WebFresher052023.HCSN.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher052023.HCSN.Application.Interface
{
    public interface IFixedAssetService : IBaseService<FixedAssetDto, FixedAssetCreateDto, FixedAssetUpdateDto>
    {
        /// <summary>
        /// Lấy ra danh sách tài sản theo điều kiện lọc, phân trang và trả về tổng số bản ghi
        /// </summary>
        /// <param name="model"></param>
        /// <returns>danh sách tài sản hợp lệ và tổng số bản ghi</returns>
        /// Created by: LB.Thành (08/08/2023)
        public Task<FixedAssetPagingDto> GetFilterPagingAsset(FixedAssetFilterDto dto);
        /// <summary>
        /// Lấy FixedAssetCode mới
        /// </summary>
        /// <returns>FixedAssetCode mới</returns>
        /// Created by: LB.Thành (1/08/2023)
        public Task<string> GetNewFixedAssetCodeAsync();
        /// <summary>
        /// Xuất danh sách tài sản ra tệp Excel bất đồng bộ.
        /// </summary>
        /// <param name="fixedAssetIds">Danh sách Id tài sản để xuất ra Excel.</param>
        /// <returns>Mảng byte chứa dữ liệu của tệp Excel.</returns>
        /// Created by: LB.Thành (06/08/2023)
        Task<byte[]> ExportFixedAssetListToExcelAsync(List<Guid> fixedAssetIds);

        /// <summary>
        /// Lấy danh sách tài sản có loại những bản ghi đã chọn để hiện thị trên form chọn tài sản cho chứng từ
        /// </summary>
        /// <param name="pageNumber">Số trang</param>
        /// <param name="pageLimit">Số lượng tối đa bản ghi mỗi trang</param>
        /// <param name="dtos">Danh sách truyền vào để loại những bản ghi đó ra</param>
        /// <returns>Danh sách loại tài sản đáp ứng đúng các điều kiện trên</returns>
        /// Created by: LB.Thành (09/09/2023)
        public Task<BaseFilterResponse<FixedAssetDto>> FilterFixedAssetForTransfer(int? pageNumber, int? pageLimit, List<FixedAssetDto> dtos, List<Guid> transferAssetDetailIds);

        /// <summary>
        /// Kiểm tra tài sản có phát sinh chứng từ không
        /// </summary>
        /// <param name="assetIds">Danh sách id tài sản</param>
        /// <param name="action">Hành động như xóa hay cập nhật</param>
        /// <returns>Ném ra 1 ngoại lệ nếu có chứng từ phát sinh</returns>
        /// Created by: ldtuan (17/09/2023)
        public Task CheckExistTransferAsync(List<Guid> assetIds, ActionMode action);
    }
}
