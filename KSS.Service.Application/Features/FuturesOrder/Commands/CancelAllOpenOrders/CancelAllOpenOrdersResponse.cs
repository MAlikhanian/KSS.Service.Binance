namespace KSS.Service.Application.Features.FuturesOrder.Commands;

public class CancelAllOpenOrdersResponse
{
    public int CancelledCount { get; set; }
    public bool Success { get; set; }
    public string? ErrorMessage { get; set; }
}

