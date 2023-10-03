using Dapper;
using MISA.WebFresher052023.HCSN.Domain;
using MISA.WebFresher052023.HCSN.Domain.Entity;
using MISA.WebFresher052023.HCSN.Domain.Interface;
using MISA.WebFresher052023.HCSN.Domain.Model;
using MISA.WebFresher052023.HCSN.Domain.Model.Transfer_Asset_Model;
using MISA.WebFresher052023.HCSN.Infrastructure.Repository.Base;
using MISA.WebFresher052023.HCSN.Infrastructure.UnitOfWork;
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
        /// <summary>
        /// Lấy code mơi cho chứng từ
        /// </summary>
        /// <returns></returns>
        /// Created b: LB.Thành (10/09/2023)
        public async Task<string> GetNewTransferAssetCode()
        {
            var procname = $"Proc_GetNewTransferAssetCode";
            var transferAssetCode = await _uow.Connection.QueryFirstOrDefaultAsync<string>(procname, commandType: CommandType.StoredProcedure, transaction: _uow.Transaction);
            return transferAssetCode;
        }

        /// <summary>
        /// Lấy chứng từ mới nhất trong những chứng từ mà tài sản có
        /// </summary>
        /// <param name="assetIds">Danh sách id của tài sản</param>
        /// <returns>Chứng từ mới nhất trong đống tài sản</returns>
        /// Created by: LB.Thành (07/09/2023)
        public async Task<List<TransferAsset>> GetNewestTransferAssetByAssetId(List<Guid> assetIds)
        {
            string listAssetId = "";
            if (assetIds != null && assetIds.Count > 0)
            {
                listAssetId = string.Join(",", assetIds.Select(assetId => assetId.ToString()));
            }

            string procedureName = "Proc_GetNewestTransferAssetByAssetId";
            var paramName = "p_List";
            var dynamicParams = new DynamicParameters();
            dynamicParams.Add(paramName, listAssetId);

            var transferAsset = await _uow.Connection.QueryAsync<TransferAsset>(procedureName, dynamicParams, commandType: CommandType.StoredProcedure, transaction: _uow.Transaction);
            return transferAsset.ToList();
        }

        /// <summary>
        /// Truyền vào 1 danh sách chứng từ, tìm các tài sản trong chứng từ đó, rồi tìm tất cả các chứng từ có chứa các tài sản này
        /// </summary>
        /// <param name="transferAssetIds">Danh sách id chứng từ</param>
        /// <returns>Danh sách chứng từ của các tài sản trong các chứng từ truyền vào</returns>
        /// Created by: LB.Thành (09/09/2023)
        public async Task<List<TransferAssetDeleteModel>> GetAllTransferAssetOfAsset(List<Guid> transferAssetIds)
        {
            string listAssetId = "";
            if (transferAssetIds != null && transferAssetIds.Count > 0)
            {
                listAssetId = string.Join(",", transferAssetIds.Select(transferAssetId => transferAssetId.ToString()));
            }

            string procedureName = "Proc_GetAllTransferAssetOfAsset";
            var paramName = "p_List";
            var dynamicParams = new DynamicParameters();
            dynamicParams.Add(paramName, listAssetId);

            var transferAsset = await _uow.Connection.QueryAsync<TransferAssetDeleteModel>(procedureName, dynamicParams, commandType: CommandType.StoredProcedure, transaction: _uow.Transaction);
            return transferAsset.ToList();
        }

        /// <summary>
        /// Lấy các chứng từ của các tài sản truyền đến
        /// </summary>
        /// <param name="assetIds">Danh sách id tài sản</param>
        /// <returns>Danh sách chứng từ, có chứa id tài sản</returns>
        /// Created by: LB.Thành (17/09/2023)
        public async Task<List<TransferAssetDeleteModel>> GetAllTransferAssetByAssetId(List<Guid> assetIds)
        {
            string listAssetId = "";
            if (assetIds != null && assetIds.Count > 0)
            {
                listAssetId = string.Join(",", assetIds.Select(assetId => assetId.ToString()));
            }

            string procedureName = "Proc_GetTransferAssetByAssetId";
            var paramName = "p_List";
            var dynamicParams = new DynamicParameters();
            dynamicParams.Add(paramName, listAssetId);

            var transferAsset = await _uow.Connection.QueryAsync<TransferAssetDeleteModel>(procedureName, dynamicParams, commandType: CommandType.StoredProcedure, transaction: _uow.Transaction);
            return transferAsset.ToList();
        }
    }
}
