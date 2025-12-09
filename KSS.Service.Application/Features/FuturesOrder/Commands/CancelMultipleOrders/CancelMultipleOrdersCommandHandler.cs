using MediatR;
using Microsoft.Extensions.Logging;

namespace KSS.Service.Application.Features.FuturesOrder.Commands.CancelMultipleOrders;

public class CancelMultipleOrdersCommandHandler : IRequestHandler<CancelMultipleOrdersCommand, CancelMultipleOrdersResponse>
{
    private readonly ILogger<CancelMultipleOrdersCommandHandler> _logger;

    public CancelMultipleOrdersCommandHandler(ILogger<CancelMultipleOrdersCommandHandler> logger)
    {
        _logger = logger;
    }

    public async Task<CancelMultipleOrdersResponse> Handle(CancelMultipleOrdersCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await Task.CompletedTask;
            return new CancelMultipleOrdersResponse { Success = true };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error canceling multiple orders. Symbol: {Symbol}", request.Symbol);
            return new CancelMultipleOrdersResponse { Success = false, ErrorMessage = "An error occurred while canceling orders" };
        }
    }
}

