using MISA.WebFresher052023.HCSN.Domain.Interface;
using MISA.WebFresher052023.HCSN.Domain;
using MISA.WebFresher052023.HCSN.Infrastructure.Repository.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MISA.WebFresher052023.HCSN.Domain.Entity;

namespace MISA.WebFresher052023.HCSN.Infrastructure.Repository
{
    public class DepartmentRepository : BaseRepository<Department>, IDepartmentRepository
    {
        #region Constructors
        public DepartmentRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        } 
        #endregion
    }
}
