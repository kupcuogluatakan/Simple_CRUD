using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Kendo.Mvc.UI;
using ODMSCommon.Resources;
using ODMSModel.ListModel;

namespace ODMSModel.LabourType
{
    public class LabourTypeListModel : BaseListWithPagingModel
    {
        public int Id { get; set; }

        [Display(Name = "LabourType_Display_Name", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string Name { get; set; }

        [Display(Name = "LabourType_Display_VatRatio", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public double VatRatio { get; set; }

        [Display(Name = "LabourType_Display_Description", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string Description { get; set; }

        [Display(Name = "Global_Display_IsActive", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public bool IsActive { get; set; }

        [Display(Name = "Global_Display_IsActive", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public new string IsActiveString
        {
            get
            {
                if (IsActive)
                    return MessageResource.Global_Display_Active;
                return MessageResource.Global_Display_Passive;
            }
        }

        public LabourTypeListModel([DataSourceRequest] DataSourceRequest request)
            : base(request)
        {
            var dMapper = new Dictionary<string, string>
                 {
                     {"Id", "ID_LABOUR_TYPE"},
                     {"Name", "LABOUR_TYPE_DESC"},
                     {"VatRatio", "VAT_RATIO"},
                     {"Description", "ADMIN_DESC"},
                     {"IsActiveString", "IS_ACTIVE"},
                 };
            SetMapper(dMapper);
        }

        public LabourTypeListModel() { }

    }
}
