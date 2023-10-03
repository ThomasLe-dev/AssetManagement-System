using Dapper;
using MISA.WebFresher052023.HCSN.Domain;
using MISA.WebFresher052023.HCSN.Domain.Interface;
using MISA.WebFresher052023.HCSN.Domain.Model;
using MISA.WebFresher052023.HCSN.Domain.Model.Fixed_Asset_Model;
using MISA.WebFresher052023.HCSN.Infrastructure.Repository.Base;
using MISA.WebFresher052023.HCSN.Infrastructure.UnitOfWork;
using MySqlConnector;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher052023.HCSN.Infrastructure
{
    public class FixedAssetRepository : BaseRepository<FixedAsset>, IFixedAssetRepository
    {
        #region Fields
        private readonly IUnitOfWork _uow;
        #endregion

        #region Constructor
        /// <summary>
        /// Khởi tạo một đối tượng AssetRepository mới.
        /// </summary>
        /// <param>unit of work</param>
        /// Created by: LB.Thành (16/07/2023)
        public FixedAssetRepository(IUnitOfWork uow) : base(uow)
        {
            _uow = uow;
        }

        #endregion

        #region Methods
        /// <summary>
        /// Tìm kiếm một tài sản dựa trên code.
        /// </summary>
        /// <param name="code">code của tài sản cần tìm.</param>
        /// <returns>Thông tin tài sản hoặc null nếu không tìm thấy.</returns>
        /// Created by: LB.Thành (16/07/2023)
        public async Task<FixedAsset?> FindByCodeAsync(string code)
        {
            var query = $"SELECT * FROM FixedAsset a WHERE a.FixedAssetCode = @code";
            var parameters = new DynamicParameters();
            parameters.Add("code", code);

            var result = await _uow.Connection.QueryFirstOrDefaultAsync<FixedAsset>(query, parameters, transaction: _uow.Transaction);
            return result;
        }

        /// <summary>
        /// Lấy ra danh sách tài sản theo điều kiện lọc, phân trang và trả về tổng số bản ghi
        /// </summary>
        /// <param name="model"></param>
        /// <returns>danh sách tài sản hợp lệ và tổng số bản ghi</returns>
        /// Created by: LB.Thành (08/08/2023)
        public async Task<FixedAssetPagingModel> GetFilterPagingAsset(FixedAssetFilterModel model)
        {
            var procnameTotal = $"Proc_GetAllFixedAssetFilter";

            var parameters = new DynamicParameters();

            foreach (var property in typeof(FixedAssetFilterModel).GetProperties())
            {
                parameters.Add(property.Name, property.GetValue(model));
            }

            var totalRecord = await _uow.Connection.QueryFirstOrDefaultAsync<int>(procnameTotal, parameters, commandType: CommandType.StoredProcedure, transaction: _uow.Transaction);

            var procnamePaging = $"Proc_GetFixedAssetPaging";


            var fixedAssetEntitis = await _uow.Connection.QueryAsync<FixedAsset>(procnamePaging, parameters, commandType: CommandType.StoredProcedure, transaction: _uow.Transaction);

            var fixedAssetPagingEntity = new FixedAssetPagingModel
            {
                TotalRecords = totalRecord,
                Entities = fixedAssetEntitis
            };
            return fixedAssetPagingEntity;
        }
        /// <summary>
        /// Lấy FixedAssetCode mới
        /// </summary>
        /// <returns>FixedAssetCode mới</returns>
        /// Created by: LB.Thành (1/08/2023)
        public async Task<string> GetNewFixedAssetCode()
        {
            var procname = $"Proc_GetNewFixedAssetCode";
            var fixedAssetCode = await _uow.Connection.QueryFirstOrDefaultAsync<string>(procname, commandType: CommandType.StoredProcedure, transaction: _uow.Transaction);
            return fixedAssetCode;
        }
        /// <summary>
        /// Thực hiện chức năng xuất danh sách tài sản ra file Excel.
        /// </summary>
        /// <param name="fixedAssetExcelModels">Danh sách tài sản để xuất ra Excel.</param>
        /// <returns>Mảng byte chứa dữ liệu của file Excel.</returns>
        /// Created by: LB.Thành (06/08/2023)
        public byte[] ExportFixedAssetListToExcel(IEnumerable<FixedAssetExcelModel> fixedAssetExcelModels)
        {
            // Tạo một MemoryStream để lưu dữ liệu file Excel
            var stream = new MemoryStream();

            // Đường dẫn đến tệp mẫu Excel
            var templateFile = Path.Combine(Directory.GetCurrentDirectory(), "TemplateExcel", "TemplateListFixedAsset.xlsx");

            // Tạo FileInfo từ đường dẫn tệp mẫu
            var fileInfo = new FileInfo(templateFile);

            // Kiểm tra nếu tệp mẫu tồn tại
            if (fileInfo.Exists)
            {
                // Sử dụng ExcelPackage để làm việc với tệp Excel
                using var pck = new ExcelPackage(fileInfo);

                // Lấy ra worksheet có tên "FixedAsset"
                var ws = pck.Workbook.Worksheets["FixedAsset"];

                // Dòng bắt đầu của dữ liệu
                var startRow = 2;

                // Lặp qua từng mẫu tài sản để thiết lập dữ liệu và định dạng
                foreach (var fixedAssetExcelModel in fixedAssetExcelModels)
                {
                    ws.Cells.SetCellValue(startRow, 0, startRow - 1);
                    startRow++;
                    ws.Row(startRow).Height = 30;
                    for (var i = 1; i <= 18; i++)
                    {
                        ws.Cells[startRow, i].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                        ws.Cells[startRow, i].Style.Font.Size = 13;
                    }
                }

                // Đặt dữ liệu vào worksheet từ ô "B3" trở đi
                ws.Cells["B3"].LoadFromCollection(fixedAssetExcelModels, PrintHeaders: false);

                // Lưu workbook vào MemoryStream
                pck.SaveAs(stream);

                // Đưa vị trí của MemoryStream về đầu để đọc dữ liệu
                stream.Position = 0;
            }

            // Chuyển dữ liệu từ MemoryStream thành mảng byte
            var contentFile = stream.ToArray();

            // Trả về mảng byte chứa dữ liệu tệp Excel
            return contentFile;
        }

        /// <summary>
        /// Truy vấn cơ sở dữ liệu để lọc dữ liệu tài sản cố định cho việc chuyển giao.
        /// </summary>
        /// <param name="pageNumber">Số trang hiện tại.</param>
        /// <param name="pageLimit">Số lượng bản ghi trên mỗi trang.</param>
        /// <param name="ids">Danh sách các ID cần lọc (có thể là một chuỗi danh sách).</param>
        /// <returns>Đối tượng chứa danh sách tài sản cố định và tổng số bản ghi.</returns>
        /// Created by: LB.Thành (09/09/2023)
        public async Task<FixedAssetForTransferModel> FilterFixedAssetForTransfer(int? pageNumber, int? pageLimit, string ids, string detailIds)
        {
            var procedureName = "Proc_FilterFixedAssetForTransfer";

            var parameters = new DynamicParameters();
            parameters.Add("p_PageNumber", pageNumber);
            parameters.Add("p_PageLimit", pageLimit);
            parameters.Add("p_List", ids);
            parameters.Add("p_ListDetail", detailIds);

            parameters.Add("p_Count", dbType: DbType.Int32, direction: ParameterDirection.Output);

            var entities = await _uow.Connection.QueryAsync<FixedAssetModel>(procedureName, parameters, commandType: CommandType.StoredProcedure, transaction: _uow.Transaction);

            var total = parameters.Get<int>("p_Count");

            var fixedAssetPagingModel = new FixedAssetForTransferModel
            {
                TotalRecords = total,
                FixedAssetModels = entities.ToList()
            };

            return fixedAssetPagingModel;
        }


        #endregion

    }
}
