using FluentValidation;

namespace KSS.Service.Application.Features.FuturesOrder.Commands.NewMultipleOrders;

public class NewMultipleOrdersCommandValidator : AbstractValidator<NewMultipleOrdersCommand>
{
    public NewMultipleOrdersCommandValidator()
    {
        RuleFor(x => x.Orders)
            .NotEmpty()
            .WithMessage("At least one order is required");
    }
}

