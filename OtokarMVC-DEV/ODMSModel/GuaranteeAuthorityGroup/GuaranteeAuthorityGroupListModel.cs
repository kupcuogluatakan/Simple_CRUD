using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Kendo.Mvc.UI;
using ODMSModel.ListModel;

namespace ODMSModel.GuaranteeAuthorityGroup
{
    public class GuaranteeAuthorityGroupListModel:BaseListWithPagingModel
    {
        public GuaranteeAuthorityGroupListModel(DataSourceRequest request):base(request)
        {
            var dMapper = new Dictionary<string, string>
                {
                    {"GroupId", "ID_GROUP"},
                    {"GroupName", "GROUP_NAME"},
                    {"MailList", "MAIL_LIST"},
                    {"IsActiveString", "IS_ACTIVE"}
                };
            SetMapper(dMapper);
        }

        public GuaranteeAuthorityGroupListModel()
        {
            // TODO: Complete member initialization
        }

        [Display(Name = "GuaranteeAuthorityGroup_Display_GroupName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public int GroupId { get; set; }
        [Display(Name = "GuaranteeAuthorityGroup_Display_GroupName", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string GroupName { get; set; }
        [Display(Name = "GuaranteeAuthorityGroup_Display_MailList", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string MailList { get; set; }
        [Display(Name = "Global_Display_IsActive", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public new string IsActiveString { get; set; }
    }
}
 