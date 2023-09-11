using AutoMapper;
using MISA.WebFresher052023.HCSN.Application.DTO;
using MISA.WebFresher052023.HCSN.Application.DTO.AssetDTO;
using MISA.WebFresher052023.HCSN.Domain;
using MISA.WebFresher052023.HCSN.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher052023.HCSN.Application
{
    public class FixedAssetProfile : Profile
    {
        /// <summary>
        /// Auto Mapper xử lý ánh xạ Asset
        /// </summary>
        /// Created by: LB.Thành (19/07/2023)
        public FixedAssetProfile()
        {
            CreateMap<FixedAsset, FixedAssetDto>();
            CreateMap<FixedAsset, FixedAssetModel>();
            CreateMap<FixedAssetModel, FixedAssetDto>();
            CreateMap<FixedAsset, FixedAssetExcelModel>();
            CreateMap<FixedAssetFilterDto, FixedAssetFilterModel>();
            CreateMap<FixedAssetPagingModel, FixedAssetPagingDto>().ForMember(
                dest => dest.FixedAssets, opt => opt.MapFrom(src => src.Entities));
            CreateMap<FixedAssetCreateDto, FixedAsset>();
            CreateMap<FixedAssetUpdateDto, FixedAsset>();
        }
    }
}
