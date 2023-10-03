using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.WebFresher052023.HCSN.Application.Interface;

namespace MISA.WebFresher052023.HCSN.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ReceiverController : ControllerBase
    {
        #region Fields
        private readonly IReceiverService _receiverService;
        #endregion

        #region Constructors
        public ReceiverController(IReceiverService receiverService)
        {
            _receiverService = receiverService;
        }
        #endregion

        /// <summary>
        /// Lấy toàn bộ ban giao nhận theo Id chứng từ
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// Created by: LB.Thành (10/09/2023)
        [HttpGet("{id}")]
        public async Task<IActionResult> getAllReceiverByTransferAssetAsync([FromRoute] Guid id)
        {
            var result = await _receiverService.GetAllByTransferAsset(id);
            return Ok(result);
        }

        /// <summary>
        /// Lấy phòng ban giao nhận mới nhất
        /// </summary>
        /// <returns></returns>
        /// Created by: LB.Thành (14/09/2023)
        [HttpGet]
        public async Task<IActionResult> getNewestReceiverAsync()
        {
            var result = await _receiverService.GetNewestReceiverAsync();
            return Ok(result);
        }
    }
}
