using MISA.WebFresher052023.HCSN.Application.DTO.AssetTypeDto;
using MISA.WebFresher052023.HCSN.Application.Interface.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher052023.HCSN.Application.Interface
{
    public interface IFixedAssetCategoryService : IBaseService<FixedAssetCategoryDto, FixedAssetCategoryCreateDto, FixedAssetCategoryUpdateDto>
    {
    }
}
