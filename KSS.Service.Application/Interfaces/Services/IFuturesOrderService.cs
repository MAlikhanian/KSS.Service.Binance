using KSS.Service.Application.Features.FuturesOrder.Commands;
using KSS.Service.Application.Features.FuturesOrder.Queries;
using KSS.Service.Domain.Entities;

namespace KSS.Service.Application.Interfaces.Services;

public interface IFuturesOrderService
{
    Task<FuturesOrder?> GetOrderAsync(
        GetOrderQuery request,
        CancellationToken cancellationToken = default);

    Task<FuturesOrder?> NewOrderAsync(
        NewOrderCommand request,
        CancellationToken cancellationToken = default);
}

