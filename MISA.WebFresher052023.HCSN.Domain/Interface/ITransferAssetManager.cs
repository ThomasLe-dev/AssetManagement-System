using MISA.WebFresher052023.HCSN.Domain.Entity;
using MISA.WebFresher052023.HCSN.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher052023.HCSN.Domain.Interface
{
    public interface ITransferAssetManager
    {
        /// <summary>
        /// Kiểm tra xem các đối tượng đầu vào có null hay không và ném ngoại lệ InvalidDataException nếu có bất kỳ đối tượng nào là null.
        /// </summary>
        /// <typeparam name="T">Kiểu dữ liệu của danh sách chi tiết.</typeparam>
        /// <param name="transferAsset">Đối tượng TransferAsset.</param>
        /// <param name="listTransferAssetDetail">Danh sách chi tiết TransferAsset.</param>
        ///// <exception cref="InvalidDataException">Ngoại lệ nếu bất kỳ đối tượng nào là null.</exception>
        /// Created by: LB.Thành (06/09/2023)
        public void CheckNullRequest<T>(TransferAsset? transferAsset, List<T>? listTransferAssetDetail);

        /// <summary>
        /// Kiểm tra ngày điều chuyển có lớn hơn ngày chứng từ hay không
        /// </summary>
        /// <param name="transferAsset">Chứng từ</param>
        /// <exception cref="DataException">Lỗi data truyền về</exception>
        /// Created by: LB.Thành (06/09/2023)
        public void CheckTransferDate(TransferAsset? transferAsset);

        /// <summary>
        /// Kiểm tra ngày chứng từ của tài sản khi thực hiện các hành động tạo hoặc cập nhật.
        /// </summary>
        /// <param name="transferAsset">Chứng từ điều chuyển tài sản.</param>
        /// <param name="listAssetIds">Danh sách ID của tài sản.</param>
        /// <param name="actionMode">Chế độ hành động (CREATE hoặc UPDATE).</param>
        /// <returns>Task.</returns>
        /// Created by: LB.Thành (10/09/2023)
        public Task CheckDateAsync(TransferAsset? transferAsset, List<Guid> listAssetIds, ActionMode actionMode);
    }
}
