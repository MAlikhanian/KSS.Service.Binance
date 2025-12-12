using FluentValidation;
using KSS.Service.API.Requests.FuturesOrder.Commands;

namespace KSS.Service.API.Validators.FuturesOrder.Commands;

public class NewMarketBuyOrderRequestValidator : AbstractValidator<NewMarketBuyOrderRequest>
{
    public NewMarketBuyOrderRequestValidator()
    {
        RuleFor(x => x.Symbol)
            .NotEmpty()
            .WithMessage("Symbol is required")
            .Matches(@"^[A-Z0-9]+$")
            .WithMessage("Symbol must contain only uppercase letters and numbers");

        RuleFor(x => x.Quantity)
            .GreaterThan(0)
            .WithMessage("Quantity must be greater than 0");
    }
}

