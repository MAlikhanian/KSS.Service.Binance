using KSS.Common.CQRS;
using KSS.Service.Application.DTOs;

namespace KSS.Service.Application.Features.FuturesOrder.Commands;

public record NewOrderCommand(
    string Symbol,
    string Side,
    string Type,
    decimal Quantity,
    decimal? Price = null,
    string? ClientOrderId = null) : ICommandApi<FuturesOrderDto>;

