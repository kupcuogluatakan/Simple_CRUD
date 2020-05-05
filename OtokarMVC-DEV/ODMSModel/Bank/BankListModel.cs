using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Kendo.Mvc.UI;
using ODMSModel.ListModel;

namespace ODMSModel.Bank
{
    public class BankListModel : BaseListWithPagingModel
    {
        public int Id { get; set; }

        [Display(Name = "Bank_Display_Code", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string Code { get; set; }

        [Display(Name = "Bank_Display_Name", ResourceType = typeof(ODMSCommon.Resources.MessageResource))]
        public string Name { get; set; }


        public BankListModel([DataSourceRequest] DataSourceRequest request)
            : base(request)
        {
            var dMapper = new Dictionary<string, string>
                 {
                     {"Id", "ID_BANK"},
                     {"Name", "BANK_CODE"},
                     {"Code", "BANK_NAME"}
                 };
            SetMapper(dMapper);
        }

        public BankListModel() { }
    }
}
