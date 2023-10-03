using MISA.WebFresher052023.HCSN.Application.DTO;
using MISA.WebFresher052023.HCSN.Application.DTO.TransferAssetDto;
using MISA.WebFresher052023.HCSN.Application.Interface.Base;
using MISA.WebFresher052023.HCSN.Domain.Entity;
using System;

namespace MISA.WebFresher052023.HCSN.Application.Interface
{
    public interface ITransferAssetService 
    {
        /// <summary>
        /// Lấy danh sách chứng từ phân trang và tổng số bản ghi
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>danh sách chứng từ, tổng số bản ghi</returns>
        /// Created by: LB.Thành (30/08/2023)
        public Task<TransferAssetPagingDto> GetFilterPagingAssetAsync(TransferAssetFilterDto dto);
        /// <summary>
        /// Thêm 1 chứng từ vào db
        /// </summary>
        /// <param name="entity">chi tiết của chứng từ mới</param>
        /// created by: LB.Thành (16/07/2023)
        Task<bool> CreateAsync(TransferAssetCreateDto entityCreateDto);

        /// <summary>
        /// Chỉnh sửa 1 chứng từ
        /// </summary>
        /// <param name="entity">chi tiết của tài cần sửa</param>
        /// created by: LB.Thành (16/07/2023)
        Task UpdateAsync(Guid id, TransferAssetUpdateDto entityUpdateDto);

        /// <summary>
        /// Xóa 1 chứng từ
        /// </summary>
        /// <param name="id">Id chứng từ cần xóa</param>
        /// created by: LB.Thành (16/07/2023)
        Task DeleteAsync(Guid id);

        /// <summary>
        /// Xóa nhiều chứng từ
        /// </summary>
        /// <param name="id">List Ids chứng từ cần xóa</param>
        /// created by: LB.Thành (16/07/2023)
        Task DeleteManyAsync(List<Guid> ids);

        /// <summary>
        /// Lấy TransferAssetCode mới
        /// </summary>
        /// <returns>FixedAssetCode mới</returns>
        /// Created by: LB.Thành (10/09/2023)
        public Task<string> GetNewTransferAssetCodeAsync();
    }
}
