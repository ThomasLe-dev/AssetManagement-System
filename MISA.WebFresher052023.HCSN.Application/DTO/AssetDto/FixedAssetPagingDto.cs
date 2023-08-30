using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher052023.HCSN.Application.DTO
{
    public class FixedAssetPagingDto
    {
        /// <summary>
        /// Tổng số bản ghi
        /// </summary>
        /// Created By: LB.Thành (08/08/2023)
        public int TotalRecords { get; set; }

        /// <summary>
        /// Danh sách tài sản trong một trang
        /// </summary>
        /// Created By: LB.Thành (08/08/2023)
        public IEnumerable<AssetDto> FixedAssets { get; set; }
    }
}
