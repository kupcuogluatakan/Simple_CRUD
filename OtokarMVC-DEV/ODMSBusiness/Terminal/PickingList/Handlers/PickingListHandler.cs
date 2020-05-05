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
    public class PickingListHandler:IHandleRequest<PickingListRequest,EnumerableResponse<PickingListItem>>
    {
        private readonly DbHelper _dbHelper;

        public PickingListHandler(DbHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public EnumerableResponse<PickingListItem> Handle(PickingListRequest request)
        {
            var list = _dbHelper.ExecuteListReader<PickingListItem>("P_TERMINAL_LIST_PICKING",
                request.DealerId,
                request.PagingInfo.CurrentPage,
                request.PagingInfo.PageSize,
                0
                );
            request.PagingInfo.Total = int.Parse(_dbHelper.GetOutputValue("TOTAL").ToString());
            return new EnumerableResponse<PickingListItem>(list, request.PagingInfo);
        }
    }
}
