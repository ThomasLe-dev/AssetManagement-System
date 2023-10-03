using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher052023.HCSN.Application.DTO.AssetDto
{
    public class FixedAssetForTransferDto
    {
        public int? pageNumber { get; set; }
        public int? pageLimit { get; set; }
        public List<FixedAssetDto>? FixedAssetDtos { get; set; }
        public List<Guid> transferAssetDetailIds { get; set; }
    }
}
