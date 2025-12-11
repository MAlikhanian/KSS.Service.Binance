using KSS.Service.Application.Interfaces.Services;
using KSS.Service.Application.Mappings;
using MediatR;
using Microsoft.Extensions.Logging;

namespace KSS.Service.Application.Features.FuturesOrder.Commands.NewOrder;

public class NewOrderCommandHandler : IRequestHandler<NewOrderCommand, NewOrderResponse>
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

    public async Task<NewOrderResponse> Handle(NewOrderCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var order = await _futuresOrderService.NewOrderAsync(
                symbol: request.Symbol,
                side: request.Side,
                type: request.Type,
                quantity: request.Quantity,
                price: request.Price,
                clientOrderId: request.ClientOrderId,
                cancellationToken: cancellationToken);

            if (order == null)
            {
                return new NewOrderResponse
                {
                    Success = false,
                    ErrorMessage = "Failed to create order"
                };
            }

            return new NewOrderResponse
            {
                Success = true,
                Order = FuturesOrderMapper.ToDto(order)
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating new order. Symbol: {Symbol}, Side: {Side}, Type: {Type}",
                request.Symbol, request.Side, request.Type);
            
            return new NewOrderResponse
            {
                Success = false,
                ErrorMessage = "An error occurred while creating the order"
            };
        }
    }
}

