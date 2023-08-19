using AutoMapper;
using MISA.WebFresher052023.HCSN.Application.DTO;
using MISA.WebFresher052023.HCSN.Application.Interface.Base;
using MISA.WebFresher052023.HCSN.Domain.Interface.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher052023.HCSN.Application.Service.Base
{
    public abstract class BaseReadOnlyService<TEntity, TEntityDto> : IReadOnlyService<TEntityDto>
    {
        #region Fields
        protected readonly IReadOnlyRepository<TEntity> _readOnlyRepository;
        protected readonly IMapper _mapper;
        #endregion

        #region Constructor
        protected BaseReadOnlyService(IReadOnlyRepository<TEntity> readOnlyRepository, IMapper mapper)
        {
            _readOnlyRepository = readOnlyRepository;
            _mapper = mapper;
        } 
        #endregion

        /// <summary>
        /// Hàm lấy tất cả các bản ghi
        /// </summary>
        /// <returns>Tất cả các bản ghi</returns>
        /// Created by: LB.Thành (16/07/2023)
        public async Task<IEnumerable<TEntityDto>> GetAllAsync()
        {
            var entities = await _readOnlyRepository.GetAllAsync();

            var result = _mapper.Map<IEnumerable<TEntityDto>>(entities);
            return result;
        }

        /// <summary>
        /// Hàm lấy 1 bản ghi theo Id
        /// </summary>
        /// <param name="id">Id của bản ghi</param>
        /// <returns>1 bản ghi thỏa mãn</returns>
        /// Created by: LB.Thành (16/07/2023)
        public async Task<TEntityDto> GetAsync(Guid id)
        {
            var entity = await _readOnlyRepository.GetAsync(id);
            var result = _mapper.Map<TEntityDto>(entity);

            return (TEntityDto)result;
        }
    }
}
