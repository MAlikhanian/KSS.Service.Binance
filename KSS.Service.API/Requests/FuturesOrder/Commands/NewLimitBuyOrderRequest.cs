namespace KSS.Service.API.Requests.FuturesOrder.Commands;

public record NewLimitBuyOrderRequest(
    string Symbol,
    decimal Quantity,
    decimal Price,
    string? ClientOrderId = null);

