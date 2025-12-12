namespace KSS.Service.API.Requests.FuturesOrder.Commands;

public record CancelOrderRequest(
    string Symbol,
    long? OrderId = null,
    string? ClientOrderId = null);

