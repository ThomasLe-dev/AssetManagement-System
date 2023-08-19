using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher052023.HCSN.Domain.Interface
{
    public interface IUnitOfWork : IDisposable, IAsyncDisposable
    {
        #region Fields
        DbConnection Connection { get; }
        DbTransaction? Transaction { get; }
        #endregion

        #region Methods
        /// <summary>
        /// Mở transaction
        /// </summary>
        /// CreatedBy: LB.Thành (18/07/2023)
        void BeginTransaction();

        /// <summary>
        /// Mở transaction bất đồng bộ
        /// </summary>
        /// CreatedBy: LB.Thành (18/07/2023)
        Task BeginTransactionAsync();

        /// <summary>
        /// Xác nhận transaction
        /// </summary>
        /// CreatedBy: LB.Thành (18/07/2023)
        void Commit();

        /// <summary>
        /// Xác nhận transaction bất đồng bộ
        /// </summary>
        /// CreatedBy: LB.Thành (18/07/2023)
        Task CommitAsync();

        /// <summary>
        /// Rollback transaction
        /// </summary>
        /// CreatedBy: LB.Thành (18/07/2023)
        void Rollback();

        /// <summary>
        /// Rollback transaction transaction bất đồng bộ
        /// </summary>
        /// CreatedBy: LB.Thành (18/07/2023)
        Task RollbackAsync();
        #endregion
    }
}
