using MediatR;

namespace KSS.Service.Application.Features.FuturesOrder.Commands.CancelAllOpenOrders;

public class CancelAllOpenOrdersCommand : IRequest<CancelAllOpenOrdersResponse>
{
    public string Symbol { get; set; } = string.Empty;
}

