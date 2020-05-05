using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FluentValidation.Attributes;

namespace ODMSModel.SparePartBarcode
{
    [Validator(typeof(SparePartBarcodeIndexViewModelValidator))]
    public class SparePartBarcodeIndexViewModel : ModelBase
    {
        public SparePartBarcodeIndexViewModel()
        {
        }


        public int ClaimDismantledPartId { get; set; }

        [Display(Name = "SparePart_Display_PartName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int PartId { get; set; }

        [Display(Name = "SparePart_Display_Barcode", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string Barcode { get; set; }

        [Display(Name = "SparePart_Display_Barcode", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string EncodedBarcodeImage { get; set; }

        public List<string> CodeList { get; set; }

        [Display(Name = "SparePart_Display_WorkOrderDetail", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int WorkOrderDetail { get; set; }

        [Display(Name = "SparePart_Display_Quantity", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public decimal Quantity { get; set; }

        [Display(Name = "SparePart_Display_WorkOrderId", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int WorkOrderId { get; set; }

        [Display(Name = "SparePart_Display_VinNo", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string VinNo { get; set; }

        [Display(Name = "SparePart_Display_WarrantlyStartDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime WarrantlyStartDate { get; set; }

        [Display(Name = "SparePart_Display_WorkOrderDate", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime WorkOrderDate { get; set; }

        [Display(Name = "SparePart_Display_Barcode_PartCode", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string PartCode { get; set; }

        [Display(Name = "SparePart_Display_Barcode_PartName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string PartName { get; set; }

        [Display(Name = "SparePart_Display_VehicleKm", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string VehicleKm { get; set; }

        [Display(Name = "SparePart_Display_DealerName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string DealerName { get; set; }

        [Display(Name = "SparePart_Display_ShipQuantity", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int ShipQuantity { get; set; }
        [Display(Name = "SparePart_Display_GifNo", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string GifNo { get; set; }

    }
}
