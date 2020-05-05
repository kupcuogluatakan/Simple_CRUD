using Kendo.Mvc.UI;
using ODMSModel.ListModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ODMSModel.FixAssetInventoryOutput
{
    public class FixAssetInventoryOutputListModel : BaseListWithPagingModel
    {
        public FixAssetInventoryOutputListModel([DataSourceRequest] DataSourceRequest request)
            : base(request)
        {
            var dMapper = new Dictionary<string, string>
                {
                    {"FixAssetName","FIX_ASSET_NAME"},
                    {"SerialNo","SERIAL_NO"},
                    {"ExitDesc","EXIT_DESC"},
                    {"CreateDate","CREATE_DATE"},
                    {"StockType","STOCK_TYPE"},
                    {"ExitDate","EXIT_DATE"}
                };
            SetMapper(dMapper);
        }

        public FixAssetInventoryOutputListModel()
        {
        }

        public int IdFixAssetInventory { get; set; }

        [Display(Name = "FixAssetInventoryOutput_Display_FixAssetName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string FixAssetName { get; set; }

        [Display(Name = "FixAssetInventoryOutput_Display_SerialNo", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string SerialNo { get; set; }

        [Display(Name = "FixAssetInventoryOutput_Display_ExitDesc", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string ExitDesc { get; set; }

        [Display(Name = "FixAssetInventoryOutput_Display_CreateDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime? CreateDate { get; set; }

        [Display(Name = "FixAssetInventoryOutput_Display_StockType", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int? StockType { get; set; }

        [Display(Name = "FixAssetInventoryOutput_Display_StockType", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string StockTypeName { get; set; }

        [Display(Name = "FixAssetInventoryOutput_Display_ExitDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public DateTime? ExitDate { get; set; }

        [Display(Name = "FixAssetInventoryOutput_Display_FixAssetStatus", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int? FixAssetStatus { get; set; }
        public string FixAssetStatusName { get; set; }
    }
}
