using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ODMSTerminal.Models
{
    public class WarehouseSearchModel
    {
        public int WarehouseId { get; set; }

        public string WarehouseCode { get; set; }

        public int RackId { get; set; }

        public string RackCode { get; set; }

        public long PartId { get; set; }

        public string PartCode { get; set; }

        public int TargetWarehouseId { get; set; }

        public string TargetWarehouseCode { get; set; }

        public int  TargetRackId { get; set; }

        public string TargetRackCode { get; set; }

        public int StockTypeId { get; set; }

        public decimal TargetQuantity { get; set; }

        public decimal? Quantity { get; set; }

        public string Unit { get; set; }

        public string Description { get; set; }
    }
}