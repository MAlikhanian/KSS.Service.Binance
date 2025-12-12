using KSS.Common.CQRS;
using KSS.Service.Application.DTOs;

namespace KSS.Service.Application.Features.FuturesOrder.Commands;

public record CancelAllOpenOrdersCommand(
    string Symbol) : ICommandApi<List<FuturesOrderDto>>;

