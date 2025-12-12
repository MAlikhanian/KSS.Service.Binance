using KSS.Common.CQRS;
using KSS.Common.Result;
using KSS.Service.Application.Interfaces.Services;
using KSS.Service.Application.Mappings;
using Microsoft.Extensions.Logging;

namespace KSS.Service.Application.Features.FuturesOrder.Queries;

public class GetOrderQueryHandler : IQueryHandlerApi<GetOrderQuery, DTOs.FuturesOrderDto>
{
    private readonly IFuturesOrderService _futuresOrderService;
    private readonly ILogger<GetOrderQueryHandler> _logger;

    public GetOrderQueryHandler(
        IFuturesOrderService futuresOrderService,
        ILogger<GetOrderQueryHandler> logger)
    {
        _futuresOrderService = futuresOrderService;
        _logger = logger;
    }

    public async Task<ApiResult<DTOs.FuturesOrderDto>> Handle(GetOrderQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var order = await _futuresOrderService.GetOrderAsync(
                request,
                cancellationToken);

            if (order == null)
            {
                return ApiResult<DTOs.FuturesOrderDto>.NotFoundResult("Order not found");
            }

            return ApiResult<DTOs.FuturesOrderDto>.SuccessResult(
                FuturesOrderMapper.ToDto(order),
                "Order retrieved successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving order. Symbol: {Symbol}, OrderId: {OrderId}",
                request.Symbol, request.OrderId);
            
            return ApiResult<DTOs.FuturesOrderDto>.FailureResult("An error occurred while retrieving the order");
        }
    }
}

