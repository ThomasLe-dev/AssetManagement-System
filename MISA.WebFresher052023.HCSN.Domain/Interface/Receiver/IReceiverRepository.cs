using MISA.WebFresher052023.HCSN.Domain.Entity;
using MISA.WebFresher052023.HCSN.Domain.Interface.Base;
using System;

namespace MISA.WebFresher052023.HCSN.Domain.Interface
{
    public interface IReceiverRepository : IBaseRepository<Receiver>
    {
        public Task<IEnumerable<Receiver>> GetAllByTransferAsset(Guid id);
        /// <summary>
        /// Cập nhật nhiều bản ghi Receiver trong cơ sở dữ liệu.
        /// </summary>
        /// <param name="entities">Danh sách các đối tượng Receiver cần cập nhật.</param>
        /// <returns>Trả về true nếu có ít nhất một bản ghi được cập nhật thành công, ngược lại trả về false.</returns>
        /// <remarks>
        /// Created by: LB.Thành (04/09/2023)
        /// </remarks>
        public Task<bool> UpdateManyAsync(List<Receiver> entities);

        /// <summary>
        /// Thực hiện chèn nhiều đối tượng Receiver vào cơ sở dữ liệu.
        /// </summary>
        /// <param name="entities">Danh sách các đối tượng Receiver cần chèn.</param>
        /// <returns>Trả về true nếu việc chèn thành công, ngược lại trả về false.</returns>
        /// <created>
        /// Created by: LB.Thành (06/09/2023)
        /// </created>
        public Task<bool> InsertMultiAsync(List<Receiver> entities);

        /// <summary>
        /// Lấy toàn bộ bản ghi theo Id của chứng từ
        /// </summary>
        /// <param name="ids"></param>
        /// <returns>toàn bộ bản ghi theo Id của chứng từ</returns>
        /// Created by: LB.Thành (10/09/2023)
        public Task<List<Receiver>> GetListReceiverByTransferAsset(List<Guid> ids);

        /// <summary>
        /// Lấy phòng ban giao nhận mới nhất
        /// </summary>
        /// <returns></returns>
        /// Created by: LB.Thành (14/09/2023)
        public Task<List<Receiver>> GetNewestReceiver();
    }
}
