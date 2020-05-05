using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ODMSModel.ListModel;

namespace ODMSModel.GeneralParameter
{
    public class GeneralParameterListModel:BaseListWithPagingModel
    {

        public GeneralParameterListModel(Kendo.Mvc.UI.DataSourceRequest request):base(request)
        {
            var dMapper = new Dictionary<string, string>
                {
                    {"ParameterId", "PARAMETER_ID"},
                    {"Description", "DESCRIPTION"},
                    {"Value", "VALUE"},
                    {"Type", "TYPE"}
                };
            SetMapper(dMapper);
        }

        public GeneralParameterListModel(){}

        public string ParameterId { get; set; }
        [Display(Name = "Global_Display_Description", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string Description { get; set; }

        public string Type { get; set; }

        [Display(Name = "GeneralParameter_Display_Value", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string Value { get; set; }

    }
}
