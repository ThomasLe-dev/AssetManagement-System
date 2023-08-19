using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher052023.HCSN.Domain
{
    /// <summary>
    /// Lớp ngoại lệ được sử dụng khi không tìm thấy dữ liệu
    /// </summary>
    public class NotFoundException : Exception
    {
        /// <summary>
        /// Mã lỗi
        /// </summary>
        public int ErrorCode { get; set; }

        /// <summary>
        /// Khởi tạo một instance mới của NotFoundException
        /// </summary>
        public NotFoundException() { }

        /// <summary>
        /// Khởi tạo một instance mới của NotFoundException với mã lỗi
        /// </summary>
        /// <param name="errorCode">Mã lỗi</param>
        public NotFoundException(int errorCode)
        {
            ErrorCode = errorCode;
        }

        /// <summary>
        /// Khởi tạo một instance mới của NotFoundException với thông báo lỗi
        /// </summary>
        /// <param name="message">Thông báo lỗi</param>
        public NotFoundException(string message) : base(message) { }

        /// <summary>
        /// Khởi tạo một instance mới của NotFoundException với thông báo lỗi và mã lỗi
        /// </summary>
        /// <param name="message">Thông báo lỗi</param>
        /// <param name="errorCode">Mã lỗi</param>
        public NotFoundException(string message, int errorCode) : base(message)
        {
            ErrorCode = errorCode;
        }
    }
}
