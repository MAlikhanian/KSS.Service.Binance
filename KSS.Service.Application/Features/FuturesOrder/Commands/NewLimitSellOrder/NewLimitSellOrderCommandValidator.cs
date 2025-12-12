using FluentValidation;

namespace KSS.Service.Application.Features.FuturesOrder.Commands;

public class NewLimitSellOrderCommandValidator : AbstractValidator<NewLimitSellOrderCommand>
{
    public NewLimitSellOrderCommandValidator()
    {
        RuleFor(x => x.Symbol)
            .NotEmpty()
            .WithMessage("Symbol is required")
            .Matches(@"^[A-Z0-9]+$")
            .WithMessage("Symbol must contain only uppercase letters and numbers");

        RuleFor(x => x.Quantity)
            .GreaterThan(0)
            .WithMessage("Quantity must be greater than 0");

        RuleFor(x => x.Price)
            .GreaterThan(0)
            .WithMessage("Price must be greater than 0");
    }
}

