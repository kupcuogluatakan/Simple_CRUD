using FluentValidation;
using ODMSCommon.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODMSModel.SparePartSale
{
    public class OtokarSparePartSaleViewModelValidator:AbstractValidator<OtokarSparePartSaleViewModel>
    {
        public OtokarSparePartSaleViewModelValidator()
        {
            //required
            RuleFor(c => c.CustomerId).NotEmpty().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.SaleDate).NotEmpty().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.StatusId).NotNull().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.StockTypeId).NotNull().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.InvoiceNo).NotNull().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.InvoiceSerialNo).NotNull().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.InvoiceDate).NotNull().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.BillingAddressId).NotNull().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.ShippingAddressId).NotNull().WithLocalizedMessage(() => MessageResource.Validation_Required);
        }
    }
}
