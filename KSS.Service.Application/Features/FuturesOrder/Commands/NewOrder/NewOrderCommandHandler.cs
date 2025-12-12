using KSS.Common.CQRS;
using KSS.Common.Result;
using KSS.Service.Application.Interfaces.Services;
using KSS.Service.Application.Mappings;
using Microsoft.Extensions.Logging;

namespace KSS.Service.Application.Features.FuturesOrder.Commands;

public class NewOrderCommandHandler : ICommandHandlerApi<NewOrderCommand, DTOs.FuturesOrderDto>
{
    private readonly IFuturesOrderService _futuresOrderService;
    private readonly ILogger<NewOrderCommandHandler> _logger;

    public NewOrderCommandHandler(
        IFuturesOrderService futuresOrderService,
        ILogger<NewOrderCommandHandler> logger)
    {
        _futuresOrderService = futuresOrderService;
        _logger = logger;
    }

    public async Task<ApiResult<DTOs.FuturesOrderDto>> Handle(NewOrderCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var order = await _futuresOrderService.NewOrderAsync(
                request,
                cancellationToken);

            if (order == null)
            {
                return ApiResult<DTOs.FuturesOrderDto>.FailureResult("Failed to create order");
            }

            return ApiResult<DTOs.FuturesOrderDto>.CreateSuccess(
                FuturesOrderMapper.ToDto(order),
                "Order created successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating new order. Symbol: {Symbol}, Side: {Side}, Type: {Type}",
                request.Symbol, request.Side, request.Type);
            
            return ApiResult<DTOs.FuturesOrderDto>.FailureResult("An error occurred while creating the order");
        }
    }
}

