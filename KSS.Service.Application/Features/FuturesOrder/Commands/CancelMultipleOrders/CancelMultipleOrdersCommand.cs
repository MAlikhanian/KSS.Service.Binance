using MediatR;

namespace KSS.Service.Application.Features.FuturesOrder.Commands;

public class CancelMultipleOrdersCommand : IRequest<CancelMultipleOrdersResponse>
{
    public string Symbol { get; set; } = string.Empty;
    public List<long> OrderIds { get; set; } = new();
}

