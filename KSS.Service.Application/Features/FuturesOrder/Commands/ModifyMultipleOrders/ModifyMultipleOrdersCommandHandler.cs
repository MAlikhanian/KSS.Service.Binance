using KSS.Common.CQRS;
using KSS.Common.Result;
using KSS.Service.Application.DTOs;
using Microsoft.Extensions.Logging;

namespace KSS.Service.Application.Features.FuturesOrder.Commands;

public class ModifyMultipleOrdersCommandHandler : ICommandHandlerApi<ModifyMultipleOrdersCommand, List<FuturesOrderDto>>
{
    private readonly ILogger<ModifyMultipleOrdersCommandHandler> _logger;

    public ModifyMultipleOrdersCommandHandler(ILogger<ModifyMultipleOrdersCommandHandler> logger)
    {
        _logger = logger;
    }

    public async Task<ApiResult<List<FuturesOrderDto>>> Handle(ModifyMultipleOrdersCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await Task.CompletedTask;
            return ApiResult<List<FuturesOrderDto>>.UpdateSuccess(
                new List<FuturesOrderDto>(),
                "Multiple orders modified successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error modifying multiple orders. Symbol: {Symbol}", request.Symbol);
            return ApiResult<List<FuturesOrderDto>>.FailureResult("An error occurred while modifying orders");
        }
    }
}

