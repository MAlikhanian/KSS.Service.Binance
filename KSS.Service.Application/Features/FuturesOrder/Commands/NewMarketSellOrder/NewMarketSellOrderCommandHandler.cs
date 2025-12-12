using KSS.Service.Application.Interfaces.Services;
using KSS.Service.Application.Mappings;
using MediatR;
using Microsoft.Extensions.Logging;

namespace KSS.Service.Application.Features.FuturesOrder.Commands;

public class NewMarketSellOrderCommandHandler : IRequestHandler<NewMarketSellOrderCommand, NewMarketSellOrderResponse>
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

    public async Task<NewMarketSellOrderResponse> Handle(NewMarketSellOrderCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var order = await _futuresOrderService.NewOrderAsync(
                symbol: request.Symbol,
                side: "Sell",
                type: "Market",
                quantity: request.Quantity,
                price: null,
                clientOrderId: request.ClientOrderId,
                cancellationToken: cancellationToken);

            if (order == null)
            {
                return new NewMarketSellOrderResponse
                {
                    Success = false,
                    ErrorMessage = "Failed to create market sell order"
                };
            }

            return new NewMarketSellOrderResponse
            {
                Success = true,
                Order = FuturesOrderMapper.ToDto(order)
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating market sell order. Symbol: {Symbol}", request.Symbol);
            
            return new NewMarketSellOrderResponse
            {
                Success = false,
                ErrorMessage = "An error occurred while creating the order"
            };
        }
    }
}

