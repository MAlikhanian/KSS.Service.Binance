using FluentValidation;

namespace KSS.Service.Application.Features.FuturesOrder.Commands.CancelAllOpenOrders;

public class CancelAllOpenOrdersCommandValidator : AbstractValidator<CancelAllOpenOrdersCommand>
{
    public CancelAllOpenOrdersCommandValidator()
    {
        RuleFor(x => x.Symbol)
            .NotEmpty()
            .WithMessage("Symbol is required");
    }
}

