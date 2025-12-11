using KSS.Service.Application.Interfaces.Services;
using KSS.Service.Application.Mappings;
using MediatR;
using Microsoft.Extensions.Logging;

namespace KSS.Service.Application.Features.FuturesOrder.Commands.NewLimitSellOrder;

public class NewLimitSellOrderCommandHandler : IRequestHandler<NewLimitSellOrderCommand, NewLimitSellOrderResponse>
{
    private readonly IFuturesOrderService _futuresOrderService;
    private readonly ILogger<NewLimitSellOrderCommandHandler> _logger;

    public NewLimitSellOrderCommandHandler(
        IFuturesOrderService futuresOrderService,
        ILogger<NewLimitSellOrderCommandHandler> logger)
    {
        _futuresOrderService = futuresOrderService;
        _logger = logger;
    }

    public async Task<NewLimitSellOrderResponse> Handle(NewLimitSellOrderCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var order = await _futuresOrderService.NewOrderAsync(
                symbol: request.Symbol,
                side: "Sell",
                type: "Limit",
                quantity: request.Quantity,
                price: request.Price,
                clientOrderId: request.ClientOrderId,
                cancellationToken: cancellationToken);

            if (order == null)
            {
                return new NewLimitSellOrderResponse
                {
                    Success = false,
                    ErrorMessage = "Failed to create limit sell order"
                };
            }

            return new NewLimitSellOrderResponse
            {
                Success = true,
                Order = FuturesOrderMapper.ToDto(order)
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating limit sell order. Symbol: {Symbol}", request.Symbol);
            
            return new NewLimitSellOrderResponse
            {
                Success = false,
                ErrorMessage = "An error occurred while creating the order"
            };
        }
    }
}

