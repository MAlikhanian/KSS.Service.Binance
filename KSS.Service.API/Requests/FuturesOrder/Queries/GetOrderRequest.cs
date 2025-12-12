namespace KSS.Service.API.Requests.FuturesOrder.Queries;

public class GetOrderRequest
{
    public string Symbol { get; set; } = string.Empty;
    public long? OrderId { get; set; }
    public string? ClientOrderId { get; set; }
}

