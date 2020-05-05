using System;
using FluentValidation;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSCommon.Security;

namespace ODMSModel.Vehicle
{
    public class VehicleIndexViewModelValidator : AbstractValidator<VehicleIndexViewModel>
    {
        public VehicleIndexViewModelValidator()
        {
            RuleFor(c => c.VehicleCode).NotEmpty().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.VehicleCode)
                .Length(0, 20)
                .WithLocalizedMessage(() => MessageResource.Validation_Length,20);
            RuleFor(c => c.VinNo).NotNull().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.VinNo)
                .Length(0, 17)
                .WithLocalizedMessage(() => MessageResource.Validation_Length,17);
            RuleFor(c => c.EngineNo)
                .Length(0, 30)
                .WithLocalizedMessage(() => MessageResource.Validation_Length,30);
            RuleFor(c => c.ModelYear).NotEmpty().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.ModelYear)
                .ExclusiveBetween(1900, 2500)
                .WithLocalizedMessage(() => MessageResource.Validation_Year_Format);
            RuleFor(c => c.VatExcludeType).NotNull().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.ContractNo)
                .Length(0, 30)
                .WithLocalizedMessage(() => MessageResource.Validation_Length,30);
            RuleFor(c => c.Plate)
                .Length(0, 20)
                .WithLocalizedMessage(() => MessageResource.Validation_Length,20);
            RuleFor(c => c.Color)
                .Length(0, 20)
                .WithLocalizedMessage(() => MessageResource.Validation_Length,20);
            RuleFor(c => c.WarrantyEndKilometer)
                .LessThanOrEqualTo(9999999)
                .WithMessage(string.Format(MessageResource.Validation_IntegerLength, 7));
            RuleFor(c => c.VehicleKilometer)
                .InclusiveBetween(0, 9999999)
                .WithMessage(string.Format(MessageResource.Validation_IntegerLength, 7));
            RuleFor(c => c.SpecialConditions).Length(0, 2000)
                                             .WithLocalizedMessage(() => MessageResource.Validation_Length,2000);
            RuleFor(c => c.Notes).Length(0, 2000)
                                 .WithLocalizedMessage(() => MessageResource.Validation_Length);
            RuleFor(c => c.OutOfWarrantyDescription).NotNull().When(c => c.WarrantyStatus == 0 && !UserManager.UserInfo.IsDealer).WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.OutOfWarrantyDescription).Length(0, 2000)
                                                    .WithLocalizedMessage(() => MessageResource.Validation_Length);
            RuleFor(c => c.IsActive).NotNull().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.Hour).GreaterThanOrEqualTo(0).When(c => c.IsHourMaint).WithLocalizedMessage(() => MessageResource.Validation_Invalid_Hour);
            RuleFor(c => c.Hour).NotNull().When(c => c.IsHourMaint).WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.IdPriceList).NotNull().WithLocalizedMessage(() => MessageResource.Validation_Required);
            RuleFor(c => c.FactoryQualityControlDate)
                .GreaterThanOrEqualTo(o => o.FactoryProductionDate.GetValue<DateTime>()).When(c => !UserManager.UserInfo.IsDealer)
                .WithMessage(string.Format(MessageResource.Validation_EndDateGreaterThanBeginDate,
                                           MessageResource.Vehicle_Display_FactoryQualityControlDate,
                                           MessageResource.Vehicle_Display_FactoryProductionDate));
            RuleFor(c => c.FactoryShipmentDate)
                .GreaterThanOrEqualTo(o => o.FactoryQualityControlDate.GetValue<DateTime>()).When(c => !UserManager.UserInfo.IsDealer)
                .WithMessage(string.Format(MessageResource.Validation_EndDateGreaterThanBeginDate,
                                           MessageResource.Vehicle_Display_FactoryShipmentDate,
                                           MessageResource.Vehicle_Display_FactoryQualityControlDate));
            RuleFor(c => c.WarrantyStartDate)
                .GreaterThanOrEqualTo(o => o.FactoryShipmentDate.GetValue<DateTime>()).When(c => !UserManager.UserInfo.IsDealer)
                .WithMessage(string.Format(MessageResource.Validation_EndDateGreaterThanBeginDate,
                                           MessageResource.Vehicle_Display_WarrantyStartDate,
                                           MessageResource.Vehicle_Display_FactoryShipmentDate));
            RuleFor(c => c.WarrantyEndDate)
                .GreaterThanOrEqualTo(o => o.WarrantyStartDate.GetValue<DateTime>()).When(c => !UserManager.UserInfo.IsDealer)
                .WithMessage(string.Format(MessageResource.Validation_EndDateGreaterThanBeginDate,
                                           MessageResource.Vehicle_Display_WarrantyEndDate,
                                           MessageResource.Vehicle_Display_WarrantyStartDate));
            RuleFor(c => c.CorrosionWarrantyEndDate)
                .LessThanOrEqualTo(o => o.WarrantyEndDate.GetValue<DateTime>()).When(c => !UserManager.UserInfo.IsDealer)
                .WithMessage(string.Format(MessageResource.Validation_EndDateGreaterThanBeginDate,
                                           MessageResource.Vehicle_Display_CorrosionWarrantyEndDate,
                                           MessageResource.Vehicle_Display_WarrantyEndDate));
            RuleFor(c => c.PaintWarrantyEndDate)
                .GreaterThanOrEqualTo(o => o.CorrosionWarrantyEndDate.GetValue<DateTime>()).When(c => !UserManager.UserInfo.IsDealer)
                .WithMessage(string.Format(MessageResource.Validation_EndDateGreaterThanBeginDate,
                                           MessageResource.Vehicle_Display_PaintWarrantyEndDate,
                                           MessageResource.Vehicle_Display_CorrosionWarrantyEndDate));

            RuleFor(c => c.Location)
                .Length(0, 30)
                .WithLocalizedMessage(() => MessageResource.Validation_Length,30);
            RuleFor(c => c.ResponsiblePerson)
                .Length(0, 30)
                .WithLocalizedMessage(() => MessageResource.Validation_Length,30);
            RuleFor(c => c.ResponsiblePersonPhone)
                .Length(0, 20)
                .WithLocalizedMessage(() => MessageResource.Validation_Length,20);
        }
    }
}
