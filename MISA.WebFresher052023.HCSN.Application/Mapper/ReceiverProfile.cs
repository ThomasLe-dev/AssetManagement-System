using AutoMapper;
using MISA.WebFresher052023.HCSN.Application.DTO.Receiver;
using MISA.WebFresher052023.HCSN.Domain.Entity;
using System;

namespace MISA.WebFresher052023.HCSN.Application.Mapper
{
    public class ReceiverProfile : Profile
    {
        /// <summary>
        /// Auto Mapper xử lý ánh xạ Receiver
        /// </summary>
        /// Created by: LB.Thành (04/09/2023)
        public ReceiverProfile() 
        {
            CreateMap<Receiver, ReceiverDto>().ReverseMap();
            CreateMap<Receiver, ReceiverCreateDto>().ReverseMap();
            CreateMap<Receiver, ReceiverUpdateDto>().ReverseMap();
        }
    }
}
