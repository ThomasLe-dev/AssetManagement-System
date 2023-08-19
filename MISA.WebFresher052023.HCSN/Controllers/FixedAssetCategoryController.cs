using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.WebFresher052023.HCSN.Application.DTO.AssetDTO;
using MISA.WebFresher052023.HCSN.Application.DTO;
using MISA.WebFresher052023.HCSN.Controllers.Base;
using MISA.WebFresher052023.HCSN.Application.DTO.AssetTypeDto;
using MISA.WebFresher052023.HCSN.Application.Interface.Base;
using MISA.WebFresher052023.HCSN.Application.Interface;

namespace MISA.WebFresher052023.HCSN.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class FixedAssetCategoryController : BaseController<FixedAssetCategoryDto, FixedAssetCategoryCreateDto, FixedAssetCategoryUpdateDto>
    {
        #region Constructors
        public FixedAssetCategoryController(IFixedAssetCategoryService assetTypeService) : base(assetTypeService)
        {
        } 
        #endregion
    }
}
