namespace KSS.Service.API.Requests.FuturesOrder.Commands;

public record NewMultipleOrdersRequest(
    List<OrderRequestDto> Orders);

public record OrderRequestDto(
    string Symbol,
    string Side,
    string Type,
    decimal Quantity,
    decimal? Price = null,
    string? ClientOrderId = null);

