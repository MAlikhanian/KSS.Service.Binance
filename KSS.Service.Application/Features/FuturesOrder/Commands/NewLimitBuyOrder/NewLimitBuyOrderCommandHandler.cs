using KSS.Service.Application.Interfaces.Services;
using KSS.Service.Application.Mappings;
using MediatR;
using Microsoft.Extensions.Logging;

namespace KSS.Service.Application.Features.FuturesOrder.Commands;

public class NewLimitBuyOrderCommandHandler : IRequestHandler<NewLimitBuyOrderCommand, NewLimitBuyOrderResponse>
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

    public async Task<NewLimitBuyOrderResponse> Handle(NewLimitBuyOrderCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var order = await _futuresOrderService.NewOrderAsync(
                symbol: request.Symbol,
                side: "Buy",
                type: "Limit",
                quantity: request.Quantity,
                price: request.Price,
                clientOrderId: request.ClientOrderId,
                cancellationToken: cancellationToken);

            if (order == null)
            {
                return new NewLimitBuyOrderResponse
                {
                    Success = false,
                    ErrorMessage = "Failed to create limit buy order"
                };
            }

            return new NewLimitBuyOrderResponse
            {
                Success = true,
                Order = FuturesOrderMapper.ToDto(order)
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating limit buy order. Symbol: {Symbol}", request.Symbol);
            
            return new NewLimitBuyOrderResponse
            {
                Success = false,
                ErrorMessage = "An error occurred while creating the order"
            };
        }
    }
}

