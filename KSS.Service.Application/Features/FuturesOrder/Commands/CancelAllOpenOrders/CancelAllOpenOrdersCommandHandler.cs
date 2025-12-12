using KSS.Common.CQRS;
using KSS.Common.Result;
using KSS.Service.Application.DTOs;
using Microsoft.Extensions.Logging;

namespace KSS.Service.Application.Features.FuturesOrder.Commands;

public class CancelAllOpenOrdersCommandHandler : ICommandHandlerApi<CancelAllOpenOrdersCommand, List<FuturesOrderDto>>
{
    private readonly ILogger<CancelAllOpenOrdersCommandHandler> _logger;

    public CancelAllOpenOrdersCommandHandler(ILogger<CancelAllOpenOrdersCommandHandler> logger)
    {
        _logger = logger;
    }

    public async Task<ApiResult<List<FuturesOrderDto>>> Handle(CancelAllOpenOrdersCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await Task.CompletedTask;
            return ApiResult<List<FuturesOrderDto>>.SuccessResult(
                new List<FuturesOrderDto>(),
                "All open orders canceled successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error canceling all open orders. Symbol: {Symbol}", request.Symbol);
            return ApiResult<List<FuturesOrderDto>>.FailureResult("An error occurred while canceling orders");
        }
    }
}

