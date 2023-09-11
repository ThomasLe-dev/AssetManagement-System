using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher052023.HCSN.Application.Response
{
    public class BaseFilterResponse<TEntityDto>
    {
        public BaseFilterResponse(int? totalPages, int? totalRecords, List<TEntityDto>? data)
        {
            TotalPages = totalPages;
            TotalRecords = totalRecords;
            Data = data;
        }

        /// <summary>
        /// Tổng số trang
        /// </summary>
        /// Created by: LB.Thành (06/09/2023)
        public int? TotalPages { get; set; }

        /// <summary>
        /// Tổng số bản ghi
        /// </summary>
        /// Created by: LB.Thành (06/09/2023)
        public int? TotalRecords { get; set; }

        /// <summary>
        /// Tất cả dữ liệu trả về
        /// </summary>
        /// Created by: LB.Thành (06/09/2023)
        public List<TEntityDto>? Data { get; set; }
    }
}
