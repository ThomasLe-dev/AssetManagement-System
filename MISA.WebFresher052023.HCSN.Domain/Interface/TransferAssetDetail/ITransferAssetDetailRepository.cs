using MISA.WebFresher052023.HCSN.Domain.Entity;
using MISA.WebFresher052023.HCSN.Domain.Interface.Base;
using MISA.WebFresher052023.HCSN.Domain.Model.Transfer_Asset_Detail_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher052023.HCSN.Domain.Interface
{
    public interface ITransferAssetDetailRepository : IBaseRepository<TransferAssetDetail>
    {
        /// <summary>
        /// Lấy toàn bộ bản ghi theo chứng từ
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// Created by: LB.Thành (06/09/2023)
        public Task<IEnumerable<TransferAssetDetail>> GetAllByTransferAsset(Guid id);

        /// <summary>
        /// Cập nhật nhiều bản ghi
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public Task<bool> UpdateManyAsync(List<TransferAssetDetail> entities);

        /// <summary>
        /// chèn nhiều bản ghi
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public Task<bool> InsertMultiAsync(List<TransferAssetDetail> entities);
        
        /// <summary>
        /// Lấy toàn bộ chi tiết theo chứng từ
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public Task<List<TransferAssetDetail>> GetListDetailByIdsAsync(List<Guid> ids);
        /// <summary>
        /// Lấy phân trang chi tiết bản ghi
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Task<TransferAssetDetailPagingModel> GetAllByTransferAsset(TransferAssetDetailFilterModel model);
    }
}
