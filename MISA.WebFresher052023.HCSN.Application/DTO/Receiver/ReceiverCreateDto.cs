using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher052023.HCSN.Application.DTO.Receiver
{
    public class ReceiverCreateDto
    {
        /// <summary>
        /// Số thứ tự người nhận
        /// </summary>
        public int ReceiverOrder { get; set; }
        /// <summary>
        /// Họ tên đầy đủ
        /// </summary>
        public string FullName { get; set; }
        /// <summary>
        /// Đại diện
        /// </summary>
        public string Delegate { get; set; }
        /// <summary>
        /// Chức vụ
        /// </summary>
        public string Position { get; set; }
        /// <summary>
        /// Id chứng từ
        /// </summary>
        public Guid TransferAssetId { get; set; }
    }
}
