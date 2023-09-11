using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.WebFresher052023.HCSN.Application.DTO.TransferAssetDetail;
using MISA.WebFresher052023.HCSN.Application.Interface;

namespace MISA.WebFresher052023.HCSN.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class TransferAssetDetailController : ControllerBase
    {
        #region Fields
        private readonly ITransferAssetDetailService _transferAssetDetailService;
        #endregion

        #region Constructors
        public TransferAssetDetailController(ITransferAssetDetailService transferAssetDetailService)
        {
            _transferAssetDetailService = transferAssetDetailService;
        }
        #endregion
        [HttpGet] 
        public async Task<IActionResult> GetAllByTransferAssetAsync([FromQuery] Guid id)
        {
            var result = await _transferAssetDetailService.GetAllByTransferAsset(id);
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> InsertManyAsync([FromBody] List<TransferAssetDetailDto> dtos)
        {
            await _transferAssetDetailService.InsertManyAsync(dtos);
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteManyAsync([FromBody] List<Guid> ids)
        {
            await _transferAssetDetailService.DeleteManyAsync(ids);
            return StatusCode(StatusCodes.Status200OK);
        }
        
    }
}
