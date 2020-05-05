using System.Collections.Generic;
using ODMSBusiness.Terminal.Common;

namespace ODMSBusiness.Terminal.StockMovement.Dtos
{
    public class DealerWareHouseListItem : IResponse
    {
        private List<DealerRackListItem> _racks;

        public DealerWareHouseListItem(int warehouseId, string warehouseName, string warehouseCode)
        {
            WarehouseCode = warehouseCode;
            WarehouseId = warehouseId;
            WarehouseName = warehouseName;
            _racks = new List<DealerRackListItem>();
        }

        public string WarehouseCode { get; }
        public int WarehouseId { get; }
        public string WarehouseName { get; }

        public IEnumerable<DealerRackListItem> Racks
        {
            get { return _racks; }
        }

        public void AddRacks(IEnumerable<DealerRackListItem> racks)
        {
            if (racks == null) return;
            _racks.AddRange(racks);
        }
    }
}