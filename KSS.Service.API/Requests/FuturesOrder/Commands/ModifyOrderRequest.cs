namespace KSS.Service.API.Requests.FuturesOrder.Commands;

public record ModifyOrderRequest(
    string Symbol,
    long? OrderId = null,
    string? ClientOrderId = null,
    decimal? Quantity = null,
    decimal? Price = null);

