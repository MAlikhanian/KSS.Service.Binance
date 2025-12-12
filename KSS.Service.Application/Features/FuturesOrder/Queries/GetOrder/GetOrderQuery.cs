using KSS.Common.CQRS;
using KSS.Service.Application.DTOs;

namespace KSS.Service.Application.Features.FuturesOrder.Queries;

public record GetOrderQuery(
    string Symbol,
    long? OrderId = null,
    string? ClientOrderId = null) : IQueryApi<FuturesOrderDto>;

