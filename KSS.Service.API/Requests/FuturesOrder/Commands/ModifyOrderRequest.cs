namespace KSS.Service.API.Requests.FuturesOrder.Commands;

public class ModifyOrderRequest
{
    public string Symbol { get; set; } = string.Empty;
    public long? OrderId { get; set; }
    public string? ClientOrderId { get; set; }
    public decimal? Quantity { get; set; }
    public decimal? Price { get; set; }
}

