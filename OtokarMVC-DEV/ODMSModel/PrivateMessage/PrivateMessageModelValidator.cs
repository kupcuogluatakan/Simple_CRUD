using FluentValidation;
using ODMSCommon.Resources;

namespace ODMSModel.PrivateMessage
{
    public class PrivateMessageModelValidator:AbstractValidator<PrivateMessageModel>
    {
        public PrivateMessageModelValidator()
        {
            RuleFor(c => c.Reciever).NotEmpty().WithMessage(MessageResource.Validation_Required);
            RuleFor(c => c.RecieverId).NotEmpty().WithMessage(MessageResource.Validation_Required);
            RuleFor(c => c.Message).NotEmpty().WithMessage(MessageResource.Validation_Required);
            RuleFor(c => c.Title)
                .NotEmpty()
                .When(c => c.MessageId == 0)
                .WithMessage(MessageResource.Validation_Required);
            RuleFor(c => c.Title)
                .Length(0, 300)
                .When(c => c.MessageId == 0)
                .WithMessage(string.Format(MessageResource.Validation_Length, 300));
            RuleFor(c => c.Reciever)
                .NotEmpty()
                 .When(c => c.MessageId == 0)
                .WithMessage(MessageResource.Validation_Required);
        }
    }
}
