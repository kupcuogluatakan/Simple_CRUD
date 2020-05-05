using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ODMSBusiness.Terminal.Common;
using ODMSBusiness.Terminal.PickingList.Dtos;
using ODMSData.Utility;

namespace ODMSBusiness.Terminal.PickingList.Handlers
{
    public class PickingPartsListHandler:IHandleRequest<PickingPartsRequest,EnumerableResponse<PickingPartsListItem>>
    {
        private readonly DbHelper _helper;

        public PickingPartsListHandler(DbHelper helper)
        {
            _helper = helper;
        }

        public EnumerableResponse<PickingPartsListItem> Handle(PickingPartsRequest request)
        {
            var list = _helper.ExecuteListReader<PickingPartsListItem>("P_TERMINAL_LIST_PICKING_PARTS", 
                request.PickingId,
                Utility.MakeDbNull(request.PartCode),
                request.DealerId,
                request.LanguageCode
                );
            return new EnumerableResponse<PickingPartsListItem>(list);
        }

    }
}
