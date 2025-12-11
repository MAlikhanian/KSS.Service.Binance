using MediatR;

namespace KSS.Service.Application.Features.FuturesOrder.Commands.NewLimitSellOrder;

public class NewLimitSellOrderCommand : IRequest<NewLimitSellOrderResponse>
{
    public string Symbol { get; set; } = string.Empty;
    public decimal Quantity { get; set; }
    public decimal Price { get; set; }
    public string? ClientOrderId { get; set; }
}

