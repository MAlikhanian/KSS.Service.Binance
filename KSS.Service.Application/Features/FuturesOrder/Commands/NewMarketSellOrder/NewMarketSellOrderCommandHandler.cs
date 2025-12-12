using KSS.Common.CQRS;
using KSS.Common.Result;
using KSS.Service.Application.Interfaces.Services;
using KSS.Service.Application.Mappings;
using Microsoft.Extensions.Logging;

namespace KSS.Service.Application.Features.FuturesOrder.Commands;

public class NewMarketSellOrderCommandHandler : ICommandHandlerApi<NewMarketSellOrderCommand, DTOs.FuturesOrderDto>
{
    private readonly IFuturesOrderService _futuresOrderService;
    private readonly ILogger<NewMarketSellOrderCommandHandler> _logger;

    public NewMarketSellOrderCommandHandler(
        IFuturesOrderService futuresOrderService,
        ILogger<NewMarketSellOrderCommandHandler> logger)
    {
        _futuresOrderService = futuresOrderService;
        _logger = logger;
    }

    public async Task<ApiResult<DTOs.FuturesOrderDto>> Handle(NewMarketSellOrderCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var serviceRequest = new NewOrderCommand(
                request.Symbol,
                "Sell",
                "Market",
                request.Quantity,
                null,
                request.ClientOrderId
            );

            var order = await _futuresOrderService.NewOrderAsync(
                serviceRequest,
                cancellationToken);

            if (order == null)
            {
                return ApiResult<DTOs.FuturesOrderDto>.FailureResult("Failed to create market sell order");
            }

            return ApiResult<DTOs.FuturesOrderDto>.CreateSuccess(
                FuturesOrderMapper.ToDto(order),
                "Market sell order created successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating market sell order. Symbol: {Symbol}", request.Symbol);
            
            return ApiResult<DTOs.FuturesOrderDto>.FailureResult("An error occurred while creating the order");
        }
    }
}

