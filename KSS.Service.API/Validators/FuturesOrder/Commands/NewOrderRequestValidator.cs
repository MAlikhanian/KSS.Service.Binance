using FluentValidation;
using KSS.Service.API.Requests.FuturesOrder.Commands;

namespace KSS.Service.API.Validators.FuturesOrder.Commands;

public class NewOrderRequestValidator : AbstractValidator<NewOrderRequest>
{
    public NewOrderRequestValidator()
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

