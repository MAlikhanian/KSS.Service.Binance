using KSS.Common.CQRS;
using KSS.Service.Application.DTOs;

namespace KSS.Service.Application.Features.FuturesOrder.Commands;

public record NewMarketSellOrderCommand(
    string Symbol,
    decimal Quantity,
    string? ClientOrderId = null) : ICommandApi<FuturesOrderDto>;

