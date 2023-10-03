using AutoMapper;
using MISA.WebFresher052023.HCSN.Application.DTO.TransferAssetDetail;
using MISA.WebFresher052023.HCSN.Application.DTO.TransferAssetDto;
using MISA.WebFresher052023.HCSN.Domain.Entity;
using MISA.WebFresher052023.HCSN.Domain.Model;
using MISA.WebFresher052023.HCSN.Domain.Model.Transfer_Asset_Detail_Model;
using MISA.WebFresher052023.HCSN.Domain.Model.Transfer_Asset_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher052023.HCSN.Application.Mapper
{
    public class TransferAssetDetailProfile : Profile
    {
        /// <summary>
        /// Auto Mapper xử lý ánh xạ TransferAssetDetail
        /// </summary>
        /// Created by: LB.Thành (04/09/2023)
        public TransferAssetDetailProfile()
        {
            CreateMap<TransferAssetDetail, TransferAssetDetailDto>().ReverseMap();
            CreateMap<TransferAssetDetail, TransferAssetDetailCreateDto>().ReverseMap();
            CreateMap<TransferAssetDetailFilterDto, TransferAssetDetailFilterModel>().ReverseMap();
            CreateMap<TransferAssetDetailPagingModel, TransferAssetDetailPagingDto>().ReverseMap();
            CreateMap<TransferAssetDetail, TransferAssetDetailUpdateDto>().ReverseMap();
        }
    }
}
