using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Kendo.Mvc.UI;
using ODMSModel.ListModel;

namespace ODMSModel.GuaranteeAuthorityLimitations
{
    public class GuaranteeAuthorityLimitationsListModel : BaseListWithPagingModel
    {
        public GuaranteeAuthorityLimitationsListModel(DataSourceRequest request)
            : base(request)
        {

            var dMapper = new Dictionary<string, string>
            {
                {"ModelName", "MODEL_NAME"},
                {"CurrencyName", "CURRENCY_NAME"},
                {"Amount", "AMOUNT"},
                {"CumulativeAmount", "CUMULATIVE_AMOUNT"}
            };
            SetMapper(dMapper);
        }

        public GuaranteeAuthorityLimitationsListModel()
        {
            // TODO: Complete member initialization
        }

        [Display(Name = "Campaign_Display_ModelKod", ResourceType = typeof (ODMSCommon.Resources.MessageResource))]
        public string ModelKod { get; set; }

        [Display(Name = "Appointment_Display_VehicleModel", ResourceType = typeof (ODMSCommon.Resources.MessageResource)
            )]
        public string ModelName { get; set; }

        [Display(Name = "Currency_Display_CurrencyCode", ResourceType = typeof (ODMSCommon.Resources.MessageResource))]
        public string CurrencyCode { get; set; }

        [Display(Name = "Currency_Display_CurrencyName", ResourceType = typeof (ODMSCommon.Resources.MessageResource))]
        public string CurrencyName { get; set; }

        [Display(Name = "GuaranteeAuthorityLimitations_Display_Amount",
            ResourceType = typeof (ODMSCommon.Resources.MessageResource))]
        public decimal Amount { get; set; }

        [Display(Name = "GuaranteeAuthorityLimitations_Display_CumulativeAmount",
            ResourceType = typeof (ODMSCommon.Resources.MessageResource))]
        public decimal CumulativeAmount { get; set; }
    }
}
