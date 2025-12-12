using KSS.Common.CQRS;
using KSS.Common.Result;
using KSS.Service.Application.Interfaces.Services;
using KSS.Service.Application.Mappings;
using Microsoft.Extensions.Logging;

namespace KSS.Service.Application.Features.FuturesOrder.Commands;

public class NewLimitBuyOrderCommandHandler : ICommandHandlerApi<NewLimitBuyOrderCommand, DTOs.FuturesOrderDto>
{
    private readonly IFuturesOrderService _futuresOrderService;
    private readonly ILogger<NewLimitBuyOrderCommandHandler> _logger;

    public NewLimitBuyOrderCommandHandler(
        IFuturesOrderService futuresOrderService,
        ILogger<NewLimitBuyOrderCommandHandler> logger)
    {
        _futuresOrderService = futuresOrderService;
        _logger = logger;
    }

    public async Task<ApiResult<DTOs.FuturesOrderDto>> Handle(NewLimitBuyOrderCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var serviceRequest = new NewOrderCommand(
                request.Symbol,
                "Buy",
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
                return ApiResult<DTOs.FuturesOrderDto>.FailureResult("Failed to create limit buy order");
            }

            return ApiResult<DTOs.FuturesOrderDto>.CreateSuccess(
                FuturesOrderMapper.ToDto(order),
                "Limit buy order created successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating limit buy order. Symbol: {Symbol}", request.Symbol);
            
            return ApiResult<DTOs.FuturesOrderDto>.FailureResult("An error occurred while creating the order");
        }
    }
}

