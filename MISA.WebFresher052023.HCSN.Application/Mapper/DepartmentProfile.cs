using AutoMapper;
using MISA.WebFresher052023.HCSN.Application.DTO;
using MISA.WebFresher052023.HCSN.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MISA.WebFresher052023.HCSN.Domain.Entity;
using MISA.WebFresher052023.HCSN.Application.DTO.DepartmentDto;

namespace MISA.WebFresher052023.HCSN.Application.Mapper
{
    public class DepartmentProfile : Profile
    {
        /// <summary>
        /// Auto Mapper xử lý ánh xạ DepartmentDto
        /// </summary>
        /// Created by: LB.Thành (19/07/2023)
        public DepartmentProfile() 
        {
            CreateMap<Department, DepartmentDto>();
            CreateMap<DepartmentCreateDto, Department>();
            CreateMap<DepartmentUpdateDto, Department>();
        }
    }
}
