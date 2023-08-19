using Microsoft.AspNetCore.Mvc;
using MISA.WebFresher052023.HCSN.Application.DTO;
using MISA.WebFresher052023.HCSN.Application.DTO.AssetDTO;
using MISA.WebFresher052023.HCSN.Application.Interface;
using MISA.WebFresher052023.HCSN.Application.Service;
using MISA.WebFresher052023.HCSN.Controllers.Base;
using MISA.WebFresher052023.HCSN.Domain;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MISA.WebFresher052023.HCSN.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class FixedAssetsController : BaseController<AssetDto, AssetCreateDto, AssetUpdateDto>
    {
        #region Fields
        private readonly IFixedAssetService _assetService;
        #endregion
        #region Constructor
        public FixedAssetsController(IFixedAssetService assetService) : base(assetService)
        {
            _assetService = assetService;
        }
        #endregion
        /// <summary>
        /// Lấy ra danh sách tài sản theo trang và bộ lọc
        /// </summary>
        /// <param name="fixedAssetFilterDto"></param>
        /// <returns>Danh sách bản ghi hợp lệ và tổng số bản ghi</returns>
        /// Created by: LB.Thành (08/08/2023)
        [HttpGet("Filter")]
        public async Task<IActionResult> GetFixedAssetFilterPagingAsync([FromQuery] FixedAssetFilterDto fixedAssetFilterDto)
        {
            var assetList = await _assetService.GetFilterPagingAsset(fixedAssetFilterDto);
            return Ok(assetList);
        }
        /// <summary>
        /// Lấy FixedAssetCode mới
        /// </summary>
        /// <returns>FixedAssetCode mới</returns>
        /// Created by: LB.Thành (1/08/2023)
        [HttpGet("Code")]
        public async Task<IActionResult> GetNewFixedAssetCodeAsync()
        {
            var newFixedAssetCode = await _assetService.GetNewFixedAssetCodeAsync();
            return Ok(newFixedAssetCode);
        }
        /// <summary>
        /// Xuất danh sách tài sản dưới dạng tệp Excel.
        /// </summary>
        /// <param name="fixedAssetIds">Danh sách Id tài sản để xuất ra Excel.</param>
        /// <returns>ActionResult chứa tệp Excel để tải về.</returns>
        /// Created by: LB.Thành (06/08/2023)
        [HttpPost("Export")]
        public async Task<IActionResult> ExportFixedAssetAsync([FromBody] List<Guid> fixedAssetIds)
        {
            // Gọi phương thức từ dịch vụ tài sản để xuất danh sách tài sản ra file Excel
            var contentFile = await _assetService.ExportFixedAssetListToExcelAsync(fixedAssetIds);

            // Trả về tệp Excel để tải về
            return File(contentFile, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Danh_Sach_Tai_San.xlsx");
        }

    }
}
