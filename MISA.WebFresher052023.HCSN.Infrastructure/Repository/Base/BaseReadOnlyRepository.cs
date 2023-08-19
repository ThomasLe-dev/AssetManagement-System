using Dapper;
using MISA.WebFresher052023.HCSN.Domain;
using MISA.WebFresher052023.HCSN.Domain.Interface;
using MISA.WebFresher052023.HCSN.Domain.Interface.Base;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher052023.HCSN.Infrastructure.Repository.Base
{
    public abstract class BaseReadOnlyRepository<TEntity> : IReadOnlyRepository<TEntity>
    {
        #region Fields
        protected readonly IUnitOfWork _uow;
        public virtual string TableName { get; } = typeof(TEntity).Name;
        public virtual string TableId { get; protected set; } = typeof(TEntity).Name + "Id";
        #endregion

        #region Constructor
        protected BaseReadOnlyRepository(IUnitOfWork unitOfWork)
        {
            this._uow = unitOfWork;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Hàm lấy toàn bộ bản ghi trong 1 bảng
        /// </summary>
        /// <returns>Toàn bộ bản ghi trong bảng mong muốn</returns>
        /// Created by: LB.Thành (19/07/2023)
        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            var procedure = $"GetAll{TableName}";
            var result = await _uow.Connection.QueryAsync<TEntity>(procedure, commandType: CommandType.StoredProcedure);
            return result;
        }

        /// <summary>
        /// Hàm lấy thông tin bản ghi theo Id
        /// </summary>
        /// <param name="id">Id của bản ghi cần lấy thông tin</param>
        /// <returns>Bản ghi có Id tương ứng</returns>
        /// <exception cref="NotFoundException">Nếu không tìm thấy bản ghi theo Id</exception>
        /// Created by: LB.Thành (19/07/2023)
        public async Task<TEntity> GetAsync(Guid id)
        {
            var entity = await FindAsync(id);
            if (entity == null)
            {
                throw new NotFoundException("Không tìm thấy bản ghi theo Id " + id);
            }
            return entity;
        }

        /// <summary>
        /// Hàm tìm kiếm thông tin bản ghi theo Id
        /// </summary>
        /// <param name="id">Id của bản ghi cần tìm kiếm</param>
        /// <returns>Bản ghi có Id tương ứng hoặc null nếu không tìm thấy</returns>
        /// Created by: LB.Thành (19/07/2023)
        public async Task<TEntity?> FindAsync(Guid id)
        {
            var query = $"SELECT * FROM {TableName}  WHERE {TableId} = @id";
            var parameters = new DynamicParameters();
            parameters.Add("id", id);

            var result = await _uow.Connection.QueryFirstOrDefaultAsync<TEntity>(query, parameters, transaction: _uow.Transaction);
            return result;
        }

        public async Task<List<TEntity>> GetListByIdsAsync(List<Guid> ids)
        {
            var query = $"SELECT * FROM {TableName}  WHERE {TableId} in @ListIds";
            var parameters = new DynamicParameters();
            parameters.Add("ListIds", ids);

            var result = await _uow.Connection.QueryAsync<TEntity>(query, parameters, transaction: _uow.Transaction);
            return result.ToList();
        }
        #endregion
    }
}
