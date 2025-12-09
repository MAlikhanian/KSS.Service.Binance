namespace KSS.Service.Application.Features.FuturesOrder.Commands.CancelAllOpenOrders;

public class CancelAllOpenOrdersResponse
{
    public int CancelledCount { get; set; }
    public bool Success { get; set; }
    public string? ErrorMessage { get; set; }
}

