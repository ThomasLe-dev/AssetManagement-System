using MISA.WebFresher052023.HCSN.Domain.Entity;
using MISA.WebFresher052023.HCSN.Domain.Model.Transfer_Asset_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher052023.HCSN.Domain.Resource
{
    public class TransferAssetErrorMessages
    {
        public static string Arise(string fixedAssetCode)
        {
            return $"Tài sản <strong>{fixedAssetCode}</strong> đã có phát sinh chứng từ. Bạn không thể xóa chứng từ này";
        }

        public static string AriseUpdate(string fixedAssetCode)
        {
            return $"Tài sản <strong>{fixedAssetCode}</strong> đã có phát sinh chứng từ. Bạn không thể cập nhật chứng từ này";
        }
        public static string AriseAssetDelete(string fixedAssetCode)
        {
            return $"Tài sản <strong>{fixedAssetCode}</strong> đã có phát sinh chứng từ. Bạn không thể xóa tài sản này";
        }
        public static string AriseAssetUpdate(string fixedAssetCode)
        {
            return $"Tài sản <strong>{fixedAssetCode}</strong> đã có phát sinh chứng từ. Bạn không thể cập nhật tài sản này";
        }

        public static string Infor(List<TransferAssetDeleteModel> transfers)
        {
            var transferMessages = transfers.Select(transfer =>
                $"- Chứng từ điều chuyển <strong>{transfer.TransferAssetCode} ({transfer.TransferDate:dd/MM/yyyy})</strong>");

            return string.Join(", ", transferMessages);
        }

        public static string TransferSmallTransaction => "Ngày điều chuyển phải lớn hơn hoặc bằng ngày chứng từ";

        public static string InforDate(string fixedAssetCode, TransferAsset transferAsset)
        {
            return $"Tài sản <strong>{fixedAssetCode}</strong> đã có phát sinh chứng từ. Ngày điều chuyển phải " +
                $"lớn hơn của chứng từ <strong>{transferAsset.TransferAssetCode} ({transferAsset.TransferDate:dd/MM/yyyy})</strong>";
        }
    }

}
