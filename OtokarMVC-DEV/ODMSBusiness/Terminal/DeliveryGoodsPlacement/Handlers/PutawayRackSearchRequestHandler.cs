using ODMSBusiness.Terminal.Common;
using ODMSBusiness.Terminal.DeliveryGoodsPlacement.Dtos;
using ODMSCommon.Exception;
using ODMSCommon.Resources;
using ODMSData.Utility;

namespace ODMSBusiness.Terminal.DeliveryGoodsPlacement.Handlers
{
    public class PutawayRackSearchRequestHandler:IHandleRequest<PutawayRackSearchRequest, PutawayRackSearchResponse>
    {
        private readonly DbHelper _dbHelper;

        public PutawayRackSearchRequestHandler(DbHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public PutawayRackSearchResponse Handle(PutawayRackSearchRequest request)
        {
            var response =
                _dbHelper.ExecuteReader<PutawayRackSearchResponse>("P_TERMINAL_SEARCH_RACK_FOR_PUTAWAY",
                    request.RackCode,
                    request.WarehouseId,
                    request.PartId,
                    request.DealerId
                    );
            if(response==null || response.RackId==0)
                throw new ODMSException(MessageResource.Exception_RackNotFound);
            return response;
        }
    }
}
