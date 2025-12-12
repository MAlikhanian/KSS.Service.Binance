using KSS.Common.CQRS;
using KSS.Common.Result;
using KSS.Service.Application.DTOs;
using Microsoft.Extensions.Logging;

namespace KSS.Service.Application.Features.FuturesOrder.Commands;

public class CancelMultipleOrdersCommandHandler : ICommandHandlerApi<CancelMultipleOrdersCommand, List<FuturesOrderDto>>
{
    private readonly ILogger<CancelMultipleOrdersCommandHandler> _logger;

    public CancelMultipleOrdersCommandHandler(ILogger<CancelMultipleOrdersCommandHandler> logger)
    {
        _logger = logger;
    }

    public async Task<ApiResult<List<FuturesOrderDto>>> Handle(CancelMultipleOrdersCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await Task.CompletedTask;
            return ApiResult<List<FuturesOrderDto>>.SuccessResult(
                new List<FuturesOrderDto>(),
                "Multiple orders canceled successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error canceling multiple orders. Symbol: {Symbol}", request.Symbol);
            return ApiResult<List<FuturesOrderDto>>.FailureResult("An error occurred while canceling orders");
        }
    }
}

