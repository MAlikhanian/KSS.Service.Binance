using KSS.Service.Application.DTOs;

namespace KSS.Service.Application.Features.FuturesOrder.Queries;

public class GetOrderResponse
{
    public FuturesOrderDto? Order { get; set; }
    public bool Success { get; set; }
    public string? ErrorMessage { get; set; }
}

