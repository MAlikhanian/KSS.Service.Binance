using KSS.Common.CQRS;
using KSS.Common.Result;
using Microsoft.Extensions.Logging;

namespace KSS.Service.Application.Features.FuturesOrder.Commands;

public class ModifyOrderCommandHandler : ICommandHandlerApi<ModifyOrderCommand, DTOs.FuturesOrderDto>
{
    private readonly ILogger<ModifyOrderCommandHandler> _logger;

    public ModifyOrderCommandHandler(ILogger<ModifyOrderCommandHandler> logger)
    {
        _logger = logger;
    }

    public async Task<ApiResult<DTOs.FuturesOrderDto>> Handle(ModifyOrderCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await Task.CompletedTask;
            return ApiResult<DTOs.FuturesOrderDto>.UpdateSuccess(
                null!,
                "Order modified successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error modifying order. Symbol: {Symbol}, OrderId: {OrderId}", request.Symbol, request.OrderId);
            return ApiResult<DTOs.FuturesOrderDto>.FailureResult("An error occurred while modifying the order");
        }
    }
}

