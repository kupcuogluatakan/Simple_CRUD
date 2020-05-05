using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Kendo.Mvc.UI;
using ODMSModel.ListModel;
namespace ODMSModel.BodyworkDetail
{
    public class BodyworkDetailListModel : BaseListWithPagingModel
    {
        public BodyworkDetailListModel([DataSourceRequest] DataSourceRequest request)
            : base(request)
        {
            var dMapper = new Dictionary<string, string>
                 {
                     {"BodyworkCode", "BODYWORK_CODE"},
                     {"BodyworkCodeName", "BODYWORK_NAME"},
                     {"Description", "DESCRIPTION"}
                 };
            SetMapper(dMapper);
        }

        public BodyworkDetailListModel()
        {
        }

        [Display(Name = "BodyworkDetail_Display_Code", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string BodyworkCode { get; set; }

        [Display(Name = "BodyworkDetail_Display_Name", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string BodyworkCodeName { get; set; }

        [Display(Name = "Global_Display_Description", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string Description { get; set; }

    }
}
