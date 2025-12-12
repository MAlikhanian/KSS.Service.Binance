using FluentValidation;
using System.Text.RegularExpressions;

namespace KSS.Service.Application.Features.FuturesOrder.Queries;

public class GetOrderQueryValidator : AbstractValidator<GetOrderQuery>
{
    public GetOrderQueryValidator()
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

