using KSS.Common.CQRS;
using KSS.Service.Application.DTOs;

namespace KSS.Service.Application.Features.FuturesOrder.Commands;

public record ModifyOrderCommand(
    string Symbol,
    long? OrderId = null,
    string? ClientOrderId = null,
    decimal? Quantity = null,
    decimal? Price = null) : ICommandApi<FuturesOrderDto>;

