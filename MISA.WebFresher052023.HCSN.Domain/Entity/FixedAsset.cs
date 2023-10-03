using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher052023.HCSN.Domain
{
    public class FixedAsset : BaseAudit, IHasKey
    {
        #region Fields
        /// <summary>
        /// Id của tài sản
        /// </summary>
        /// Created by: LB.Thành (07/08/2023)
        [Required]
        public Guid FixedAssetId { get; set; }
        /// <summary>
        /// Mã code của tài sản
        /// </summary>
        /// Created by: LB.Thành (07/08/2023)
        [Required(ErrorMessage = "Phải nhập mã tài sản")]
        [StringLength(50, ErrorMessage = "Mã tài sản không được vượt quá 50 ký tự")]
        public string FixedAssetCode { get; set; }
        /// <summary>
        /// Tên tài sản
        /// </summary>
        /// Created by: LB.Thành (07/08/2023)
        [Required(ErrorMessage = "Phải nhập tên tài sản")]
        [StringLength(255, ErrorMessage = "Tên tài sản không được vượt quá 255 ký tự")]
        public string FixedAssetName { get; set; }
        /// <summary>
        /// Id của đơn vị
        /// </summary>
        /// Created by: LB.Thành (07/08/2023)
        public Guid? OrganizationId { get; set; }
        /// <summary>
        /// Mã đơn vị
        /// </summary>
        /// Created by: LB.Thành (07/08/2023)
        [StringLength(50, ErrorMessage = "Mã đơn vị không được vượt quá 50 ký tự")]
        public string? OrganizationCode { get; set; }
        /// <summary>
        /// Tên của đơn vị
        /// </summary>
        /// Created by: LB.Thành (07/08/2023)
        [StringLength(255, ErrorMessage = "Tên đơn vị không được vượt quá 255 ký tự")]
        public string? OrganizationName { get; set; }
        /// <summary>
        /// Id tài sản
        /// </summary>
        /// Created by: LB.Thành (07/08/2023)
        [Required(ErrorMessage = "Phải nhập id phòng ban")]
        public Guid DepartmentId { get; set; }
        /// <summary>
        /// Mã tài sản
        /// </summary>
        /// Created by: LB.Thành (07/08/2023)
        [Required(ErrorMessage = "Phải nhập mã phòng ban")]
        [StringLength(50, ErrorMessage = "Mã phòng ban không được vượt quá 50 ký tự")]
        public string DepartmentCode { get; set; }
        /// <summary>
        /// Tên tài sản
        /// </summary>
        /// Created by: LB.Thành (07/08/2023)
        [Required(ErrorMessage = "Phải nhập tên phòng ban")]
        [StringLength(255, ErrorMessage = "Tên phòng ban không được vượt quá 255 ký tự")]
        public string DepartmentName { get; set; }
        /// <summary>
        /// Id loại tài sản
        /// </summary>
        /// Created by: LB.Thành (07/08/2023)
        [Required(ErrorMessage = "Phải nhập id loại tài sản")]
        public Guid FixedAssetCategoryId { get; set; }
        /// <summary>
        /// Mã loại tài sản
        /// </summary>
        /// Created by: LB.Thành (07/08/2023)
        [Required(ErrorMessage = "Phải nhập mã loại tài sản")]
        [StringLength(50, ErrorMessage = "Mã loại tài sản không được vượt quá 50 ký tự")]
        public string FixedAssetCategoryCode { get; set; }
        /// <summary>
        /// Tên loại tài sản
        /// </summary>
        /// Created by: LB.Thành (07/08/2023)
        [Required(ErrorMessage = "Phải nhập mã loại tài sản")]
        [StringLength(255, ErrorMessage = "Mã loại tài sản không được vượt quá 255 ký tự")]
        public string FixedAssetCategoryName { get; set; }
        /// <summary>
        /// Ngày mua
        /// </summary>
        /// Created by: LB.Thành (07/08/2023)
        [Required(ErrorMessage = "Phải nhập ngày mua")]
        public DateTime PurchaseDate { get; set; }
        /// <summary>
        /// Ngày bắt đầu sử dụng
        /// </summary>
        /// Created by: LB.Thành (07/08/2023)
        [Required(ErrorMessage = "Phải nhập ngày bắt đầu sử dụng")]
        public DateTime StartUsingDate { get; set; }
        /// <summary>
        /// Nguyên giá
        /// </summary>
        /// Created by: LB.Thành (07/08/2023)
        [Required(ErrorMessage = "Phải nhập nguyên giá")]
        [DisplayFormat(DataFormatString = "{0:n4}")]
        public decimal Cost { get; set; }
        /// <summary>
        /// Số lượng
        /// </summary>
        /// Created by: LB.Thành (07/08/2023)
        [Required(ErrorMessage = "Phải nhập số lượng")]
        [Range(1, int.MaxValue, ErrorMessage = "Số lượng tài sản phái lớn hơn 0")]
        public int Quantity { get; set; }
        /// <summary>
        /// Tỷ lệ hao mòn (%)
        /// </summary>
        /// Created by: LB.Thành (07/08/2023)
        public float? DepreciationRate { get; set; }
        /// <summary>
        /// Năm bắt đầu theo dõi tài sản trên phần mềm
        /// </summary>
        /// Created by: LB.Thành (07/08/2023)
        [Required(ErrorMessage = "Phải nhập năm bắt đầu theo dõi")]
        public int TrackedYear { get; set; }
        /// <summary>
        /// Số năm sử dụng
        /// </summary>
        /// Created by: LB.Thành (07/08/2023)
        [Required(ErrorMessage = "Phải nhập số năm sử dụng")]
        public int LifeTime { get; set; }
        /// <summary>
        /// Năm sử dụng
        /// </summary>
        /// Created by: LB.Thành (07/08/2023)
        [Required(ErrorMessage = "Phải nhập năm sử dụng")]
        public int ProductionYear { get; set; }
        /// <summary>
        /// Sử dụng
        /// </summary>
        /// Created by: LB.Thành (07/08/2023)
        public bool? Active { get; set; }

        /// <summary>
        /// Nhận được giá trị của thuộc tính khóa trong một đối tượng cụ thể 
        /// mà không cần truy cập trực tiếp vào thuộc tính đó, TEntity có thể xài để lấy đc id
        /// </summary>
        /// <returns>Id của tài sản</returns>
        /// Created by: ldtuan (19/07/2023)
        public Guid GetKey()
        {
            return FixedAssetId;
        }

        public Guid SetKey(Guid id)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
