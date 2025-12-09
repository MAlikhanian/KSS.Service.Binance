using MediatR;

namespace KSS.Service.Application.Features.FuturesOrder.Commands.NewMultipleOrders;

public class NewMultipleOrdersCommand : IRequest<NewMultipleOrdersResponse>
{
    public List<OrderRequest> Orders { get; set; } = new();
}

public class OrderRequest
{
    public string Symbol { get; set; } = string.Empty;
    public string Side { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public decimal Quantity { get; set; }
    public decimal? Price { get; set; }
    public string? ClientOrderId { get; set; }
}

