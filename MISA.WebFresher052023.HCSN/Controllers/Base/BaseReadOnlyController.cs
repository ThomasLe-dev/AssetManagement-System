using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.WebFresher052023.HCSN.Application.DTO;
using MISA.WebFresher052023.HCSN.Application.Interface.Base;

namespace MISA.WebFresher052023.HCSN.Controllers.Base
{
    public class BaseReadOnlyController<TEntityDto> : ControllerBase
    {
        #region Fields
        private readonly IReadOnlyService<TEntityDto> _readOnlyservice; 
        #endregion

        protected BaseReadOnlyController(IReadOnlyService<TEntityDto> readOnlyservice)
        {
            _readOnlyservice = readOnlyservice;
        }

        /// <summary>
        /// Lấy toàn bộ bản ghi trong bảng
        /// </summary>
        /// <returns>toàn bộ bản ghi</returns>
        /// Created by: LB.Thành (19/07/2023)
        [HttpGet]
        public async Task<IEnumerable<TEntityDto>> GetAllAsync()
        {
            var result = await _readOnlyservice.GetAllAsync();
            return result;
        }

        /// <summary>
        /// Lấy 1 bản ghi theo ID
        /// </summary>
        /// <param name="id">Id của bản ghi cần tìm</param>
        /// <returns>1 bản ghi thỏa mãn</returns>
        /// Created by: LB.Thành (19/07/2023)
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            var result = await _readOnlyservice.GetAsync(id);
            return StatusCode(StatusCodes.Status200OK, result);
        }
    }
}
