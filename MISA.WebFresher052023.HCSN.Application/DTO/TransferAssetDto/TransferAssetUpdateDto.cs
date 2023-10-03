using MISA.WebFresher052023.HCSN.Application.DTO.Receiver;
using MISA.WebFresher052023.HCSN.Application.DTO.TransferAssetDetail;
using MISA.WebFresher052023.HCSN.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher052023.HCSN.Application.DTO.TransferAssetDto
{
    public class TransferAssetUpdateDto
    {
        // dữ liệu của chứng từ
        public TransferAsset? TransferAsset { get; set; }

        // danh sách dữ liệu của tài sản thuộc chứng từ
        public List<TransferAssetDetailUpdateDto>? ListTransferAssetDetail { get; set; }

        // danh sách dữ liệu của ban giao nhận
        public List<ReceiverUpdateDto>? ListReceiver { get; set; }
    }
}
