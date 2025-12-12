namespace KSS.Service.API.Requests.FuturesOrder.Commands;

public record ModifyMultipleOrdersRequest(
    string Symbol,
    List<OrderModificationRequest> Orders);

public record OrderModificationRequest(
    long? OrderId = null,
    string? ClientOrderId = null,
    decimal? Quantity = null,
    decimal? Price = null);

