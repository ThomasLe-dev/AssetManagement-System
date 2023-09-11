using MISA.WebFresher052023.HCSN.Domain.Entity;
using MISA.WebFresher052023.HCSN.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher052023.HCSN.Domain.Service
{
    public class TransferAssetManager : IEntityManager, ITransferAssetManager
    {
        #region Fields
        private readonly ITransferAssetRepository _transferAssetRepository;
        #endregion

        #region Constructors
        public TransferAssetManager(ITransferAssetRepository transferAssetRepository)
        {
            _transferAssetRepository = transferAssetRepository;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Check trùng mã chứng từ
        /// </summary>
        /// <param name="code"></param>
        /// <param name="oldCode"></param>
        /// <returns></returns>
        /// <exception cref="ConflictException"></exception>
        /// Created by: LB.Thành (06/09/2023)
        public async Task CheckExistedEntityByCode(string code, string? oldCode = null)
        {
            var existedTransferAsset = await _transferAssetRepository.FindByCode(code);
            if (existedTransferAsset != null && existedTransferAsset.TransferAssetCode != oldCode)
            {
                throw new ConflictException("Mã chứng từ không được phép trùng lặp");
            }
        }

        /// <summary>
        /// Kiểm tra xem các đối tượng đầu vào có null hay không và ném ngoại lệ InvalidDataException nếu có bất kỳ đối tượng nào là null.
        /// </summary>
        /// <typeparam name="T">Kiểu dữ liệu của danh sách chi tiết.</typeparam>
        /// <param name="transferAsset">Đối tượng TransferAsset.</param>
        /// <param name="listTransferAssetDetail">Danh sách chi tiết TransferAsset.</param>
        ///// <exception cref="InvalidDataException">Ngoại lệ nếu bất kỳ đối tượng nào là null.</exception>
        /// Created by: LB.Thành (06/09/2023)
        public void CheckNullRequest<T>(TransferAsset? transferAsset, List<T>? listTransferAssetDetail)
        {
            if (transferAsset == null || listTransferAssetDetail == null)
            {
                throw new InvalidDataException();
            }
        }

        /// <summary>
        /// Kiểm tra ngày điều chuyển có lớn hơn ngày chứng từ hay không
        /// </summary>
        /// <param name="transferAsset">Chứng từ</param>
        /// <exception cref="DataException">Lỗi data truyền về</exception>
        /// Created by: LB.Thành (06/09/2023)
        public void CheckTransferDate(TransferAsset? transferAsset)
        {
            if (transferAsset != null && transferAsset.TransferDate < transferAsset.TransactionDate)
            {
                throw new InvalidDataException();
            }
        }
        #endregion
    }
}
