using FluentValidation;
using ODMSCommon.Resources;

namespace ODMSModel.SparePartBarcode
{
    public class SparePartBarcodeIndexViewModelValidator : AbstractValidator<SparePartBarcodeIndexViewModel>
    {
        public SparePartBarcodeIndexViewModelValidator()
        {
            RuleFor(c => c.PartId).NotEqual(0).WithLocalizedMessage(() => MessageResource.Validation_Required);
        }
    }
}
