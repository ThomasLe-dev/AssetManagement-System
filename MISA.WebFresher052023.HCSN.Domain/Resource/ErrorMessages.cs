using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher052023.HCSN.Domain.Resource
{
    public class ErrorMessages
    {
        public static string InvalidData => "Dữ liệu rỗng hoặc không hợp lệ";
        public static string NotFound => "Không tìm thấy tài nguyên";
        public static string ServerError => "Đã có lỗi xảy ra phía server, vui lòng liên hệ trung tâm CSKH MISA để được hỗ trợ";
    }
}
