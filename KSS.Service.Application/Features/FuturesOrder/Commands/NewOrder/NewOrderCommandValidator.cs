using FluentValidation;

namespace KSS.Service.Application.Features.FuturesOrder.Commands.NewOrder;

public class NewOrderCommandValidator : AbstractValidator<NewOrderCommand>
{
    public NewOrderCommandValidator()
    {
        RuleFor(x => x.Symbol)
            .NotEmpty()
            .WithMessage("Symbol is required");

        RuleFor(x => x.Side)
            .NotEmpty()
            .WithMessage("Side is required");

        RuleFor(x => x.Type)
            .NotEmpty()
            .WithMessage("Type is required");

        RuleFor(x => x.Quantity)
            .GreaterThan(0)
            .WithMessage("Quantity must be greater than 0");
    }
}

