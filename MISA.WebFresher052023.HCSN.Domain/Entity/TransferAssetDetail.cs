using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher052023.HCSN.Domain.Entity
{
    public class TransferAssetDetail : BaseAudit, IHasKey
    {
        /// <summary>
        /// Id chi tiết chứng từ
        /// </summary>
        public Guid TransferAssetDetailId { get; set; }
        /// <summary>
        /// Id của tài sản
        /// </summary>
        public Guid FixedAssetId { get; set; }
        /// <summary>
        /// Mã tài sản
        /// </summary>
        public string FixedAssetCode { get; set; }
        /// <summary>
        /// Tên tài sản
        /// </summary>
        public string FixedAssetName { get; set;}
        /// <summary>
        /// Nguyên giá
        /// </summary>
        public decimal Cost { get; set;}
        /// <summary>
        /// Giá trị còn lại
        /// </summary>
        public decimal RemainderCost { get; set;}
        /// <summary>
        /// Id của phòng ban
        /// </summary>
        public Guid OldDepartmentId { get; set; }
        /// <summary>
        /// Tên phòng ban
        /// </summary>
        public string OldDepartmentName { get; set;}
        /// <summary>
        /// Id của bộ phận điều chuyển
        /// </summary>
        public Guid TransferDepartmentId { get; set; }
        /// <summary>
        /// Tên của bộ phận điều chuyển
        /// </summary>
        public string TransferDepartmentName { get; set;}
        /// <summary>
        /// Ghi chú
        /// </summary>
        public string? Reason { get; set;}
        /// <summary>
        /// Chứng từ
        /// </summary>
        public Guid TransferAssetId { get; set; }

        public Guid GetKey()
        {
            return TransferAssetDetailId;
        }
    }
}
