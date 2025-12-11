using KSS.Service.Application.DTOs;

namespace KSS.Service.Application.Interfaces.Services;

public interface IFuturesOrderService
{
    Task<FuturesOrderDto?> GetOrderAsync(
        string symbol,
        long? orderId = null,
        string? clientOrderId = null,
        CancellationToken cancellationToken = default);

    Task<FuturesOrderDto?> NewOrderAsync(
        string symbol,
        string side,
        string type,
        decimal quantity,
        decimal? price = null,
        string? clientOrderId = null,
        CancellationToken cancellationToken = default);
}

