using FluentValidation;

namespace KSS.Service.Application.Features.FuturesOrder.Commands.CancelOrder;

public class CancelOrderCommandValidator : AbstractValidator<CancelOrderCommand>
{
    public CancelOrderCommandValidator()
    {
        RuleFor(x => x.Symbol)
            .NotEmpty()
            .WithMessage("Symbol is required");

        RuleFor(x => x)
            .Must(x => x.OrderId.HasValue || !string.IsNullOrWhiteSpace(x.ClientOrderId))
            .WithMessage("Either OrderId or ClientOrderId must be provided");
    }
}

