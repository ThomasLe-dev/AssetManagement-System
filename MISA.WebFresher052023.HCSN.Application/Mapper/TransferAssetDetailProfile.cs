using AutoMapper;
using MISA.WebFresher052023.HCSN.Application.DTO.TransferAssetDetail;
using MISA.WebFresher052023.HCSN.Domain.Entity;
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
        }
    }
}
