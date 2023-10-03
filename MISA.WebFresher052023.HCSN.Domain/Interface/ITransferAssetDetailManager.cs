using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher052023.HCSN.Domain.Interface
{
    public interface ITransferAssetDetailManager
    {
        /// <summary>
        /// Kiểm tra các chi tiết chứng từ này có chứng từ phát sinh trước đó không
        /// </summary>
        /// <param name="ids">Các id của chi tiết chứng từ</param>
        /// <returns>Nếu có chứng từ phát sinh trước đó thì ném ra exception chứa các chứng từ đó</returns>
        /// Created by: LB.Thành (06/09/2023)
        public Task CheckExistTransferBefore(List<Guid> ids, Guid transferAssetId);
    }
}
