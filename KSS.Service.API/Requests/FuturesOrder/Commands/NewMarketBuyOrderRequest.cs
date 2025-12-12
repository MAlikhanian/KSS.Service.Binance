namespace KSS.Service.API.Requests.FuturesOrder.Commands;

public record NewMarketBuyOrderRequest(
    string Symbol,
    decimal Quantity,
    string? ClientOrderId = null);

