using AutoMapper;
using MISA.WebFresher052023.HCSN.Application.DTO;
using MISA.WebFresher052023.HCSN.Application.DTO.AssetDTO;
using MISA.WebFresher052023.HCSN.Application.Interface;
using MISA.WebFresher052023.HCSN.Application.Response;
using MISA.WebFresher052023.HCSN.Domain;
using MISA.WebFresher052023.HCSN.Domain.Interface;
using MISA.WebFresher052023.HCSN.Domain.Model;
using MISA.WebFresher052023.HCSN.Domain.Model.Fixed_Asset_Model;
using MISA.WebFresher052023.HCSN.Domain.Model.Transfer_Asset_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher052023.HCSN.Application.Service
{
    public class FixedAssetService : BaseService<FixedAsset, FixedAssetDto, FixedAssetCreateDto, FixedAssetUpdateDto>, IFixedAssetService
    {
        #region Fields
        private readonly IFixedAssetRepository _assetRepository;
        private readonly IEntityManager _entityManager;
        #endregion

        #region Constructors
        public FixedAssetService(IFixedAssetRepository assetRepository, IMapper mapper, IEntityManager entityManager, IUnitOfWork unitOfWork) : base(assetRepository, mapper, unitOfWork)
        {
            _assetRepository = assetRepository;
            _entityManager = entityManager;
        }

        #endregion

        #region Methods
        /// <summary>
        /// Ánh xạ dữ liệu từ AssetCreateDto vào đối tượng FixedAsset để tạo mới tài sản.
        /// </summary>
        /// <param name="entityCreateDto">Dữ liệu chứa thông tin tạo mới cho tài sản.</param>
        /// <returns>Đối tượng FixedAsset đã được tạo mới.</returns>
        /// Created by: LB.Thành (19/07/2023)
        public override async Task<FixedAsset> MapCreateDtoToEntity(FixedAssetCreateDto entityCreateDto)
        {
            // Thực hiện kiểm tra nghiệp vụ trước khi tạo mới đối tượng Asset.
            // Kiểm tra xem có tài sản nào tồn tại với mã tài sản đã được cung cấp (entityCreateDto.FixedAssetCode) chưa.
            await _entityManager.CheckExistedEntityByCode(entityCreateDto.FixedAssetCode);

            // Khởi tạo một đối tượng Asset mới để đưa dữ liệu từ AssetCreateDto vào.
            var asset = _mapper.Map<FixedAsset>(entityCreateDto);

            // Trả về đối tượng Asset đã được khởi tạo và sẵn sàng để lưu vào cơ sở dữ liệu.
            return asset;
        }


        /// <summary>
        /// Ánh xạ dữ liệu cập nhật từ AssetUpdateDto vào đối tượng FixedAsset để thực hiện việc cập nhật.
        /// </summary>
        /// <param name="id">ID của tài sản cần cập nhật.</param>
        /// <param name="entityUpdateDto">Dữ liệu chứa thông tin cập nhật cho tài sản.</param>
        /// <returns>Đối tượng FixedAsset đã được cập nhật.</returns>
        /// Created by: LB.Thành (19/07/2023)
        public override async Task<FixedAsset> MapUpdateDtoToEntity(Guid id, FixedAssetUpdateDto entityUpdateDto)
        {
            // Lấy thông tin tài sản hiện tại từ cơ sở dữ liệu dựa trên ID được cung cấp.
            var oldAsset = await _baseRepository.GetAsync(id);

            // Thực hiện kiểm tra nghiệp vụ trước khi cập nhật đối tượng Asset.
            await _entityManager.CheckExistedEntityByCode(entityUpdateDto.FixedAssetCode, oldAsset.FixedAssetCode);

            // Khởi tạo một đối tượng Asset mới để đưa dữ liệu từ AssetUpdateDto vào.
            var asset = _mapper.Map<FixedAsset>(entityUpdateDto);

            // Trả về đối tượng Asset đã được khởi tạo và sẵn sàng để cập nhật thông tin vào cơ sở dữ liệu.
            return asset;
        }

        /// <summary>
        /// Lấy ra danh sách tài sản theo điều kiện lọc, phân trang và trả về tổng số bản ghi
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>danh sách tài sản hợp lệ và tổng số bản ghi</returns>
        /// Created by: LB.Thành (08/08/2023)
        public async Task<FixedAssetPagingDto> GetFilterPagingAsset(FixedAssetFilterDto dto)
        {
            var fixedAssetFilterEntity = _mapper.Map<FixedAssetFilterModel>(dto);

            var fixedAssetPagingEntity = await _assetRepository.GetFilterPagingAsset(fixedAssetFilterEntity);

            var fixedAssetPagingDto = _mapper.Map<FixedAssetPagingDto>(fixedAssetPagingEntity);
            return fixedAssetPagingDto;
        }
        /// <summary>
        /// Lấy FixedAssetCode mới
        /// </summary>
        /// <returns>FixedAssetCode mới</returns>
        /// Created by: LB.Thành (1/08/2023)
        public async Task<string> GetNewFixedAssetCodeAsync()
        {
            var fixedAssetCode = await _assetRepository.GetNewFixedAssetCode();
            return fixedAssetCode;
        }

        /// <summary>
        /// Xuất danh sách tài sản ra tệp Excel bất đồng bộ.
        /// </summary>
        /// <param name="fixedAssetIds">Danh sách ID tài sản để xuất ra Excel.</param>
        /// <returns>Mảng byte chứa dữ liệu của tệp Excel.</returns>
        /// Created by: LB.Thành (06/08/2023)
        public async Task<byte[]> ExportFixedAssetListToExcelAsync(List<Guid> fixedAssetIds)
        {
            if (fixedAssetIds.Count == 0)
            {
                throw new Exception("Không được truyền danh sách rỗng");
            }

            // Tìm kiếm danh sách tài sản theo Id
            var fixedAssetEntities = await _assetRepository.GetListByIdsAsync(fixedAssetIds);

            if (!fixedAssetEntities.Any())
            {
                throw new NotFoundException("Không tìm thấy tài sản");
            }

            if (fixedAssetIds.Count > fixedAssetEntities.Count())
            {
                throw new NotFoundException("Có tài sản không tồn tại");
            }

            // Chuyển sang model
            var fixedAssetExcelModels = _mapper.Map<IEnumerable<FixedAssetExcelModel>>(fixedAssetEntities);

            // Xuất ra file excel
            var contentFile = _assetRepository.ExportFixedAssetListToExcel(fixedAssetExcelModels);

            return contentFile;
        }

        /// <summary>
        /// Lấy danh sách tài sản có loại những bản ghi đã chọn để hiện thị trên form chọn tài sản cho chứng từ
        /// </summary>
        /// <param name="pageNumber">Số trang</param>
        /// <param name="pageLimit">Số lượng tối đa bản ghi mỗi trang</param>
        /// <param name="dtos">Danh sách truyền vào để loại những bản ghi đó ra</param>
        /// <returns>Danh sách loại tài sản đáp ứng đúng các điều kiện trên</returns>
        /// Created by: LB.Thành (09/09/2023)
        public async Task<BaseFilterResponse<FixedAssetDto>> FilterFixedAssetForTransfer(int? pageNumber, int? pageLimit, List<FixedAssetDto> dtos)
        {
            string ids = "";
            if (dtos != null && dtos.Count > 0)
            {
                ids = string.Join(",", dtos.Select(asset => asset.FixedAssetId));
            }

            List<FixedAssetModel> entities;
            pageNumber = pageNumber.HasValue && pageNumber.Value > 0 ? pageNumber : 1;
            pageLimit = pageLimit.HasValue && pageLimit.Value > 0 ? pageLimit : 20;

            var model = await _assetRepository.FilterFixedAssetForTransfer(pageNumber, pageLimit, ids);
            int totalRecords = model.TotalRecords;
            int totalPages = Convert.ToInt32(Math.Ceiling((double)totalRecords / (double)pageLimit));
            entities = model.FixedAssetModels;

            List<FixedAssetDto> entitiesDto = _mapper.Map<List<FixedAssetDto>>(entities);
            var filterData = new BaseFilterResponse<FixedAssetDto>(totalPages, totalRecords, entitiesDto);

            return filterData;
        }

        #endregion

    }
}
