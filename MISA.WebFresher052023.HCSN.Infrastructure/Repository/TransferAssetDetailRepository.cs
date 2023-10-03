using Dapper;
using MISA.WebFresher052023.HCSN.Domain.Entity;
using MISA.WebFresher052023.HCSN.Domain.Interface;
using MISA.WebFresher052023.HCSN.Domain.Model.Transfer_Asset_Detail_Model;
using MISA.WebFresher052023.HCSN.Domain.Model.Transfer_Asset_Model;
using MISA.WebFresher052023.HCSN.Infrastructure.Repository.Base;
using MISA.WebFresher052023.HCSN.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher052023.HCSN.Infrastructure.Repository
{
    public class TransferAssetDetailRepository : BaseRepository<TransferAssetDetail>, ITransferAssetDetailRepository
    {
        #region Fields
        private readonly IUnitOfWork _uow; 
        #endregion

        #region Constructors
        public TransferAssetDetailRepository(IUnitOfWork uow) : base(uow)
        {
            _uow = uow;
        } 
        #endregion

        #region Methods
        public async Task<TransferAssetDetailPagingModel> GetAllByTransferAsset(TransferAssetDetailFilterModel model)
        {
            // Tên stored procedure trong database để lấy dữ liệu phân trang 
            var procname = $"Proc_GetAllDetailsPagingByTransfer";

            // Tạo các tham số động cho stored procedure
            var parameters = new DynamicParameters();
            foreach (var property in typeof(TransferAssetDetailFilterModel).GetProperties())
            {
                parameters.Add(property.Name, property.GetValue(model));
            }
            // Thêm một tham số để lấy tổng số bản ghi 
            parameters.Add("DetailsTotal", dbType: DbType.Int32, direction: ParameterDirection.Output);
            var transferAssetDetailEntities = await _uow.Connection.QueryAsync<TransferAssetDetail>(procname, parameters, commandType: CommandType.StoredProcedure, transaction: _uow.Transaction);

            // Lấy tổng số bản ghi từ tham số đầu ra
            var transferAssetDetailTotal = parameters.Get<int>("DetailsTotal");

            var transferAssetDetailPaging = new TransferAssetDetailPagingModel
            {
                TotalRecords = transferAssetDetailTotal,
                Entities = transferAssetDetailEntities
            };

            return transferAssetDetailPaging;
        }

        /// <summary>
        /// Thực hiện chèn nhiều đối tượng TransferAssetDetail vào cơ sở dữ liệu.
        /// </summary>
        /// <param name="entities">Danh sách các đối tượng TransferAssetDetail cần chèn.</param>
        /// <returns>Trả về true nếu việc chèn thành công, ngược lại trả về false.</returns>
        /// <created>
        /// Created by: LB.Thành (06/09/2023)
        /// </created>
        public async Task<bool> InsertMultiAsync(List<TransferAssetDetail> entities)
        {
            string sql = GenerateInsertQuery();
            var affectedRow = await _uow.Connection.ExecuteAsync(sql, entities, transaction: _uow.Transaction);
            return affectedRow > 0;
        }

        public string GenerateInsertQuery()
        {
            string fields = string.Join(", ", typeof(TransferAssetDetail).GetProperties().Select(prop => prop.Name));
            string values = string.Join(", ", typeof(TransferAssetDetail).GetProperties().Select(prop => $"@{prop.Name}"));
            return $"Insert into {TableName} ({fields}) values ({values})";
        }

        /// <summary>
        /// Cập nhật nhiều bản ghi TransferAssetDetail trong cơ sở dữ liệu.
        /// </summary>
        /// <param name="entities">Danh sách các đối tượng TransferAssetDetail cần cập nhật.</param>
        /// <returns>Trả về true nếu có ít nhất một bản ghi được cập nhật thành công, ngược lại trả về false.</returns>
        /// <remarks>
        /// Created by: LB.Thành (04/09/2023)
        /// </remarks>
        public async Task<bool> UpdateManyAsync(List<TransferAssetDetail> entities)
        {
            // Tạo danh sách các tên cột cần cập nhật
            var columnNames = typeof(TransferAssetDetail).GetProperties().Select(prop => prop.Name);

            // Tạo danh sách các thay thế tham số
            var paramList = new List<object>();
            foreach (var entity in entities)
            {
                var param = new DynamicParameters();
                foreach (var columnName in columnNames)
                {
                    // Đặt giá trị tham số cho từng cột
                    param.Add($"@{columnName}", typeof(TransferAssetDetail).GetProperty(columnName).GetValue(entity));
                }
                paramList.Add(param);
            }

            // Tạo câu truy vấn SQL UPDATE
            string setClauses = string.Join(", ", columnNames.Select(columnName => $"{columnName} = @{columnName}"));
            string sql = $"UPDATE TransferAssetDetail SET {setClauses} WHERE TransferAssetDetailId = @{TableId}";

            // Thực hiện truy vấn SQL và lấy số bản ghi bị ảnh hưởng
            var affectedRows = await _uow.Connection.ExecuteAsync(sql, paramList, transaction: _uow.Transaction);

            // Trả về true nếu có ít nhất một bản ghi được cập nhật thành công
            return affectedRows > 0;
        }

        /// <summary>
        /// Lấy danh sách các bản ghi trong danh sách id
        /// </summary>
        /// <param name="ids">Danh sách id của các bản ghi cần lấy dữ liệu</param>
        /// <returns>Danh sách bản ghi</returns>
        /// Created by: LB.Thành (06/09/2023)
        public async Task<List<TransferAssetDetail>> GetListDetailByIdsAsync(List<Guid> ids)
        {
            string listId = "";
            if (ids != null && ids.Count > 0)
            {
                listId = string.Join(",", ids.Select(id => id.ToString()));
            }
            string procedureName = "Proc_GetTransferDetaiByAssetId";
            var parameters = new
            {
                p_List = listId
            };

            var entities = await _uow.Connection.QueryAsync<TransferAssetDetail>(procedureName, parameters, commandType: CommandType.StoredProcedure, transaction: _uow.Transaction);
            return entities.ToList();
        }

        public async Task<IEnumerable<TransferAssetDetail>> GetAllByTransferAsset(Guid id)
        {
            var query = $"SELECT * FROM TransferAssetDetail  WHERE TransferAssetId = @id";
            var parameters = new DynamicParameters();
            parameters.Add("id", id);

            var result = await _uow.Connection.QueryAsync<TransferAssetDetail>(query, parameters, transaction: _uow.Transaction);
            return result;
        }
        #endregion
    }
}
