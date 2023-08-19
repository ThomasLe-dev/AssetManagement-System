using MISA.WebFresher052023.HCSN.Application.DTO.DepartmentDto;
using MISA.WebFresher052023.HCSN.Application.Interface.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher052023.HCSN.Application.Interface
{
    public interface IDepartmentService : IBaseService<DepartmentDto, DepartmentCreateDto, DepartmentUpdateDto>
    {
    }
}
