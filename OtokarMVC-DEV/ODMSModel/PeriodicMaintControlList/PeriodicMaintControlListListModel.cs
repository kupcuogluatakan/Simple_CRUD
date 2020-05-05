using Kendo.Mvc.UI;
using ODMSCommon.Resources;
using ODMSModel.ListModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ODMSModel.PeriodicMaintControlList
{
    public class PeriodicMaintControlListListModel : BaseListWithPagingModel
    {
        public PeriodicMaintControlListListModel([DataSourceRequest] DataSourceRequest request)
            : base(request)
        {
            var dMapper = new Dictionary<string, string>
                {
                    {"ModelKod", "MODEL_KOD"},
                    {"TypeName", "TYPE_NAME"},
                    {"EngineType", "ENGINE_TYPE"},
                    {"DocumentDesc", "DOCUMENT_DESC"},
                    {"DocName", "DOC_NAME"},
                    {"LanguageName", "LANGUAGE_NAME"},
                };
            SetMapper(dMapper);
        }

        public PeriodicMaintControlListListModel()
        {
        }

        public int PeriodicMaintCtrlListId { get; set; }

        [Display(Name = "PeriodicMaintControlList_Display_Idtype", ResourceType = typeof(MessageResource))]
        public int? IdType { get; set; }

        [Display(Name = "PeriodicMaintControlList_Display_Language", ResourceType = typeof(MessageResource))]
        public string LanguageCustom { get; set; }

        [Display(Name = "PeriodicMaintControlList_Display_DocName", ResourceType = typeof(MessageResource))]
        public Int64? DocId { get; set; }

        [Display(Name = "PeriodicMaintControlList_Display_ModelKod", ResourceType = typeof(MessageResource))]
        public string ModelKod { get; set; }

        [Display(Name = "PeriodicMaintControlList_Display_Idtype", ResourceType = typeof(MessageResource))]
        public string TypeName { get; set; }

        [Display(Name = "PeriodicMaintControlList_Display_DocumentDesc", ResourceType = typeof(MessageResource))]
        public string DocumentDesc { get; set; }

        [Display(Name = "PeriodicMaintControlList_Display_DocName", ResourceType = typeof(MessageResource))]
        public string DocName { get; set; }

        [Display(Name = "PeriodicMaintControlList_Display_Language", ResourceType = typeof(MessageResource))]
        public string LanguageName { get; set; }

        [Display(Name = "Vehicle_Display_EngineType", ResourceType = typeof(MessageResource))]
        public string EngineType { get; set; }
    }
}
