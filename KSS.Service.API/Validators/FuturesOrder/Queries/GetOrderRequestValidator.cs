using FluentValidation;
using KSS.Service.API.Requests.FuturesOrder.Queries;

namespace KSS.Service.API.Validators.FuturesOrder.Queries;

public class GetOrderRequestValidator : AbstractValidator<GetOrderRequest>
{
    public GetOrderRequestValidator()
    {
        RuleFor(x => x.Symbol)
            .NotEmpty()
            .WithMessage("Symbol is required")
            .Matches(@"^[A-Z0-9]+$")
            .WithMessage("Symbol must contain only uppercase letters and numbers");

        RuleFor(x => x)
            .Must(x => x.OrderId.HasValue || !string.IsNullOrWhiteSpace(x.ClientOrderId))
            .WithMessage("Either OrderId or ClientOrderId must be provided");

        When(x => x.OrderId.HasValue, () =>
        {
            RuleFor(x => x.OrderId)
                .GreaterThan(0)
                .WithMessage("OrderId must be greater than 0");
        });

        When(x => !string.IsNullOrWhiteSpace(x.ClientOrderId), () =>
        {
            RuleFor(x => x.ClientOrderId)
                .MaximumLength(50)
                .WithMessage("ClientOrderId cannot exceed 50 characters");
        });
    }
}

