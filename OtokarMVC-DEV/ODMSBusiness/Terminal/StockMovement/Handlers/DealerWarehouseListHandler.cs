using System.Linq;
using ODMSBusiness.Terminal.Common;
using ODMSBusiness.Terminal.StockMovement.Dtos;
using ODMSData.Utility;
using ODMSModel.Warehouse;
using ODMSCommon.Security;

namespace ODMSBusiness.Terminal.StockMovement.Handlers
{
    public class DealerWarehouseListHandler :
        IHandleRequest<DealerWareHouseListRequest, EnumerableResponse<DealerWareHouseListItem>>
    {
        private readonly WarehouseBL _bus;
        private readonly DbHelper _dbHelper;

        public DealerWarehouseListHandler(WarehouseBL bus, DbHelper dbHelper)
        {
            _bus = bus;
            _dbHelper = dbHelper;
        }

        public EnumerableResponse<DealerWareHouseListItem> Handle(DealerWareHouseListRequest request)
        {
            int total;
            var list =
                _bus.ListWarehouses(UserManager.UserInfo,
                    new WarehouseListModel {DealerId = request.DealerId, IsActive = true, SortColumn = "Name"},
                    out total).Data.Where(c=>c.IsActive);
            var warehouses =
                new EnumerableResponse<DealerWareHouseListItem>(
                    list.Select(c => new DealerWareHouseListItem(c.Id, c.Name,c.Code)).ToList());
            if (request.IncludeRacks && warehouses.Any())
                AddWarehouseRacks(warehouses, request.DealerId);
            return warehouses;
        }

        private void AddWarehouseRacks(EnumerableResponse<DealerWareHouseListItem> wareHouseList, int dealerId)
        {
            var handler = new DealerRackListHandler(_dbHelper);
            foreach (var wareHouseListItem in wareHouseList)
            {
                var dealerRackListRequest = new DealerRackListRequest(dealerId, wareHouseListItem.WarehouseId);
                var racks = handler.Handle(dealerRackListRequest);
                wareHouseListItem.AddRacks(racks);
            }
        }
    }
}