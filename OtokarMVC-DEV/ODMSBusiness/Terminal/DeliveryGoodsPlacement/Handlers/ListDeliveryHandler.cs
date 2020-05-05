namespace ODMSBusiness.Terminal.DeliveryGoodsPlacement.Handlers
{
    using ODMSBusiness.Terminal.Common;
    using ODMSBusiness.Terminal.DeliveryGoodsPlacement.Dtos;
    using ODMSData.Utility;
    public class ListDeliveryHandler : IHandleRequest<DeliveryListRequest, ListResponse<DeliveryListItem>>
    {
        private readonly DbHelper _dbHelper;

        public ListDeliveryHandler(DbHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public ListResponse<DeliveryListItem> Handle(DeliveryListRequest request)
        {
            var response = new ListResponse<DeliveryListItem>();
            response.Items = _dbHelper.ExecuteListReader<DeliveryListItem>("PT_LIST_DELIVERY_GOODS_PLACEMENT",
                Utility.MakeDbNull(request.SapDeliveryNo),
                Utility.MakeDbNull(request.WaybillNo),
                request.DealerId,
                Utility.MakeDbNull(request.DeliveryId),
                request.PagingInfo.CurrentPage,
                request.PagingInfo.PageSize,
                0
                );
            request.PagingInfo.Total = int.Parse(_dbHelper.GetOutputValue("TOTAL").ToString());
            response.PageInfo = request.PagingInfo;
            return response;
        }
    }
}
