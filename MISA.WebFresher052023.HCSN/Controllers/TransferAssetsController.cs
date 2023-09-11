using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.WebFresher052023.HCSN.Application.DTO;
using MISA.WebFresher052023.HCSN.Application.DTO.TransferAssetDto;
using MISA.WebFresher052023.HCSN.Application.Interface;

namespace MISA.WebFresher052023.HCSN.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class TransferAssetsController : ControllerBase
    {
        #region Fields
        private readonly ITransferAssetService _transferAssetService;
        #endregion
        #region Constructors
        public TransferAssetsController(ITransferAssetService transferAssetService)
        {
            _transferAssetService = transferAssetService;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Phân trang chứng từ
        /// </summary>
        /// <param name="transferAssetFilterDto"></param>
        /// Created by: LB.Thành (26/08/2023)
        [HttpGet]
        public async Task<IActionResult> GetAllTransferAssetPagingAsync([FromQuery] TransferAssetFilterDto transferAssetFilterDto)
        {
            var assetTransferList = await _transferAssetService.GetFilterPagingAssetAsync(transferAssetFilterDto);
            return Ok(assetTransferList);
        }

        /// <summary>
        /// Thêm 1 chứng từ mới
        /// </summary>
        /// <param name="transferAssetCreateDto"></param>
        /// Created by: LB.Thành (26/08/2023)
        [HttpPost]
        public async Task<IActionResult> InsertTransferAssetAsync([FromBody] TransferAssetCreateDto transferAssetCreateDto)
        {
            await _transferAssetService.CreateAsync(transferAssetCreateDto);

            return StatusCode(StatusCodes.Status201Created);
        }
        #endregion
    }
}
