using MediatR;

namespace KSS.Service.Application.Features.FuturesOrder.Commands;

public class CancelOrderCommand : IRequest<CancelOrderResponse>
{
    public string Symbol { get; set; } = string.Empty;
    public long? OrderId { get; set; }
    public string? ClientOrderId { get; set; }
}

