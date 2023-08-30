using MISA.WebFresher052023.HCSN.Application.DTO.AssetDTO;
using MISA.WebFresher052023.HCSN.Application.DTO;
using MISA.WebFresher052023.HCSN.Domain.Model;
using MISA.WebFresher052023.HCSN.Domain;
using AutoMapper;
using System;
using MISA.WebFresher052023.HCSN.Domain.Entity;
using MISA.WebFresher052023.HCSN.Application.DTO.TransferAssetDto;
using MISA.WebFresher052023.HCSN.Domain.Model.Transfer_Asset_Model;

namespace MISA.WebFresher052023.HCSN.Application.Mapper
{
    public class TransferAssetProfile : Profile
    {
        /// <summary>
        /// Xử lý ánh xạ Transfer Asset
        /// </summary>
        /// Created by: LB.Thành (28/08/2023)
        public TransferAssetProfile() 
        {
            CreateMap<TransferAsset, TransferAssetDto>();
            CreateMap<TransferAssetFilterDto, TransferAssetFilterModel>();
            CreateMap<TransferAssetPagingModel, TransferAssetPagingDto>().ForMember(
                dest => dest.TransferAssets, opt => opt.MapFrom(src => src.Entities)); 
            CreateMap<TransferAssetCreateDto, TransferAsset>();
            CreateMap<TransferAssetUpdateDto, TransferAsset>();
        }
    }
}
