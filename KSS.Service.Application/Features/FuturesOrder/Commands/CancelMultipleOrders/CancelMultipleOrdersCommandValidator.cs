using FluentValidation;

namespace KSS.Service.Application.Features.FuturesOrder.Commands;

public class CancelMultipleOrdersCommandValidator : AbstractValidator<CancelMultipleOrdersCommand>
{
    public CancelMultipleOrdersCommandValidator()
    {
        RuleFor(x => x.Symbol)
            .NotEmpty()
            .WithMessage("Symbol is required");

        RuleFor(x => x.OrderIds)
            .NotEmpty()
            .WithMessage("At least one OrderId is required");
    }
}

