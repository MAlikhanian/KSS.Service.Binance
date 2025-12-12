namespace KSS.Service.API.Requests.FuturesOrder.Commands;

public class NewMarketSellOrderRequest
{
    public string Symbol { get; set; } = string.Empty;
    public decimal Quantity { get; set; }
    public string? ClientOrderId { get; set; }
}

