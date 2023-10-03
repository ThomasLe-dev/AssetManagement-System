using MISA.WebFresher052023.HCSN.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher052023.HCSN.Domain.Service
{
    public class TransferAssetDetailManager : ITransferAssetDetailManager
    {
        private readonly ITransferAssetDetailRepository _transferAssetDetailRepository;
        private readonly IFixedAssetRepository _fixedAssetRepository;
        public TransferAssetDetailManager(ITransferAssetDetailRepository transferAssetDetailRepository, IFixedAssetRepository fixedAssetRepository) 
        {
            _transferAssetDetailRepository = transferAssetDetailRepository;
            _fixedAssetRepository = fixedAssetRepository;
        }

        /// <summary>
        /// Kiểm tra các chi tiết chứng từ này có chứng từ phát sinh trước đó không
        /// </summary>
        /// <param name="ids">Các id của chi tiết chứng từ</param>
        /// <returns>Nếu có chứng từ phát sinh trước đó thì ném ra exception chứa các chứng từ đó</returns>
        /// Created by: LB.Thành (06/09/2023)
        public async Task CheckExistTransferBefore(List<Guid> ids, Guid transferAssetId)
        {
            // 1.Kiểm tra xem trong db có chứa các chi tiết chứng từ này hay không
            var detailEntities = await _transferAssetDetailRepository.GetListByIdsAsync(ids);
            if (ids.Count == 0 || detailEntities.Count < ids.Count)
            {
                throw new DataException();
            }

            // 2.Kiểm tra xem trong db có chứa các tài sản của chứng từ hay không
            var assetIds = detailEntities.Select(detail => detail.FixedAssetId).ToList();
            var assetEntities = await _fixedAssetRepository.GetListByIdsAsync(assetIds);
            if (assetIds.Count == 0 || assetEntities.Count < assetIds.Count)
            {
                throw new DataException();
            }

            // 3.Lấy danh sách thông tin các chi tiết chứng từ, chứng từ có chứa tài sản này, để xem chứng từ nào mới nhất
            var detailModels = await _transferAssetDetailRepository.GetListDetailByIdsAsync(assetIds);

            // 4.Xét từng id tài sản trong danh sách trên, đếm số lượng bản ghi có cùng id tài sản, nếu lớn hơn 1 là sai
            foreach (var assetId in assetIds)
            {
                // Xem tài sản cùng chứng từ hiện tại đang ở trong chi tiết chứng từ nào
                var presentDetail = detailModels.Where(detail => detail.FixedAssetId == assetId && detail.TransferAssetId == transferAssetId).FirstOrDefault();

                // 4.1.Đếm số lượng bản ghi có cùng assetId trong detailModels
                var count = detailModels.Count(detail => detail.FixedAssetId == assetId && detail.ModifiedDate >= presentDetail.ModifiedDate);

                // 4.2.Nếu số lượng lớn hơn 1, ném ra 1 exception
                if (count > 1)
                {
                    // 4.2.1.Thêm foreach ở đây để ném thông tin chứng từ ra message
                    throw new Exception($"AssetId {assetId} có nhiều hơn 1 bản ghi trong detailModels.");
                }
            }
        }
    }
}
