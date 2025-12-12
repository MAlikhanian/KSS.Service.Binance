using KSS.Common.CQRS;
using KSS.Service.Application.DTOs;

namespace KSS.Service.Application.Features.FuturesOrder.Commands;

public record NewMultipleOrdersCommand(
    List<OrderRequest> Orders) : ICommandApi<List<FuturesOrderDto>>;

public record OrderRequest(
    string Symbol,
    string Side,
    string Type,
    decimal Quantity,
    decimal? Price = null,
    string? ClientOrderId = null);

