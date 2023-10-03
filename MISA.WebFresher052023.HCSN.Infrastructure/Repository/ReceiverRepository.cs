using Dapper;
using MISA.WebFresher052023.HCSN.Domain;
using MISA.WebFresher052023.HCSN.Domain.Entity;
using MISA.WebFresher052023.HCSN.Domain.Interface;
using MISA.WebFresher052023.HCSN.Infrastructure.Repository.Base;
using MISA.WebFresher052023.HCSN.Infrastructure.UnitOfWork;
using System;
using System.Data;
using static Dapper.SqlMapper;

namespace MISA.WebFresher052023.HCSN.Infrastructure.Repository
{
    public class ReceiverRepository : BaseRepository<Receiver>, IReceiverRepository
    {
        #region Fields
        private readonly IUnitOfWork _uow;
        #endregion

        #region Constructors
        public ReceiverRepository(IUnitOfWork uow) : base(uow)
        {
            _uow = uow;
        }
        #endregion

        #region Methods
        public async Task<IEnumerable<Receiver>> GetAllByTransferAsset(Guid id)
        {
            var query = $"SELECT * FROM Receiver  WHERE TransferAssetId = @id";
            var parameters = new DynamicParameters();
            parameters.Add("id", id);

            var result = await _uow.Connection.QueryAsync<Receiver>(query, parameters, transaction: _uow.Transaction);
            return result;
        }
        /// <summary>
        /// Cập nhật nhiều bản ghi Receiver trong cơ sở dữ liệu.
        /// </summary>
        /// <param name="entities">Danh sách các đối tượng Receiver cần cập nhật.</param>
        /// <returns>Trả về true nếu có ít nhất một bản ghi được cập nhật thành công, ngược lại trả về false.</returns>
        /// <remarks>
        /// Created by: LB.Thành (04/09/2023)
        /// </remarks>
        public async Task<bool> UpdateManyAsync(List<Receiver> entities)
        {
            // Tạo danh sách các tên cột cần cập nhật
            var columnNames = typeof(Receiver).GetProperties().Select(prop => prop.Name);

            // Tạo danh sách các thay thế tham số
            var paramList = new List<object>();
            foreach (var entity in entities)
            {
                var param = new DynamicParameters();
                foreach (var columnName in columnNames)
                {
                    // Đặt giá trị tham số cho từng cột
                    param.Add($"@{columnName}", typeof(Receiver).GetProperty(columnName).GetValue(entity));
                }
                paramList.Add(param);
            }

            // Tạo câu truy vấn SQL UPDATE
            string setClauses = string.Join(", ", columnNames.Select(columnName => $"{columnName} = @{columnName}"));
            string sql = $"UPDATE Receiver SET {setClauses} WHERE ReceiverId = @{TableId}";

            // Thực hiện truy vấn SQL và lấy số bản ghi bị ảnh hưởng
            var affectedRows = await _uow.Connection.ExecuteAsync(sql, paramList, transaction: _uow.Transaction);

            // Trả về true nếu có ít nhất một bản ghi được cập nhật thành công
            return affectedRows > 0;
        }

        /// <summary>
        /// Thực hiện chèn nhiều đối tượng Receiver vào cơ sở dữ liệu.
        /// </summary>
        /// <param name="entities">Danh sách các đối tượng Receiver cần chèn.</param>
        /// <returns>Trả về true nếu việc chèn thành công, ngược lại trả về false.</returns>
        /// <created>
        /// Created by: LB.Thành (06/09/2023)
        /// </created>
        public async Task<bool> InsertMultiAsync(List<Receiver> entities)
        {
            string sql = GenerateInsertQuery();
            var affectedRow = await _uow.Connection.ExecuteAsync(sql, entities, transaction: _uow.Transaction);
            return affectedRow > 0;
        }

        public string GenerateInsertQuery()
        {
            string fields = string.Join(", ", typeof(Receiver).GetProperties().Select(prop => prop.Name));
            string values = string.Join(", ", typeof(Receiver).GetProperties().Select(prop => $"@{prop.Name}"));
            return $"Insert into {TableName} ({fields}) values ({values})";
        }

        /// <summary>
        /// Lấy toàn bộ bản ghi theo Id của chứng từ
        /// </summary>
        /// <param name="ids"></param>
        /// <returns>toàn bộ bản ghi theo Id của chứng từ</returns>
        /// Created by: LB.Thành (10/09/2023)
        public async Task<List<Receiver>> GetListReceiverByTransferAsset(List<Guid> ids)
        {
            var query = $"SELECT * FROM Receiver  WHERE TransferAssetId in @ListIds";
            var parameters = new DynamicParameters();
            parameters.Add("ListIds", ids);

            var result = await _uow.Connection.QueryAsync<Receiver>(query, parameters, transaction: _uow.Transaction);
            return result.ToList();
        }

        /// <summary>
        /// Lấy phòng ban giao nhận mới nhất
        /// </summary>
        /// <returns></returns>
        /// Created by: LB.Thành (14/09/2023)
        public async Task<List<Receiver>> GetNewestReceiver()
        {
            string procedureName = "Proc_GetNewestReceiver";
            var receivers = await _uow.Connection.QueryAsync<Receiver>(procedureName, commandType: CommandType.StoredProcedure, transaction: _uow.Transaction);
            return receivers.ToList();
        }
        #endregion
    }
}
