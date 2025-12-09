using MediatR;

namespace KSS.Service.Application.Features.FuturesOrder.Commands.ModifyMultipleOrders;

public class ModifyMultipleOrdersCommand : IRequest<ModifyMultipleOrdersResponse>
{
    public string Symbol { get; set; } = string.Empty;
    public List<OrderModification> Orders { get; set; } = new();
}

public class OrderModification
{
    public long? OrderId { get; set; }
    public string? ClientOrderId { get; set; }
    public decimal? Quantity { get; set; }
    public decimal? Price { get; set; }
}

