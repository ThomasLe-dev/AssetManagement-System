using MISA.WebFresher052023.HCSN.Domain.Model.Base_Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher052023.HCSN.Domain.Model
{
    public class FixedAssetFilterModel : BaseFilterModel
    {
        /// <summary>
        /// Từ khóa tìm kiếm theo mã hoặc tên tài sản
        /// </summary>
        /// Created By: LB.Thành (08/08/2023)
        public string? FixedAssetCodeOrName { get; set; }

        /// <summary>
        /// Từ khóa tìm kiếm theo phòng ban
        /// </summary>
        /// Created By: LB.Thành (08/08/2023)
        public string? DepartmentName { get; set; }

        /// <summary>
        /// Từ khóa tìm kiếm theo tên loại tài sản
        /// </summary>
        /// Created By: LB.Thành (08/08/2023)
        public string? FixedAssetCategoryName { get; set; }
    }
}
