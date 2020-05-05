using System.ComponentModel.DataAnnotations;
using FluentValidation.Attributes;

namespace ODMSModel.CustomerContact
{
    [Validator(typeof(CustomerContactIndexViewModelValidator))]
    public class CustomerContactIndexViewModel : ModelBase
    {
        public CustomerContactIndexViewModel()
        {
        }
        public bool HideFormElements { get; set; }

        //ContactId
        public int ContactId { get; set; }

        //Customer
        public int CustomerId { get; set; }
        [Display(Name = "CustomerContact_Display_CustomerName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string CustomerName { get; set; }

        //ContactType
        public int? ContactTypeId { get; set; }
        [Display(Name = "CustomerContact_Display_ContactTypeName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string ContactTypeName { get; set; }
        
        //Name
        [Display(Name = "CustomerContact_Display_Name", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string Name { get; set; }

        //Surname
        [Display(Name = "CustomerContact_Display_Surname", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string Surname { get; set; }

        //ContactTypeValue
        [Display(Name = "CustomerContact_Display_ContactTypeValue", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string ContactTypeValue { get; set; }
    }
}
