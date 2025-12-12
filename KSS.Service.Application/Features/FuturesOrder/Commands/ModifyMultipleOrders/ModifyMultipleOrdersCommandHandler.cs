using MediatR;
using Microsoft.Extensions.Logging;

namespace KSS.Service.Application.Features.FuturesOrder.Commands;

public class ModifyMultipleOrdersCommandHandler : IRequestHandler<ModifyMultipleOrdersCommand, ModifyMultipleOrdersResponse>
{
    private readonly ILogger<ModifyMultipleOrdersCommandHandler> _logger;

    public ModifyMultipleOrdersCommandHandler(ILogger<ModifyMultipleOrdersCommandHandler> logger)
    {
        _logger = logger;
    }

    public async Task<ModifyMultipleOrdersResponse> Handle(ModifyMultipleOrdersCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await Task.CompletedTask;
            return new ModifyMultipleOrdersResponse { Success = true };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error modifying multiple orders. Symbol: {Symbol}", request.Symbol);
            return new ModifyMultipleOrdersResponse { Success = false, ErrorMessage = "An error occurred while modifying orders" };
        }
    }
}

