using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher052023.HCSN.Domain
{
    public interface IHasKey
    {
        /// <summary>
        /// Hàm lấy Id
        /// </summary>
        /// <returns>Id</returns>
        Guid GetKey();
    }
}
