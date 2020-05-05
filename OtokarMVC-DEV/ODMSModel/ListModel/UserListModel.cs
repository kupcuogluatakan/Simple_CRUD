using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Kendo.Mvc.UI;
using ODMSCommon.Resources;

namespace ODMSModel.ListModel
{
    public class UserListModel : BaseListWithPagingModel
    {
        public UserListModel([DataSourceRequest] DataSourceRequest request)
            : base(request)
        {
            var dMapper = new Dictionary<string, string>
                 {
                     {"UserId", "ID_DMS_USER"},
                     {"UserCode", "DMS_USER_CODE"},
                     {"DealerName", "DEALER_NAME"},
                     {"UserFirstName", "CUSTOMER_NAME"},
                     {"UserLastName", "USER_LAST_NAME"},
                     //{"TCIdentityNo", "TC_IDENTITY_NO"},
                     {"IdentityNo","IDENTITY_NO" },
                     {"EMail", "EMAIL"},
                     {"IsActiveName", "IS_ACTIVE"},
                     {"RoleTypeName", "ROLE_TYPE_NAME"}
                 };
            SetMapper(dMapper);

        }

        public UserListModel()
        { }

        public int UserId { get; set; }
        [Display(Name = "User_Display_UserCode", ResourceType = typeof(MessageResource))]
        public string UserCode { get; set; }
        public int? DealerId { get; set; }
        [Display(Name = "User_Display_DealerName", ResourceType = typeof(MessageResource))]
        public string DealerName { get; set; }
        [Display(Name = "User_Display_UserFirstName", ResourceType = typeof(MessageResource))]
        public string UserFirstName { get; set; }
        [Display(Name = "User_Display_UserLastName", ResourceType = typeof(MessageResource))]
        public string UserLastName { get; set; }
        [Display(Name = "User_Display_TCIdentityNo", ResourceType = typeof(MessageResource))]
        public string TCIdentityNo { get; set; }

        [Display(Name = "User_Display_PassportNo", ResourceType = typeof(MessageResource))]
        public string PassportNo { get; set; }

        [Display(Name = "User_Display_IdentityNo", ResourceType = typeof(MessageResource))]
        public string IdentityNo { get; set; } 

        [Display(Name = "User_Display_EMail", ResourceType = typeof(MessageResource))]
        public string EMail { get; set; }
        [Display(Name = "Global_Display_IsActive", ResourceType = typeof(MessageResource))]
        public int? IsActive { get; set; }
        [Display(Name = "Global_Display_IsActive", ResourceType = typeof(MessageResource))]
        public string IsActiveName { get; set; }
        public int? IsTechnician { get; set; }

        public int? RoleTypeId { get; set; }
        [Display(Name = "DealerUser_Display_RoleTypeName", ResourceType = typeof(MessageResource))]
        public string RoleTypeName { get; set; }

        public int? SexId { get; set; }
        public int? MaritalStatusId { get; set; }
    }
}
