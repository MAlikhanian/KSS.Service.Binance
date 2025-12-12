using MediatR;

namespace KSS.Service.Application.Features.FuturesOrder.Commands;

public class NewLimitBuyOrderCommand : IRequest<NewLimitBuyOrderResponse>
{
    public string Symbol { get; set; } = string.Empty;
    public decimal Quantity { get; set; }
    public decimal Price { get; set; }
    public string? ClientOrderId { get; set; }
}

