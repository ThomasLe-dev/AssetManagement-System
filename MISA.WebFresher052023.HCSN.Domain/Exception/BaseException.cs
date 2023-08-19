using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MISA.WebFresher052023.HCSN.Domain
{
    public class BaseException
    {
        #region Properties

        /// <summary>
        /// Mã lỗi
        /// </summary>
        public int ErrorCode { get; set; }

        /// <summary>
        /// Thông báo lỗi dành cho lập trình viên
        /// </summary>
        public string? DevMessage { get; set; }

        /// <summary>
        /// Thông báo lỗi dành cho người dùng
        /// </summary>
        public string? UserMessage { get; set; }

        /// <summary>
        /// TraceId của lỗi (dùng để theo dõi lỗi)
        /// </summary>
        public string? TraceId { get; set; }

        /// <summary>
        /// Thông tin thêm về lỗi
        /// </summary>
        public string? MoreInfo { get; set; }

        /// <summary>
        /// Danh sách các lỗi chi tiết
        /// </summary>
        public string? Errors { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Chuyển đổi đối tượng BaseException thành chuỗi JSON
        /// </summary>
        /// <returns>Chuỗi JSON đại diện cho đối tượng BaseException</returns>
        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }

        #endregion
    }
}
