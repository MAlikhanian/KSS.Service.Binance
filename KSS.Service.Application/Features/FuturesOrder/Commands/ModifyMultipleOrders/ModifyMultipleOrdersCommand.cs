using KSS.Common.CQRS;
using KSS.Service.Application.DTOs;

namespace KSS.Service.Application.Features.FuturesOrder.Commands;

public record ModifyMultipleOrdersCommand(
    string Symbol,
    List<OrderModification> Orders) : ICommandApi<List<FuturesOrderDto>>;

public record OrderModification(
    long? OrderId = null,
    string? ClientOrderId = null,
    decimal? Quantity = null,
    decimal? Price = null);

