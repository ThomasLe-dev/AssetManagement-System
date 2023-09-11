using MISA.WebFresher052023.HCSN.Domain.Enum;
using System;

namespace MISA.WebFresher052023.HCSN.Application.DTO.Receiver
{
    public class ReceiverFlagDto
    {
        public ReceiverDto? ReceiverDto { get; set; }
            
        public ActionMode ActionMode { get; set; }
    }
}
