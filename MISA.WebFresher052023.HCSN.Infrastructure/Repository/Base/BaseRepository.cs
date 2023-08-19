using Dapper;
using MISA.WebFresher052023.HCSN.Domain;
using MISA.WebFresher052023.HCSN.Domain.Interface;
using MISA.WebFresher052023.HCSN.Domain.Interface.Base;
using MISA.WebFresher052023.HCSN.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher052023.HCSN.Infrastructure.Repository.Base
{
    public abstract class BaseRepository<TEntity> : BaseReadOnlyRepository<TEntity>, IBaseRepository<TEntity> where TEntity : IHasKey
    {
        protected BaseRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        #region Methods

        /// <summary>
        /// Hàm thêm 1 bản ghi 
        /// </summary>
        /// <param name="entity">Đối tượng chứa thông tin bản ghi cần thêm</param>
        /// <returns>Task</returns>
        /// Created by: LB.Thành (20/07/2023)
        public async Task InsertAsync(TEntity entity)
        {
            var procedure = $"{TableName}_Create";

            var param = new DynamicParameters();

            var type = typeof(TEntity);
            var properties = type.GetProperties();

            foreach (var property in properties)
            {
                var propertyName = "@" + property.Name;
                var propertyValue = property.GetValue(entity);

                param.Add(propertyName, propertyValue);
            }

            await _uow.Connection.ExecuteAsync(procedure, param, commandType: CommandType.StoredProcedure);
        }

        /// <summary>
        /// Hàm cập nhật 1 bản ghi
        /// </summary>
        /// <param name="id">Id của bản ghi</param>
        /// <param name="entity">Bản ghi cần cập nhật</param>
        /// <returns>Task</returns>
        /// Created by: LB.Thành (19/07/2023)
        public async Task UpdateAsync(Guid id, TEntity entity)
        {
            var procedure = $"{TableName}Update";

            var param = new DynamicParameters();
            param.Add("Id", id);

            var type = typeof(TEntity);
            var properties = type.GetProperties();

            foreach (var property in properties)
            {
                var propertyName = "@" + property.Name;
                var propertyValue = property.GetValue(entity);

                param.Add(propertyName, propertyValue);
            }

            await _uow.Connection.ExecuteAsync(procedure, param, commandType: CommandType.StoredProcedure);
        }

        /// <summary>
        /// Hàm xóa 1 bản ghi
        /// </summary>
        /// <param name="entity">Bản ghi cần xóa</param>
        /// <returns>Task</returns>
        /// Created by: LB.Thành (19/07/2023)
        public async Task DeleteAsync(TEntity entity)
        {
            var query = $"DELETE FROM {TableName} WHERE {TableName}Id = @id";
            var param = new DynamicParameters();

            param.Add("id", entity.GetKey());
            await _uow.Connection.ExecuteAsync(query, param, transaction: _uow.Transaction);
        }

        /// <summary>
        /// Hàm xóa nhiều bản ghi
        /// </summary>
        /// <param name="entities">Danh sách các bản ghi cần xóa</param>
        /// <returns>Số lượng bản ghi đã xóa</returns>
        /// Created by: LB.Thành (19/07/2023)
        public async Task<bool> DeleteManyAsync(List<TEntity> entities)
        {
            var sql = $"DELETE FROM {TableName} WHERE {TableId} IN @ids;";

            var param = new DynamicParameters();
            param.Add("ids", entities.Select(x => x.GetKey()));
            var affectedRow = await _uow.Connection.ExecuteAsync(sql, param, transaction: _uow.Transaction);

            return affectedRow > 0;
        }

        #endregion
    }
}
