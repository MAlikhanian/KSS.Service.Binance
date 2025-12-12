using MediatR;
using Microsoft.Extensions.Logging;

namespace KSS.Service.Application.Features.FuturesOrder.Commands;

public class CancelAllOpenOrdersCommandHandler : IRequestHandler<CancelAllOpenOrdersCommand, CancelAllOpenOrdersResponse>
{
    private readonly ILogger<CancelAllOpenOrdersCommandHandler> _logger;

    public CancelAllOpenOrdersCommandHandler(ILogger<CancelAllOpenOrdersCommandHandler> logger)
    {
        _logger = logger;
    }

    public async Task<CancelAllOpenOrdersResponse> Handle(CancelAllOpenOrdersCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await Task.CompletedTask;
            return new CancelAllOpenOrdersResponse { Success = true };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error canceling all open orders. Symbol: {Symbol}", request.Symbol);
            return new CancelAllOpenOrdersResponse { Success = false, ErrorMessage = "An error occurred while canceling orders" };
        }
    }
}

