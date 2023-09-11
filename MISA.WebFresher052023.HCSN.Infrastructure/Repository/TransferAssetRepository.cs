using Dapper;
using MISA.WebFresher052023.HCSN.Domain;
using MISA.WebFresher052023.HCSN.Domain.Entity;
using MISA.WebFresher052023.HCSN.Domain.Interface;
using MISA.WebFresher052023.HCSN.Domain.Model;
using MISA.WebFresher052023.HCSN.Domain.Model.Transfer_Asset_Model;
using MISA.WebFresher052023.HCSN.Infrastructure.Repository.Base;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace MISA.WebFresher052023.HCSN.Infrastructure.Repository
{
    public class TransferAssetRepository : BaseRepository<TransferAsset>, ITransferAssetRepository
    {
        #region Fields
        #endregion

        #region Constructors
        public TransferAssetRepository(IUnitOfWork uow) : base(uow) { }
        #endregion

        /// <summary>
        /// Lấy danh sách các TransferAsset theo trang dựa trên các điều kiện lọc.
        /// </summary>
        /// <param name="model">Mô hình chứa các thông tin lọc.</param>
        /// <returns>Mô hình chứa danh sách TransferAsset phân trang.</returns>
        /// <remarks>
        /// Created by: LB.Thành (28/08/2023)
        /// </remarks>
        public async Task<TransferAssetPagingModel> GetPagingTransferAsset(TransferAssetFilterModel model)
        {
            // Tên stored procedure trong database để lấy dữ liệu phân trang TransferAsset
            var procname = $"Proc_TransferAssetPaging";

            // Tạo các tham số động cho stored procedure
            var parameters = new DynamicParameters();

            // Duyệt qua các thuộc tính của model và thêm chúng vào danh sách tham số
            foreach (var property in typeof(TransferAssetFilterModel).GetProperties())
            {
                parameters.Add(property.Name, property.GetValue(model));
            }

            // Thêm một tham số để lấy tổng số bản ghi TransferAsset
            parameters.Add("TransferAssetTotal", dbType: DbType.Int32, direction: ParameterDirection.Output);

            // Thực hiện truy vấn để lấy danh sách TransferAsset từ stored procedure
            var transferAssetEntities = await _uow.Connection.QueryAsync<TransferAsset>(procname, parameters, commandType: CommandType.StoredProcedure, transaction: _uow.Transaction);

            // Lấy tổng số bản ghi TransferAsset từ tham số đầu ra
            var transferAssetTotal = parameters.Get<int>("TransferAssetTotal");

            // Tạo một TransferAssetPagingModel để đóng gói dữ liệu phân trang
            var transferAssetPagingEntity = new TransferAssetPagingModel
            {
                TotalRecords = transferAssetTotal,
                Entities = transferAssetEntities
            };

            // Trả về kết quả
            return transferAssetPagingEntity;
        }
        /// <summary>
        /// Tìm kiếm một chứng từ dựa trên code.
        /// </summary>
        /// <param name="code">code của tài sản cần tìm.</param>
        /// <returns>Thông tin tài sản hoặc null nếu không tìm thấy.</returns>
        /// Created by: LB.Thành (26/08/2023)
        public async Task<TransferAsset?> FindByCode(string code)
        {
            var query = $"SELECT * FROM TransferAsset a WHERE a.TransferAssetCode = @code";
            var parameters = new DynamicParameters();
            parameters.Add("code", code);

            var result = await _uow.Connection.QueryFirstOrDefaultAsync<TransferAsset>(query, parameters, transaction: _uow.Transaction);
            return result;
        }
    }
}
