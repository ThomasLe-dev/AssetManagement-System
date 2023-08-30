using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher052023.HCSN.Domain.Model.Base_Model
{
    public class BaseFilterModel
    {
        /// <summary>
        /// Số tài sản trong một trang
        /// </summary>
        /// Created By: LB.Thành (08/08/2023)
        [Required(ErrorMessage = "Số bản ghi trong một trang không được để trống")]
        [Range(1, int.MaxValue, ErrorMessage = "Số bản ghi trong một trang phải lớn hơn 0")]
        public int PageLimit { get; set; }

        /// <summary>
        /// Số trang hiện tại
        /// </summary>
        /// Created By: LB.Thành (08/08/2023)
        [Required(ErrorMessage = "Số trang hiện tại không được dể trống")]
        [Range(1, int.MaxValue, ErrorMessage = "Số trang hiện tại phải lớn hơn 0")]
        public int PageNumber { get; set; }
    }
}
