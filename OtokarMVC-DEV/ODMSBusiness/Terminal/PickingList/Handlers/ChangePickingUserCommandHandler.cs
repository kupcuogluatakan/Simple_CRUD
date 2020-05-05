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
   public  class ChangePickingUserCommandHandler:IHandleCommand<ChangePickingUserCommand>
   {
       private readonly DbHelper _dbHelper;

       public ChangePickingUserCommandHandler(DbHelper dbHelper)
       {
           _dbHelper = dbHelper;
       }

       public void Handle(ChangePickingUserCommand command)
       {
          var result= _dbHelper.ExecuteNonQuery("P_TERMINAL_CHANGE_PICKING_USER",
               command.PickingId,
               command.DealerId,
               command.UserId
               );
           if (result != 1)
           {
               throw new ODMSException(MessageResource.Error_DB_NoRecordFound);
           }
       }
   }
}
