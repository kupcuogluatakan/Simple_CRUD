using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Kendo.Mvc.UI;
using ODMSCommon.Resources;
using ODMSModel.ListModel;

namespace ODMSModel.EducationType
{
    public class EducationTypeListModel : BaseListWithPagingModel
    {
        public int Id { get; set; }

        [Display(Name = "EducationType_Display_Name", ResourceType = typeof(MessageResource))]
        public string Name { get; set; }

        [Display(Name = "Global_Display_Description", ResourceType = typeof(MessageResource))]
        public string Description { get; set; }

        public EducationTypeListModel([DataSourceRequest] DataSourceRequest request)
            : base(request)
        {
            var dMapper = new Dictionary<string, string>
                 {
                     {"Id", "ID_EDUCATION_TYPE"},
                     {"Name", "EDUCATION_TYPE_NAME"}
                 };
            SetMapper(dMapper);
        }

        public EducationTypeListModel() { }
    }
}
