using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher052023.HCSN.Application.DTO.AssetTypeDto
{
    public class FixedAssetCategoryUpdateDto
    {
        /// <summary>
        /// Mã code của loại tài sản
        /// </summary>
        /// Created by: LB.Thành (07/08/2023)
        [Required(ErrorMessage = "Phải nhập mã loại tài sản")]
        [StringLength(50, ErrorMessage = "Mã loại tài sản không được vượt quá 50 ký tự")]
        public string FixedAssetCategoryCode { get; set; }
        /// <summary>
        /// Tên của loại tài sản
        /// </summary>
        /// Created by: LB.Thành (07/08/2023)
        [Required(ErrorMessage = "Phải nhập tên loại tài sản")]
        [StringLength(255, ErrorMessage = "Tên loại tài sản không được vượt quá 50 ký tự")]
        public string FixedAssetCategoryName { get; set; }
        /// <summary>
        /// Id của đơn vị
        /// </summary>
        /// Created by: LB.Thành (07/08/2023)
        public Guid? OrganizationId { get; set; }
        /// <summary>
        /// Tỷ lệ hao mòn (%)
        /// </summary>
        /// Created by: LB.Thành (07/08/2023)
        [Range(0, 100, ErrorMessage = "Tỉ lệ hao mòn phải nằm trong khoảng từ 0 đến 100%")]
        public float? DepreciationRate { get; set; }
        /// <summary>
        /// Số năm sử dụng
        /// </summary>
        /// Created by: LB.Thành (07/08/2023)
        [Range(1, int.MaxValue, ErrorMessage = "Số năm sử dụng phải lớn hơn 0")]
        public int? LifeTime { get; set; }
        /// <summary>
        /// Ghi chú
        /// </summary>
        /// Created by: LB.Thành (07/08/2023)
        [StringLength(500, ErrorMessage = "Ghi chú của loại tài sản không được vượt quá 50 ký tự")]
        public string? Description { get; set; }
    }
}
