using System;
using System.ComponentModel.DataAnnotations;
using FluentValidation.Attributes;
using ODMSCommon.Resources;

namespace ODMSModel.ClaimDismantledPartDelivery
{
    [Validator(typeof(ClaimDismantledPartDeliveryViewModelValidator))]
    public class ClaimDismantledPartDeliveryViewModel : ModelBase
    {
        public int ClaimWayBillId { get; set; }

        [Display(Name = "ClaimDismantledPartDelivery_Display_ClaimWayBillNo", ResourceType = typeof(MessageResource))]
        public string ClaimWayBillNo { get; set; }

        [Display(Name = "ClaimDismantledPartDelivery_Display_ClaimWayBillSerialNo", ResourceType = typeof(MessageResource))]
        public string ClaimWayBillSerialNo { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "ClaimDismantledPartDelivery_Display_ClaimWayBillDate", ResourceType = typeof(MessageResource))]
        public DateTime ClaimWayBillDate { get; set; }
    }
}
