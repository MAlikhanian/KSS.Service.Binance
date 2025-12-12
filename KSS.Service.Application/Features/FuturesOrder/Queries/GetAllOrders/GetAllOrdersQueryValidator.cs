using FluentValidation;

namespace KSS.Service.Application.Features.FuturesOrder.Queries;

public class GetAllOrdersQueryValidator : AbstractValidator<GetAllOrdersQuery>
{
    public GetAllOrdersQueryValidator()
    {
        RuleFor(x => x.Symbol)
            .NotEmpty()
            .WithMessage("Symbol is required")
            .Matches(@"^[A-Z0-9]+$")
            .WithMessage("Symbol must contain only uppercase letters and numbers");

        When(x => x.Limit.HasValue, () =>
        {
            RuleFor(x => x.Limit)
                .GreaterThan(0)
                .LessThanOrEqualTo(1000)
                .WithMessage("Limit must be between 1 and 1000");
        });
    }
}

