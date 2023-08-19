using MISA.WebFresher052023.HCSN.Application.DTO;
using MISA.WebFresher052023.HCSN.Application.DTO.AssetDTO;
using MISA.WebFresher052023.HCSN.Application.Interface.Base;
using MISA.WebFresher052023.HCSN.Domain;
using MISA.WebFresher052023.HCSN.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher052023.HCSN.Application.Interface
{
    public interface IFixedAssetService : IBaseService<AssetDto, AssetCreateDto, AssetUpdateDto>
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
    }
}
