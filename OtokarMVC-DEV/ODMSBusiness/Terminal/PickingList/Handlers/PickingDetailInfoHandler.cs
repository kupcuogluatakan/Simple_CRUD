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
    public class PickingDetailInfoHandler:IHandleRequest<PickingDetailInfoRequest, PickingDetailInfo>
    {
        private readonly DbHelper _dbHelper;

        public PickingDetailInfoHandler(DbHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public PickingDetailInfo Handle(PickingDetailInfoRequest request)
        {
            var pickingDetailInfo = _dbHelper.ExecuteReader<PickingDetailInfo>("P_TERMINAL_GET_PICKING_DETAIL_INFO",
                request.DealerId,
                request.PickingDetailId
                );
            if(pickingDetailInfo.PickingDetailId==0)
                throw  new ODMSException(MessageResource.Exception_PickingDetailNotFound);
            return pickingDetailInfo;
        }
    }
}
