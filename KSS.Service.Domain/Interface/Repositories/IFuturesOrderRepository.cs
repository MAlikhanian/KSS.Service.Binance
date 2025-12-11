using KSS.Service.Domain.Entities;

namespace KSS.Service.Domain.Interface.Repositories;

public interface IFuturesOrderRepository
{
    Task<FuturesOrder?> GetByIdAsync(long orderId, CancellationToken cancellationToken = default);
    Task<FuturesOrder?> GetByClientOrderIdAsync(string clientOrderId, CancellationToken cancellationToken = default);
    Task<IEnumerable<FuturesOrder>> GetAllBySymbolAsync(string symbol, int? limit = null, CancellationToken cancellationToken = default);
    Task<FuturesOrder> SaveAsync(FuturesOrder order, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(long orderId, CancellationToken cancellationToken = default);
}

