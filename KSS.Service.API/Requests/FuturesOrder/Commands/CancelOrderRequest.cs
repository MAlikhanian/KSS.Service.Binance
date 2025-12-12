namespace KSS.Service.API.Requests.FuturesOrder.Commands;

public class CancelOrderRequest
{
    public string Symbol { get; set; } = string.Empty;
    public long? OrderId { get; set; }
    public string? ClientOrderId { get; set; }
}

