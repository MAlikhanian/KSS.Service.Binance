namespace KSS.Service.API.Requests.FuturesOrder.Queries;

public record GetOrderRequest(
    string Symbol,
    long? OrderId = null,
    string? ClientOrderId = null);

