using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher052023.HCSN.Domain.Model
{
    public class BasePagingModel<TEntity>
    {
        /// <summary>
        /// Tổng số bản ghi
        /// </summary>
        /// Created By: LB.Thành (08/08/2023)
        public int TotalRecords { get; set; }

        /// <summary>
        /// Danh sách bản ghi
        /// </summary>
        /// Created By: LB.Thành (08/08/2023)
        public IEnumerable<TEntity> Entities { get; set; }
    }
}
