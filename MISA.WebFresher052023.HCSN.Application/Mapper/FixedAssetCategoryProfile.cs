using AutoMapper;
using MISA.WebFresher052023.HCSN.Application.DTO.AssetTypeDto;
using MISA.WebFresher052023.HCSN.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher052023.HCSN.Application.Mapper
{
    public class FixedAssetCategoryProfile : Profile
    {
        /// <summary>
        /// AutoMapper xử lý ánh xạ AssetTypeDto
        /// </summary>
        /// Created by: LB.Thành (25/07/2023)
        public FixedAssetCategoryProfile() 
        {
            CreateMap<FixedAssetCategory, FixedAssetCategoryDto>();
            CreateMap<FixedAssetCategoryCreateDto, FixedAssetCategory>();
            CreateMap<FixedAssetCategoryUpdateDto, FixedAssetCategory>();
        }
    }
}
