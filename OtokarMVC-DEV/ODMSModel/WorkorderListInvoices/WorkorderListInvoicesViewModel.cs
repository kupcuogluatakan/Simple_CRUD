using FluentValidation.Attributes;

namespace ODMSModel.WorkorderListInvoices
{
    [Validator(typeof(WorkorderListInvoicesViewModelValitador))]
    public class WorkorderListInvoicesViewModel : ModelBase
    {
    }
}
