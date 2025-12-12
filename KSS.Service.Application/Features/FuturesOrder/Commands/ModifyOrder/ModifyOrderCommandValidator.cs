using FluentValidation;

namespace KSS.Service.Application.Features.FuturesOrder.Commands;

public class ModifyOrderCommandValidator : AbstractValidator<ModifyOrderCommand>
{
    public ModifyOrderCommandValidator()
    {
        RuleFor(x => x.Symbol)
            .NotEmpty()
            .WithMessage("Symbol is required");

        RuleFor(x => x)
            .Must(x => x.OrderId.HasValue || !string.IsNullOrWhiteSpace(x.ClientOrderId))
            .WithMessage("Either OrderId or ClientOrderId must be provided");
    }
}

