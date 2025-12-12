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
            var serviceRequest = new NewOrderCommand
            {
                Symbol = request.Symbol,
                Side = "Buy",
                Type = "Market",
                Quantity = request.Quantity,
                Price = null,
                ClientOrderId = request.ClientOrderId
            };

            var order = await _futuresOrderService.NewOrderAsync(
                serviceRequest,
                cancellationToken);

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

