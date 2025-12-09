using MediatR;
using Microsoft.Extensions.Logging;

namespace KSS.Service.Application.Features.FuturesOrder.Commands.NewMultipleOrders;

public class NewMultipleOrdersCommandHandler : IRequestHandler<NewMultipleOrdersCommand, NewMultipleOrdersResponse>
{
    private readonly ILogger<NewMultipleOrdersCommandHandler> _logger;

    public NewMultipleOrdersCommandHandler(ILogger<NewMultipleOrdersCommandHandler> logger)
    {
        _logger = logger;
    }

    public async Task<NewMultipleOrdersResponse> Handle(NewMultipleOrdersCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await Task.CompletedTask;
            return new NewMultipleOrdersResponse { Success = true };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating multiple orders");
            return new NewMultipleOrdersResponse { Success = false, ErrorMessage = "An error occurred while creating orders" };
        }
    }
}

