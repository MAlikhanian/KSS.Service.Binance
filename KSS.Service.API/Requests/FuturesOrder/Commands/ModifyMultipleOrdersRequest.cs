namespace KSS.Service.API.Requests.FuturesOrder.Commands;

public class ModifyMultipleOrdersRequest
{
    public string Symbol { get; set; } = string.Empty;
    public List<OrderModificationRequest> Orders { get; set; } = new();
}

public class OrderModificationRequest
{
    public long? OrderId { get; set; }
    public string? ClientOrderId { get; set; }
    public decimal? Quantity { get; set; }
    public decimal? Price { get; set; }
}

