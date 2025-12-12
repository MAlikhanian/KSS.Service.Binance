using KSS.Common.CQRS;
using KSS.Service.Application.DTOs;

namespace KSS.Service.Application.Features.FuturesOrder.Commands;

public record CancelMultipleOrdersCommand(
    string Symbol,
    List<long> OrderIds) : ICommandApi<List<FuturesOrderDto>>;

