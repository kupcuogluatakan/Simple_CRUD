using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Kendo.Mvc.UI;
using ODMSModel.ListModel;

namespace ODMSModel.CustomerContact
{

    public class CustomerContactListModel : BaseListWithPagingModel
    {
        public CustomerContactListModel([DataSourceRequest] DataSourceRequest request)
            : base(request)
        {
            var dMapper = new Dictionary<string, string>
                {
                    {"CustomerId", "ID_CUSTOMER"},
                    {"ContactTypeName", "CUST_CONTTYPE_LOOKVAL"},
                    {"Name", "NAME"},
                    {"Surname", "SURNAME"},
                    {"ContactTypeValue", "CONTACT_TYPE_VALUE"}
                };
            SetMapper(dMapper);
        }

        public CustomerContactListModel()
        {
        }

        public int ContactId { get; set; }

        //CustomerId
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
