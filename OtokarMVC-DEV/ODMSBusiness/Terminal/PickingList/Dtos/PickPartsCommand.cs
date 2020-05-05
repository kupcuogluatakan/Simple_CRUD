using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ODMSBusiness.Terminal.Common;

namespace ODMSBusiness.Terminal.PickingList.Dtos
{
    public class PickPartsCommand:ICommand
    {
        public int DealerId { get; }
        public int UserId { get; }
        public string LanguageCode { get; }
        public PickingDetailInfo Model { get; }
        public PickPartsCommand(PickingDetailInfo model,int dealerId,int userId,string languageCode)
        {
            DealerId = dealerId;
            UserId = userId;
            LanguageCode = languageCode;
            Model = model;
        }
    }
}
