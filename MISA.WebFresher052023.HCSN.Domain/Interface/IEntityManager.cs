using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher052023.HCSN.Domain.Interface
{
    public interface IEntityManager
    {
        Task CheckExistedEntityByCode(string code, string? oldCode = null);
    }
}
