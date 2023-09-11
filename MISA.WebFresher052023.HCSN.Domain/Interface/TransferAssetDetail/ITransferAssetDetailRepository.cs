using MISA.WebFresher052023.HCSN.Domain.Entity;
using MISA.WebFresher052023.HCSN.Domain.Interface.Base;
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

        public Task<bool> UpdateManyAsync(List<TransferAssetDetail> entities);

        public Task<bool> InsertMultiAsync(List<TransferAssetDetail> entities);
    }
}
