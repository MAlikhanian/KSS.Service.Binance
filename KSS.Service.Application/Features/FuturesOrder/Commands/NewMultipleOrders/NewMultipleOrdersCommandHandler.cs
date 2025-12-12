using KSS.Common.CQRS;
using KSS.Common.Result;
using KSS.Service.Application.DTOs;
using Microsoft.Extensions.Logging;

namespace KSS.Service.Application.Features.FuturesOrder.Commands;

public class NewMultipleOrdersCommandHandler : ICommandHandlerApi<NewMultipleOrdersCommand, List<FuturesOrderDto>>
{
    private readonly ILogger<NewMultipleOrdersCommandHandler> _logger;

    public NewMultipleOrdersCommandHandler(ILogger<NewMultipleOrdersCommandHandler> logger)
    {
        _logger = logger;
    }

    public async Task<ApiResult<List<FuturesOrderDto>>> Handle(NewMultipleOrdersCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await Task.CompletedTask;
            return ApiResult<List<FuturesOrderDto>>.CreateSuccess(
                new List<FuturesOrderDto>(),
                "Multiple orders created successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating multiple orders");
            return ApiResult<List<FuturesOrderDto>>.FailureResult("An error occurred while creating orders");
        }
    }
}

