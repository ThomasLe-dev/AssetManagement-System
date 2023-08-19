using AutoMapper;
using MISA.WebFresher052023.HCSN.Application.DTO.DepartmentDto;
using MISA.WebFresher052023.HCSN.Application.Interface;
using MISA.WebFresher052023.HCSN.Domain.Entity;
using MISA.WebFresher052023.HCSN.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher052023.HCSN.Application.Service
{
    public class DepartmentService : BaseService<Department, DepartmentDto, DepartmentCreateDto, DepartmentUpdateDto>, IDepartmentService
    {
        #region Fields
        private readonly IDepartmentRepository _departmentRepository;
        #endregion

        #region Constructors
        public DepartmentService(IDepartmentRepository departmentRepository, IMapper mapper, IUnitOfWork unitOfWork) : base(departmentRepository, mapper, unitOfWork)
        {
            _departmentRepository = departmentRepository;
        } 
        #endregion

        public override Task<Department> MapCreateDtoToEntity(DepartmentCreateDto entityCreateDto)
        {
            throw new NotImplementedException();
        }

        public override Task<Department> MapUpdateDtoToEntity(Guid id, DepartmentUpdateDto entityUpdateDto)
        {
            throw new NotImplementedException();
        }
    }
}
