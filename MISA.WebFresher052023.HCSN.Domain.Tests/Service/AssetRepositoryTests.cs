using MISA.WebFresher052023.HCSN.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher052023.HCSN.Domain.Tests.Service
{
    public class AssetRepositoryTests : IFixedAssetRepository
    {
        public int TotalCall { get; set; }
        public Task DeleteAsync(FixedAsset entity)
        {
            throw new NotImplementedException();
        }

        public Task<FixedAsset?> FindAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<FixedAsset?> FindByCodeAsync(string code)
        {
            TotalCall++;
            return Task.FromResult<FixedAsset?>(default);
        }

        public Task<IEnumerable<FixedAsset>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<FixedAsset> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task InsertAsync(FixedAsset entity)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Guid id, FixedAsset entity)
        {
            throw new NotImplementedException();
        }
    }
}
