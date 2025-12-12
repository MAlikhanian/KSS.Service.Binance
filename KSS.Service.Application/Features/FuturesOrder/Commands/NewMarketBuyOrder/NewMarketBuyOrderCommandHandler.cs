using KSS.Service.Application.Interfaces.Services;
using KSS.Service.Application.Mappings;
using MediatR;
using Microsoft.Extensions.Logging;

namespace KSS.Service.Application.Features.FuturesOrder.Commands;

public class NewMarketBuyOrderCommandHandler : IRequestHandler<NewMarketBuyOrderCommand, NewMarketBuyOrderResponse>
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

    public async Task<NewMarketBuyOrderResponse> Handle(NewMarketBuyOrderCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var order = await _futuresOrderService.NewOrderAsync(
                symbol: request.Symbol,
                side: "Buy",
                type: "Market",
                quantity: request.Quantity,
                price: null,
                clientOrderId: request.ClientOrderId,
                cancellationToken: cancellationToken);

            if (order == null)
            {
                return new NewMarketBuyOrderResponse
                {
                    Success = false,
                    ErrorMessage = "Failed to create market buy order"
                };
            }

            return new NewMarketBuyOrderResponse
            {
                Success = true,
                Order = FuturesOrderMapper.ToDto(order)
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating market buy order. Symbol: {Symbol}", request.Symbol);
            
            return new NewMarketBuyOrderResponse
            {
                Success = false,
                ErrorMessage = "An error occurred while creating the order"
            };
        }
    }
}

