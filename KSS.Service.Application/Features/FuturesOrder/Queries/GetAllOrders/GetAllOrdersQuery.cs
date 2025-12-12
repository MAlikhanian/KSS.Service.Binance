using KSS.Common.CQRS;
using KSS.Service.Application.DTOs;

namespace KSS.Service.Application.Features.FuturesOrder.Queries;

public record GetAllOrdersQuery(
    string Symbol,
    int? Limit = null) : IQueryApi<List<FuturesOrderDto>>;

