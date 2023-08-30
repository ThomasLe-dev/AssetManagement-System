using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher052023.HCSN.Application.DTO.TransferAssetDto
{
    public class TransferAssetPagingDto
    {
        /// <summary>
        /// Tổng số bản ghi
        /// </summary>
        /// Created By: LB.Thành (08/08/2023)
        public int TotalRecords { get; set; }

        /// <summary>
        /// Danh sách chứng từ trong một trang
        /// </summary>
        /// Created By: LB.Thành (08/08/2023)
        public IEnumerable<TransferAssetDto> TransferAssets { get; set; }
    }
}
