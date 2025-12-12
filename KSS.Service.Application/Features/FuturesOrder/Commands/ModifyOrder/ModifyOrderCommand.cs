using MediatR;

namespace KSS.Service.Application.Features.FuturesOrder.Commands;

public class ModifyOrderCommand : IRequest<ModifyOrderResponse>
{
    public string Symbol { get; set; } = string.Empty;
    public long? OrderId { get; set; }
    public string? ClientOrderId { get; set; }
    public decimal? Quantity { get; set; }
    public decimal? Price { get; set; }
}

