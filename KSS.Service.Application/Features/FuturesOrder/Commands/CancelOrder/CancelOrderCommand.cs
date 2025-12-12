using KSS.Common.CQRS;
using KSS.Service.Application.DTOs;

namespace KSS.Service.Application.Features.FuturesOrder.Commands;

public record CancelOrderCommand(
    string Symbol,
    long? OrderId = null,
    string? ClientOrderId = null) : ICommandApi<FuturesOrderDto>;

