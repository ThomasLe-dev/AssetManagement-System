using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher052023.HCSN.Domain.Model.Fixed_Asset_Model
{
    public class FixedAssetForTransferModel
    {
        public int TotalRecords { get; set; }
        public List<FixedAssetModel> FixedAssetModels { get; set; }
    }
}
