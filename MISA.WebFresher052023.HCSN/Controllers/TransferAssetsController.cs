using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.WebFresher052023.HCSN.Application.DTO;
using MISA.WebFresher052023.HCSN.Application.DTO.TransferAssetDto;
using MISA.WebFresher052023.HCSN.Application.Interface;

namespace MISA.WebFresher052023.HCSN.Controllers
{
    [Route("api/[controller]")]
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
        [HttpGet]
        public async Task<IActionResult> GetAllTransferAssetPagingAsync([FromQuery] TransferAssetFilterDto transferAssetFilterDto)
        {
            var assetTransferList = await _transferAssetService.GetFilterPagingAssetAsync(transferAssetFilterDto);
            return Ok(assetTransferList);
        }
    }
}
