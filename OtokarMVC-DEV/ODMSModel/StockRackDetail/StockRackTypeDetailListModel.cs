﻿using Kendo.Mvc.UI;
using ODMSModel.ListModel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ODMSModel.StockRackDetail
{
    public class StockRackTypeDetailListModel : BaseListWithPagingModel
    {
        public StockRackTypeDetailListModel([DataSourceRequest] DataSourceRequest request)
            : base(request)
        {
            var dMapper = new Dictionary<string, string>
                {
                    {"RackName","RACK_NAME"},
                    {"WarehouseName", "WAREHOUSE_NAME"},
                    {"StockTypeName", "STOCK_TYPE_NAME"},
                    {"Quantity", "QUANTITY"}
                };
            SetMapper(dMapper);
        }

        public StockRackTypeDetailListModel()
        { 
        }
        //IdDealer
        [Display(Name = "StockRackDetail_Display_IdDealer", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int? IdDealer { get; set; }

        //RackName
        public int? RackId { get; set; }
        [Display(Name = "StockRackDetail_Display_RackName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string RackName { get; set; }

        //WarehouseName
        public int? WarehouseId { get; set; }
        [Display(Name = "StockRackDetail_Display_WarehouseName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string WarehouseName { get; set; }

        //PartName
        public int? PartId { get; set; }
        [Display(Name = "SparePart_Display_PartCode", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string PartCode { get; set; }
        [Display(Name = "StockRackDetail_Display_PartName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string PartName { get; set; }

        //Quantity
        [Display(Name = "StockRackDetail_Display_Quantity", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal Quantity { get; set; }

        //IdStockType
        [Display(Name = "StockTypeDetail_Display_IdStockType", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int? IdStockType { get; set; }

        [Display(Name = "StockTypeDetail_Display_IdStockType", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string StockTypeName { get; set; }
    }
}
