namespace KSS.Service.API.Requests.FuturesOrder.Commands;

public record NewOrderRequest(
    string Symbol,
    string Side,
    string Type,
    decimal Quantity,
    decimal? Price = null,
    string? ClientOrderId = null);

