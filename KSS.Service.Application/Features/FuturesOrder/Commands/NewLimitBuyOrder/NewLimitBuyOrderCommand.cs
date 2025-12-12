using KSS.Common.CQRS;
using KSS.Service.Application.DTOs;

namespace KSS.Service.Application.Features.FuturesOrder.Commands;

public record NewLimitBuyOrderCommand(
    string Symbol,
    decimal Quantity,
    decimal Price,
    string? ClientOrderId = null) : ICommandApi<FuturesOrderDto>;

