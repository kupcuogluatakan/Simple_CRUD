using System.ComponentModel.DataAnnotations;
using FluentValidation.Attributes;
using ODMSCommon.Resources;

namespace ODMSModel.PurchaseOrderMatch
{
    [Validator(typeof(PurchaseOrderMatchViewModelValidator))]
    public class PurchaseOrderMatchViewModel : ModelBase
    {
        [Display(Name = "PurchaseOrderMatch_Display_Purhcase_Order_Group_Name", ResourceType = typeof(MessageResource))]
        public int? PurhcaseOrderGroupId { get; set; }

        [Display(Name = "PurchaseOrderMatch_Display_Purhcase_Order_Type_Name", ResourceType = typeof(MessageResource))]
        public int? PurhcaseOrderTypeId { get; set; }

        [Display(Name = "PurchaseOrderMatch_Display_Sales_Organization", ResourceType = typeof(MessageResource))]
        public string SalesOrganization { get; set; }

        [Display(Name = "PurchaseOrderMatch_Display_DistrChan", ResourceType = typeof(MessageResource))]    
        public string DistrChan { get; set; }

        [Display(Name = "PurchaseOrderMatch_Display_Division", ResourceType = typeof(MessageResource))]
        public string Division { get; set; }


        [Display(Name = "PurchaseOrderMatch_Display_Purhcase_Order_Group_Name", ResourceType = typeof(MessageResource))]
        public string PurhcaseOrderGroupName { get; set; }

        [Display(Name = "PurchaseOrderMatch_Display_Purhcase_Order_Type_Name", ResourceType = typeof(MessageResource))]
        public string PurhcaseOrderTypeName { get; set; }

    }
}
