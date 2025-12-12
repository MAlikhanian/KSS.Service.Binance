namespace KSS.Service.API.Requests.FuturesOrder.Commands;

public record NewMarketSellOrderRequest(
    string Symbol,
    decimal Quantity,
    string? ClientOrderId = null);

