namespace KSS.Service.API.Requests.FuturesOrder.Commands;

public class NewLimitBuyOrderRequest
{
    public string Symbol { get; set; } = string.Empty;
    public decimal Quantity { get; set; }
    public decimal Price { get; set; }
    public string? ClientOrderId { get; set; }
}

