using MediatR;
using Microsoft.Extensions.Logging;

namespace KSS.Service.Application.Features.FuturesOrder.Commands.ModifyOrder;

public class ModifyOrderCommandHandler : IRequestHandler<ModifyOrderCommand, ModifyOrderResponse>
{
    private readonly ILogger<ModifyOrderCommandHandler> _logger;

    public ModifyOrderCommandHandler(ILogger<ModifyOrderCommandHandler> logger)
    {
        _logger = logger;
    }

    public async Task<ModifyOrderResponse> Handle(ModifyOrderCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await Task.CompletedTask;
            return new ModifyOrderResponse { Success = true };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error modifying order. Symbol: {Symbol}, OrderId: {OrderId}", request.Symbol, request.OrderId);
            return new ModifyOrderResponse { Success = false, ErrorMessage = "An error occurred while modifying the order" };
        }
    }
}

