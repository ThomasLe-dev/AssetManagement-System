using MISA.WebFresher052023.HCSN.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher052023.HCSN.Domain
{
    public class FixedAssetManager : IEntityManager
    {
        #region Fields
        private readonly IFixedAssetRepository _assetRepository;
        #endregion

        #region Constructor
        public FixedAssetManager(IFixedAssetRepository assetRepository)
        {
            _assetRepository = assetRepository;
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
        #endregion
    }
}
