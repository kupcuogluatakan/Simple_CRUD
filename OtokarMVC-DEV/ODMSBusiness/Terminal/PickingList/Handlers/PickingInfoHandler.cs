using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ODMSBusiness.Terminal.Common;
using ODMSBusiness.Terminal.PickingList.Dtos;
using ODMSCommon.Exception;
using ODMSCommon.Resources;
using ODMSData.Utility;

namespace ODMSBusiness.Terminal.PickingList.Handlers
{
   public  class PickingInfoHandler:IHandleRequest<PickingInfoRequest,PickingInfo>
   {
       private readonly DbHelper _dbHelper;

       public PickingInfoHandler(DbHelper dbHelper)
       {
           _dbHelper = dbHelper;
       }

       public PickingInfo Handle(PickingInfoRequest request)
       {
           var pickingInfo = _dbHelper.ExecuteReader<PickingInfo>("P_TERMINAL_GET_PICKING_INFO",
               request.PickingId,
               request.DealerId,
               request.LanguageCode
               );
            if(pickingInfo.PickingId==0)
                throw  new ODMSException(MessageResource.Exception_PickingFound);
           return pickingInfo;
       }

    }
}
