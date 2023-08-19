using AutoMapper;
using MISA.WebFresher052023.HCSN.Application.DTO.AssetTypeDto;
using MISA.WebFresher052023.HCSN.Application.Interface;
using MISA.WebFresher052023.HCSN.Domain.Entity;
using MISA.WebFresher052023.HCSN.Domain.Interface;
using MISA.WebFresher052023.HCSN.Domain.Interface.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher052023.HCSN.Application.Service
{
    public class FixedAssetCategoryService : BaseService<FixedAssetCategory, FixedAssetCategoryDto, FixedAssetCategoryCreateDto, FixedAssetCategoryUpdateDto>, Interface.IFixedAssetCategoryService
    {
        private readonly Domain.Interface.IFixedAssetCategory _assetTypeRepository;
        public FixedAssetCategoryService(Domain.Interface.IFixedAssetCategory assetTypeRepository, IMapper mapper, IUnitOfWork unitOfWork) : base(assetTypeRepository, mapper, unitOfWork)
        {
            _assetTypeRepository = assetTypeRepository;
        }

        public override Task<FixedAssetCategory> MapCreateDtoToEntity(FixedAssetCategoryCreateDto entityCreateDto)
        {
            throw new NotImplementedException();
        }

        public override Task<FixedAssetCategory> MapUpdateDtoToEntity(Guid id, FixedAssetCategoryUpdateDto entityUpdateDto)
        {
            throw new NotImplementedException();
        }
    }
}
