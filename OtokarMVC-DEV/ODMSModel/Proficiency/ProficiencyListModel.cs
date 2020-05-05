using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Kendo.Mvc.UI;
using ODMSCommon.Resources;
using ODMSModel.ListModel;

namespace ODMSModel.Proficiency
{
    /// <summary>
    /// DealerId, DealerProficiencyController ve ilgili viewler tarafindan kullanilmaktadir. ProficiencyController ve ilgili viewleri bu modelin
    /// DealerId property'si ile ilgilenmezler
    /// </summary>
    public class ProficiencyListModel : BaseListWithPagingModel
    {
        public int DealerId { get; set; }

        [Display(Name = "Proficiency_Display_Code", ResourceType = typeof(MessageResource))]
        public string ProficiencyCode { get; set; }

        [Display(Name = "Proficiency_Display_Name", ResourceType = typeof(MessageResource))]
        public string ProficiencyName { get; set; }

        [Display(Name = "Global_Display_Description", ResourceType = typeof(MessageResource))]
        public string Description { get; set; }

        public ProficiencyListModel([DataSourceRequest] DataSourceRequest request)
            : base(request)
        {
            var dMapper = new Dictionary<string, string>
                 {
                     {"ProficiencyCode", "PROFICIENCY_CODE"},
                     {"ProficiencyName", "PROFICIENCY_NAME"},
                     {"Description", "ADMIN_DESC"}
                 };
            SetMapper(dMapper);
        }

        public ProficiencyListModel() { }
    }
}
