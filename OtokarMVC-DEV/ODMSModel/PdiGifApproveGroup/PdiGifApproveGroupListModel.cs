using System.Collections.Generic;

namespace ODMSModel.PdiGifApproveGroup
{
    public class PdiGifApproveGroupListModel:GuaranteeAuthorityGroup.GuaranteeAuthorityGroupListModel
    {

        public PdiGifApproveGroupListModel(Kendo.Mvc.UI.DataSourceRequest request):base(request)
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

        public PdiGifApproveGroupListModel()
        {

        }
    }
}
