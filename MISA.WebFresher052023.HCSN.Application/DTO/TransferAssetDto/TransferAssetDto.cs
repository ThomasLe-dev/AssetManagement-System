using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher052023.HCSN.Application.DTO.TransferAssetDto
{
    public class TransferAssetDto
    {
        /// <summary>
        /// Id của chứng từ điều chuyển
        /// </summary>
        public Guid TransferAssetId { get; set; }
        /// <summary>
        /// Mã chứng từ điều chuyển
        /// </summary>
        public string TransferAssetCode { get; set; }
        /// <summary>
        /// Ngày chứng từ
        /// </summary>
        public DateTime? TransactionDate { get; set; }
        /// <summary>
        /// Ngày điều chuyển
        /// </summary>
        public DateTime? TransferDate { get; set; }
        /// <summary>
        /// Nguyên giá
        /// </summary>
        public decimal? Cost { get; set; }
        /// <summary>
        /// Giá trị còn lại
        /// </summary>
        public decimal? RemainCost { get; set; }
        /// <summary>
        /// Ghi chú
        /// </summary>
        public string? Note { get; set; }
    }
}
