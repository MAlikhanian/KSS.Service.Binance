using MediatR;

namespace KSS.Service.Application.Features.FuturesOrder.Commands.NewMarketBuyOrder;

public class NewMarketBuyOrderCommand : IRequest<NewMarketBuyOrderResponse>
{
    public string Symbol { get; set; } = string.Empty;
    public decimal Quantity { get; set; }
    public string? ClientOrderId { get; set; }
}

