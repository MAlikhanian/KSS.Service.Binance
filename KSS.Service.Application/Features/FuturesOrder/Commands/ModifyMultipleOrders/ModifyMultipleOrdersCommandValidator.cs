using FluentValidation;

namespace KSS.Service.Application.Features.FuturesOrder.Commands.ModifyMultipleOrders;

public class ModifyMultipleOrdersCommandValidator : AbstractValidator<ModifyMultipleOrdersCommand>
{
    public ModifyMultipleOrdersCommandValidator()
    {
        RuleFor(x => x.Symbol)
            .NotEmpty()
            .WithMessage("Symbol is required");

        RuleFor(x => x.Orders)
            .NotEmpty()
            .WithMessage("At least one order modification is required");
    }
}

