using KSS.Common.CQRS;
using KSS.Common.Result;
using KSS.Service.Application.Interfaces.Services;
using KSS.Service.Application.Mappings;
using Microsoft.Extensions.Logging;

namespace KSS.Service.Application.Features.FuturesOrder.Commands;

public class NewLimitSellOrderCommandHandler : ICommandHandlerApi<NewLimitSellOrderCommand, DTOs.FuturesOrderDto>
{
    private readonly IFuturesOrderService _futuresOrderService;
    private readonly ILogger<NewLimitSellOrderCommandHandler> _logger;

    public NewLimitSellOrderCommandHandler(
        IFuturesOrderService futuresOrderService,
        ILogger<NewLimitSellOrderCommandHandler> logger)
    {
        _futuresOrderService = futuresOrderService;
        _logger = logger;
    }

    public async Task<ApiResult<DTOs.FuturesOrderDto>> Handle(NewLimitSellOrderCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var serviceRequest = new NewOrderCommand(
                request.Symbol,
                "Sell",
                "Limit",
                request.Quantity,
                request.Price,
                request.ClientOrderId
            );

            var order = await _futuresOrderService.NewOrderAsync(
                serviceRequest,
                cancellationToken);

            if (order == null)
            {
                return ApiResult<DTOs.FuturesOrderDto>.FailureResult("Failed to create limit sell order");
            }

            return ApiResult<DTOs.FuturesOrderDto>.CreateSuccess(
                FuturesOrderMapper.ToDto(order),
                "Limit sell order created successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating limit sell order. Symbol: {Symbol}", request.Symbol);
            
            return ApiResult<DTOs.FuturesOrderDto>.FailureResult("An error occurred while creating the order");
        }
    }
}

