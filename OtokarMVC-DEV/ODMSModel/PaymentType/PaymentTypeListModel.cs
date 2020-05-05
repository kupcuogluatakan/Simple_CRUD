using System.Collections.Generic;
using Kendo.Mvc.UI;
using ODMSModel.ListModel;

namespace ODMSModel.PaymentType
{
    public class PaymentTypeListModel : BaseListWithPagingModel
    {
        public int Id { get; set; }
        public bool BankRequired { get; set; }
        public bool InstalmentRequired { get; set; }
        public bool TransmitNoRequired { get; set; }
        public string PaymentTypeName { get; set; }
        public bool DefermentRequired { get; set; }

        public PaymentTypeListModel([DataSourceRequest] DataSourceRequest request)
            : base(request)
        {
            var dMapper = new Dictionary<string, string>
                 {
                     {"Id", "ID_PAYMENT_TYPE"},
                     {"BankRequired", "BANK_REQUIRED"},
                     {"InstalmentRequired", "INSTALMENT_REQUIRED"},
                     {"TransmitNoRequired", "TRANSMIT_NO_REQUIRED"},
                     {"PaymentTypeName", "PAYMENT_TYPE_DESC"},
                     {"DefermentRequired", "DEFERMENT_REQUIRED"}
                 };
            SetMapper(dMapper);
        }

        public PaymentTypeListModel() { }
    }
}
