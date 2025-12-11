using MediatR;

namespace KSS.Service.Application.Features.FuturesOrder.Commands.NewMarketSellOrder;

public class NewMarketSellOrderCommand : IRequest<NewMarketSellOrderResponse>
{
    public string Symbol { get; set; } = string.Empty;
    public decimal Quantity { get; set; }
    public string? ClientOrderId { get; set; }
}

