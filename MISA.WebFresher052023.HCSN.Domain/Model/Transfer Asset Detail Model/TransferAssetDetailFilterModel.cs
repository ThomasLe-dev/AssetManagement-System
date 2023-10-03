using MISA.WebFresher052023.HCSN.Domain.Entity;
using MISA.WebFresher052023.HCSN.Domain.Resource;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher052023.HCSN.Domain.Model.Transfer_Asset_Detail_Model
{
    public class TransferAssetDetailFilterModel
    {
        /// <summary>
        /// Số tài sản trong một trang
        /// </summary>
        /// Created By: LB.Thành (08/08/2023)
        public int PageLimit { get; set; }

        /// <summary>
        /// Số trang hiện tại
        /// </summary>
        /// Created By: LB.Thành (08/08/2023)
        public int PageNumber { get; set; }

        /// <summary>
        /// Id của chứng từ
        /// </summary>
        public Guid p_TransferAssetId { get; set; }
    }
}
