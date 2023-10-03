using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.WebFresher052023.HCSN.Application.DTO;
using MISA.WebFresher052023.HCSN.Application.DTO.TransferAssetDto;
using MISA.WebFresher052023.HCSN.Application.Interface;
using MISA.WebFresher052023.HCSN.Domain.Resource;

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
        /// Lấy TransferAssetCode mới
        /// </summary>
        /// <returns>TransferAssetCode mới</returns>
        /// Created by: LB.Thành (10/09/2023)
        [HttpGet("Code")]
        public async Task<IActionResult> GetNewTransferAssetCodeAsync()
        {
            var newTransferAssetCode = await _transferAssetService.GetNewTransferAssetCodeAsync();
            return Ok(newTransferAssetCode);
        }

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

        /// <summary>
        /// Api cập nhật 1 bản ghi
        /// </summary>
        /// <param name="id">Id của bản ghi muốn cập nhật</param>
        /// <param name="assetUpdateDto">Dữ liệu của bản ghi muốn cập nhật</param>
        /// Created by: LB.Thành (19/07/2023)
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTransferAssetAsync([FromRoute] Guid id, [FromBody] TransferAssetUpdateDto transferAssetUpdateDto)
        {
            await _transferAssetService.UpdateAsync(id, transferAssetUpdateDto);
            return StatusCode(StatusCodes.Status200OK);
        }

        /// <summary>
        /// Api xóa nhiều bản ghi
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        /// Created by: LB.Thành (10/09/2023)
        [HttpDelete]
        public async Task<IActionResult> DeleteTransferAssetAsync(List<Guid> ids)
        {
            await _transferAssetService.DeleteManyAsync(ids);
            return StatusCode(StatusCodes.Status200OK);
        }
        #endregion
    }
}
