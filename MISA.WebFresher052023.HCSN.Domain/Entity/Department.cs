using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher052023.HCSN.Domain.Entity
{
    public class Department : BaseAudit, IHasKey
    {
        #region Fields
        /// <summary>
        /// Id của phòng ban
        /// </summary>
        /// Created by: LB.Thành (07/08/2023)
        [Required]
        public Guid DepartmentId { get; set; }
        /// <summary>
        /// Mã của phòng ban
        /// </summary>
        /// Created by: LB.Thành (07/08/2023)
        [Required(ErrorMessage = "Phải nhập mã phòng ban")]
        [StringLength(50, ErrorMessage = "Mã phòng ban không được vượt quá 50 ký tự")]
        public string DepartmentCode { get; set; }
        /// <summary>
        /// Tên phòng ban
        /// </summary>
        /// Created by: LB.Thành (07/08/2023)
        [Required(ErrorMessage = "Phải nhập tên phòng ban")]
        [StringLength(255, ErrorMessage = "Mã phòng ban không được vượt quá 255 ký tự")]
        public string DepartmentName { get; set; }
        /// <summary>
        /// Ghi chú
        /// </summary>
        /// Created by: LB.Thành (07/08/2023)
        [StringLength(500, ErrorMessage = "Ghi chú của phòng ban không được vượt quá 500 ký tự")]
        public string? DepartmentDescription { get; set; }
        /// <summary>
        /// Có phải là phòng ban cha không
        /// </summary>
        /// Created by: LB.Thành (07/08/2023)
        public bool? DepartmentIsParent { get; set; }
        /// <summary>
        /// Id phòng ban cha
        /// </summary>
        /// Created by: LB.Thành (07/08/2023)
        public Guid? DepartmentParentId { get; set; }
        /// <summary>
        /// Id của đơn vị
        /// </summary>
        /// Created by: LB.Thành (07/08/2023)
        public Guid? DepartmentOrganizerId { get; set; }

        /// <summary>
        /// Nhận được giá trị của thuộc tính khóa trong một đối tượng cụ thể 
        /// mà không cần truy cập trực tiếp vào thuộc tính đó, TEntity có thể xài để lấy đc id
        /// </summary>
        /// <returns>Id của phòng ban</returns>
        /// Created by: LB.Thành (07/08/2023)
        public Guid GetKey()
        {
            return DepartmentId;
        }
        #endregion
    }
}
