using MISA.WebFresher052023.HCSN.Domain.Enum;
using MISA.WebFresher052023.HCSN.Domain.Interface;
using MISA.WebFresher052023.HCSN.Domain.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher052023.HCSN.Domain
{
    public class FixedAssetManager : IFixedAssetManager
    {
        #region Fields
        private readonly IFixedAssetRepository _assetRepository;
        private readonly ITransferAssetRepository _transferAssetRepository;
        #endregion

        #region Constructor
        public FixedAssetManager(IFixedAssetRepository assetRepository, ITransferAssetRepository transferAssetRepository)
        {
            _assetRepository = assetRepository;
            _transferAssetRepository = transferAssetRepository;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Kiểm tra trùng mã
        /// </summary>
        /// <param name="code">code của bản ghi</param>
        /// <returns></returns>
        /// <exception cref="ConflictException">message</exception>
        /// Created by: LB.Thành (19/07/2023)
        public async Task CheckExistedEntityByCode(string code, string? oldCode = null)
        {
            var assetExist = await _assetRepository.FindByCodeAsync(code);

            if (assetExist != null && assetExist.FixedAssetCode != oldCode)
            {
                throw new ConflictException("Mã tài sản không được phép trùng lặp");
            }
        }

        /// <summary>
        /// Kiểm tra tài sản có phát sinh chứng từ không
        /// </summary>
        /// <param name="assetIds">Danh sách id tài sản</param>
        /// <param name="action">Hành động như xóa hay cập nhật</param>
        /// <returns>Ném ra 1 ngoại lệ nếu có chứng từ phát sinh</returns>
        /// Created by: ldtuan (17/09/2023)
        public async Task CheckExistTransferAsync(List<Guid> assetIds, ActionMode action)
        {
            var transfer = await _transferAssetRepository.GetAllTransferAssetByAssetId(assetIds);
            foreach (var assetId in assetIds)
            {
                var existTransfers = transfer.Where(transfer => transfer.FixedAssetId == assetId).ToList();
                if (existTransfers.Any())
                {
                    var asset = await _assetRepository.GetAsync(assetId);

                    switch (action)
                    {
                        case ActionMode.DELETE:
                            throw new ValidateException(TransferAssetErrorMessages.AriseAssetDelete(asset.FixedAssetCode), TransferAssetErrorMessages.Infor(existTransfers));
                        case ActionMode.UPDATE:
                            throw new ValidateException(TransferAssetErrorMessages.AriseAssetUpdate(asset.FixedAssetCode), TransferAssetErrorMessages.Infor(existTransfers));
                    }
                }
            }
        }
        #endregion
    }
}
