using FluentValidation;

namespace KSS.Service.Application.Features.FuturesOrder.Commands;

public class CancelAllOpenOrdersCommandValidator : AbstractValidator<CancelAllOpenOrdersCommand>
{
    public CancelAllOpenOrdersCommandValidator()
    {
        RuleFor(x => x.Symbol)
            .NotEmpty()
            .WithMessage("Symbol is required");
    }
}

