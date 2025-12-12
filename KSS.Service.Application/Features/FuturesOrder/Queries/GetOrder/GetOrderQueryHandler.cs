using KSS.Service.Application.Interfaces.Services;
using KSS.Service.Application.Mappings;
using MediatR;
using Microsoft.Extensions.Logging;

namespace KSS.Service.Application.Features.FuturesOrder.Queries;

public class GetOrderQueryHandler : IRequestHandler<GetOrderQuery, GetOrderResponse>
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

    public async Task<GetOrderResponse> Handle(GetOrderQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var order = await _futuresOrderService.GetOrderAsync(
                symbol: request.Symbol,
                orderId: request.OrderId,
                clientOrderId: request.ClientOrderId,
                cancellationToken: cancellationToken);

            if (order == null)
            {
                return new GetOrderResponse
                {
                    Success = false,
                    ErrorMessage = "Order not found"
                };
            }

            return new GetOrderResponse
            {
                Success = true,
                Order = FuturesOrderMapper.ToDto(order)
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving order. Symbol: {Symbol}, OrderId: {OrderId}",
                request.Symbol, request.OrderId);
            
            return new GetOrderResponse
            {
                Success = false,
                ErrorMessage = "An error occurred while retrieving the order"
            };
        }
    }
}

