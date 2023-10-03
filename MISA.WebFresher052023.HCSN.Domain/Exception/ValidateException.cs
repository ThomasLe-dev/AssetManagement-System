using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher052023.HCSN.Domain
{
    public class ValidateException : Exception
    {
        #region Fields
        public int ErrorCode { get; set; }
        public string? MoreInfo { get; set; }
        #endregion
        #region Constructor
        public ValidateException() { }
        public ValidateException(int errorCode)
        {
            ErrorCode = errorCode;
        }
        #endregion

        #region Methods
        public ValidateException(string message) : base(message)
        {

        }
        public ValidateException(string message, string moreInfo) : base(message)
        {
            MoreInfo = moreInfo;
        }
        public ValidateException(string message, int errorCode) : base(message)
        {
            ErrorCode = errorCode;
        }
        #endregion
    }
}
