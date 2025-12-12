namespace KSS.Service.API.Requests.FuturesOrder.Commands;

public record NewLimitSellOrderRequest(
    string Symbol,
    decimal Quantity,
    decimal Price,
    string? ClientOrderId = null);

