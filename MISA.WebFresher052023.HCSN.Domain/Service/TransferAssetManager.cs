using MISA.WebFresher052023.HCSN.Domain.Entity;
using MISA.WebFresher052023.HCSN.Domain.Enum;
using MISA.WebFresher052023.HCSN.Domain.Interface;
using MISA.WebFresher052023.HCSN.Domain.Resource;
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
        private readonly IFixedAssetRepository _fixedAssetRepository;
        private readonly ITransferAssetDetailRepository _transferAssetDetailRepository;
        #endregion

        #region Constructors
        public TransferAssetManager(ITransferAssetRepository transferAssetRepository, IFixedAssetRepository fixedAssetRepository, ITransferAssetDetailRepository transferAssetDetailRepository)
        {
            _transferAssetRepository = transferAssetRepository;
            _fixedAssetRepository = fixedAssetRepository;
            _transferAssetDetailRepository = transferAssetDetailRepository;
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
                throw new ValidateException("Mã chứng từ không được phép trùng lặp. Vui lòng nhập lại mã khác");
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

        /// <summary>
        /// Kiểm tra ngày chứng từ của tài sản khi thực hiện các hành động tạo hoặc cập nhật.
        /// </summary>
        /// <param name="transferAsset">Chứng từ điều chuyển tài sản.</param>
        /// <param name="listAssetIds">Danh sách ID của tài sản.</param>
        /// <param name="actionMode">Chế độ hành động (CREATE hoặc UPDATE).</param>
        /// <returns>Task.</returns>
        /// Created by: LB.Thành (10/09/2023)
        public async Task CheckDateAsync(TransferAsset? transferAsset, List<Guid> listAssetIds, ActionMode actionMode)
        {
            if (transferAsset == null || listAssetIds.Count == 0)
            {
                return;
            }

            var fixedAssetList = await _fixedAssetRepository.GetListByIdsAsync(listAssetIds);
            var detailList = await _transferAssetDetailRepository.GetListDetailByIdsAsync(listAssetIds);
            var transferList = await _transferAssetRepository.GetNewestTransferAssetByAssetId(listAssetIds);

            if (transferList == null || transferList.Count == 0)
            {
                return;
            }

            var newestTransfer = transferList.FirstOrDefault();

            if (newestTransfer == null)
            {
                return;
            }

            if (actionMode == ActionMode.CREATE)
            {
                CheckCreateTransferDate(transferAsset, newestTransfer, fixedAssetList, detailList, listAssetIds);
            }
            else if (actionMode == ActionMode.UPDATE)
            {
                CheckUpdateTransferDate(transferAsset, newestTransfer, transferList, fixedAssetList, detailList, listAssetIds);
            }
        }

        /// <summary>
        /// Kiểm tra ngày chứng từ khi thực hiện hành động tạo chứng từ điều chuyển tài sản.
        /// </summary>
        /// <param name="transferAsset">Chứng từ điều chuyển tài sản.</param>
        /// <param name="newestTransfer">Chứng từ điều chuyển tài sản mới nhất.</param>
        /// <param name="fixedAssetList">Danh sách tài sản cố định.</param>
        /// <param name="detailList">Danh sách chi tiết chứng từ điều chuyển tài sản.</param>
        /// <param name="listAssetIds">Danh sách ID của tài sản.</param>
        /// <exception cref="ValidateException">Ném ra nếu có lỗi kiểm tra.</exception>
        /// Created by: LB.Thành (10/09/2023)
        private void CheckCreateTransferDate(TransferAsset transferAsset, TransferAsset newestTransfer, List<FixedAsset> fixedAssetList, List<TransferAssetDetail> detailList, List<Guid> listAssetIds)
        {
            if (transferAsset.TransferDate < transferAsset.TransactionDate)
            {
                throw new ValidateException("Ngày điều chuyển phải lớn hơn hoặc bằng ngày chứng từ");
            }
            if (transferAsset.TransferDate <= newestTransfer.TransferDate)
            {
                var fixedAsset = GetRelatedFixedAsset(detailList, newestTransfer, fixedAssetList, listAssetIds);
                throw new ValidateException(TransferAssetErrorMessages.InforDate(fixedAsset.FixedAssetCode, newestTransfer));
            }
        }

        /// <summary>
        /// Kiểm tra ngày chứng từ khi thực hiện hành động cập nhật chứng từ điều chuyển tài sản.
        /// </summary>
        /// <param name="transferAsset">Chứng từ điều chuyển tài sản.</param>
        /// <param name="newestTransfer">Chứng từ điều chuyển tài sản mới nhất.</param>
        /// <param name="transferList">Danh sách chứng từ điều chuyển tài sản.</param>
        /// <param name="fixedAssetList">Danh sách tài sản cố định.</param>
        /// <param name="detailList">Danh sách chi tiết chứng từ điều chuyển tài sản.</param>
        /// <param name="listAssetIds">Danh sách ID của tài sản.</param>
        /// <exception cref="ValidateException">Ném ra nếu có lỗi kiểm tra.</exception>
        /// Created by: LB.Thành (10/09/2023)
        private void CheckUpdateTransferDate(TransferAsset transferAsset, TransferAsset newestTransfer, List<TransferAsset> transferList, List<FixedAsset> fixedAssetList, List<TransferAssetDetail> detailList, List<Guid> listAssetIds)
        {
            if(transferAsset.TransferDate < transferAsset.TransactionDate)
            {
                throw new ValidateException("Ngày điều chuyển phải lớn hơn hoặc bằng ngày chứng từ");
            }
            if (transferAsset.TransferAssetCode == newestTransfer.TransferAssetCode)
            {
                if (transferList.Count > 1 && transferAsset.TransferDate <= transferList[1].TransferDate)
                {
                    var fixedAsset = GetRelatedFixedAsset(detailList, transferList[1], fixedAssetList, listAssetIds);
                    throw new ValidateException(TransferAssetErrorMessages.InforDate(fixedAsset.FixedAssetCode, transferList[1]));
                }
            }
            else
            {
                if (transferAsset.TransferDate <= newestTransfer.TransferDate)
                {
                    var fixedAsset = GetRelatedFixedAsset(detailList, newestTransfer, fixedAssetList, listAssetIds);
                    throw new ValidateException(TransferAssetErrorMessages.InforDate(fixedAsset.FixedAssetCode, newestTransfer));
                }
            }
        }

        /// <summary>
        /// Lấy tài sản liên quan từ danh sách chi tiết chứng từ và danh sách tài sản cố định.
        /// </summary>
        /// <param name="detailList">Danh sách chi tiết chứng từ.</param>
        /// <param name="newestTransfer">Chứng từ mới nhất.</param>
        /// <param name="fixedAssetList">Danh sách tài sản cố định.</param>
        /// <param name="listAssetIds">Danh sách các ID tài sản cần xem.</param>
        /// <returns>Tài sản liên quan.</returns>
        /// Created by: Lb.Thành (10/09/2023)
        private FixedAsset GetRelatedFixedAsset(List<TransferAssetDetail> detailList, TransferAsset transfer, List<FixedAsset> fixedAssetList, List<Guid> listAssetIds)
        {
            var transferDetailIds = detailList
                .Where(detail => detail.TransferAssetId == transfer.TransferAssetId)
                .Select(detail => detail.FixedAssetId)
                .ToList();

            var relatedFixedAssets = fixedAssetList
                .Where(asset => transferDetailIds.Contains(asset.FixedAssetId))
                .OrderByDescending(asset => asset.ModifiedDate)
                .ToList();

            return relatedFixedAssets.FirstOrDefault(asset => listAssetIds.Contains(asset.FixedAssetId));
        }

        public Task CheckExistTransferAsync(List<Guid> assetIds, ActionMode action)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
