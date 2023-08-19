using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.WebFresher052023.HCSN.Application.DTO.AssetDTO;
using MISA.WebFresher052023.HCSN.Application.Interface.Base;

namespace MISA.WebFresher052023.HCSN.Controllers.Base
{
    public class BaseController<TEntityDto, TEntityCreateDto, TEntityUpadteDto> : BaseReadOnlyController<TEntityDto>
    {
        #region Fields
        private readonly IBaseService<TEntityDto, TEntityCreateDto, TEntityUpadteDto> _baseService; 
        #endregion

        public BaseController(IBaseService<TEntityDto, TEntityCreateDto, TEntityUpadteDto> baseService) : base(baseService)
        {
            _baseService = baseService;
        }
        /// <summary>
        /// Thêm một bản ghi mới 
        /// </summary>
        /// <param name="request">Thông tin bản ghi cần được tạo.</param>
        /// <returns>ActionResult: Kết quả của việc thêm bản ghi.</returns>
        /// Created by: LB.Thành (19/07/2023)
        [HttpPost]
        public async Task<IActionResult> InsertAsync([FromBody] TEntityCreateDto request)
        {
            await _baseService.CreateAsync(request);
            return StatusCode(StatusCodes.Status201Created);
        }


        /// <summary>
        /// Cập nhật 1 bản ghi
        /// </summary>
        /// <param name="request">Thông tin bản ghi cần được cập nhật</param>
        /// <returns>ActionResult: Kết quả của việc cập nhật.</returns>
        /// Created by: LB.Thành (19/07/2023)
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(Guid id ,[FromBody] TEntityUpadteDto request)
        {
            await _baseService.UpdateAsync(id, request);
            return StatusCode(StatusCodes.Status200OK);
        }

        /// <summary>
        /// Xóa 1 bản ghi
        /// </summary>
        /// <param name="id">Id của bản ghi muốn xóa</param>
        /// Created by: LB.Thành (19/07/2023)
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            await _baseService.DeleteAsync(id);
            return StatusCode(StatusCodes.Status200OK);
        }

        /// <summary>
        /// Xóa nhiều bản ghi 
        /// </summary>
        /// <param name="ids">danh sách id bản ghi muốn xóa</param>
        /// Created by: LB.Thành (19/07/2023)
        [HttpDelete]
        public async Task<IActionResult> DeleteManyAsync([FromBody] List<Guid> ids)
        {
            await _baseService.DeleteManyAsync(ids);
            return StatusCode(StatusCodes.Status200OK);
        }
    }
}
