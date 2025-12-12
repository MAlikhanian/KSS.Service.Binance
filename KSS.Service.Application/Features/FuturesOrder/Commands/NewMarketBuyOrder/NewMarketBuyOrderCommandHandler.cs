using KSS.Common.CQRS;
using KSS.Common.Result;
using KSS.Service.Application.Interfaces.Services;
using KSS.Service.Application.Mappings;
using Microsoft.Extensions.Logging;

namespace KSS.Service.Application.Features.FuturesOrder.Commands;

public class NewMarketBuyOrderCommandHandler : ICommandHandlerApi<NewMarketBuyOrderCommand, DTOs.FuturesOrderDto>
{
    private readonly IFuturesOrderService _futuresOrderService;
    private readonly ILogger<NewMarketBuyOrderCommandHandler> _logger;

    public NewMarketBuyOrderCommandHandler(
        IFuturesOrderService futuresOrderService,
        ILogger<NewMarketBuyOrderCommandHandler> logger)
    {
        _futuresOrderService = futuresOrderService;
        _logger = logger;
    }

    public async Task<ApiResult<DTOs.FuturesOrderDto>> Handle(NewMarketBuyOrderCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var serviceRequest = new NewOrderCommand(
                request.Symbol,
                "Buy",
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
                return ApiResult<DTOs.FuturesOrderDto>.FailureResult("Failed to create market buy order");
            }

            return ApiResult<DTOs.FuturesOrderDto>.CreateSuccess(
                FuturesOrderMapper.ToDto(order),
                "Market buy order created successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating market buy order. Symbol: {Symbol}", request.Symbol);
            
            return ApiResult<DTOs.FuturesOrderDto>.FailureResult("An error occurred while creating the order");
        }
    }
}

