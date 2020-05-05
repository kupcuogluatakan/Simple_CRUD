using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ODMSBusiness.Terminal.Common;
using ODMSBusiness.Terminal.DeliveryGoodsPlacement.Dtos;
using ODMSCommon.Exception;
using ODMSData.Utility;

namespace ODMSBusiness.Terminal.DeliveryGoodsPlacement.Handlers
{
    public class DeliveryPlacePartCommandHandler:IHandleCommand<DeliveryPlacePartCommand>
    {
        private readonly DbHelper _dbHelper;

        public DeliveryPlacePartCommandHandler(DbHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public void Handle(DeliveryPlacePartCommand command)
        {
            _dbHelper.ExecuteNonQuery("P_DML_DELIVERY_GOODS_PLACEMENT_PARTS",
                null,
                command.Quantity,
                command.RackId,
                command.DeliverySeqNo,
                "I",
                command.UserId,
                null, null
                );
            var errorNo = Convert.ToInt32(_dbHelper.GetOutputValue("ERROR_NO"));
            if (errorNo > 0)
                throw new ODMSException(Utility.ResolveDatabaseErrorXml(_dbHelper.GetOutputValue("ERROR_DESC").ToString()));
        }
    }
}
