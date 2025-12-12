namespace KSS.Service.API.Requests.FuturesOrder.Commands;

public class NewOrderRequest
{
    public string Symbol { get; set; } = string.Empty;
    public string Side { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public decimal Quantity { get; set; }
    public decimal? Price { get; set; }
    public string? ClientOrderId { get; set; }
}

