using KSS.Service.Domain.Entities;

namespace KSS.Service.Application.Interfaces.Services;

public interface IFuturesOrderService
{
    Task<FuturesOrder?> GetOrderAsync(
        string symbol,
        long? orderId = null,
        string? clientOrderId = null,
        CancellationToken cancellationToken = default);

    Task<FuturesOrder?> NewOrderAsync(
        string symbol,
        string side,
        string type,
        decimal quantity,
        decimal? price = null,
        string? clientOrderId = null,
        CancellationToken cancellationToken = default);
}

