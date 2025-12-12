using KSS.Service.Application.DTOs;

namespace KSS.Service.Application.Features.FuturesOrder.Commands;

public class NewMarketBuyOrderResponse
{
    public FuturesOrderDto? Order { get; set; }
    public bool Success { get; set; }
    public string? ErrorMessage { get; set; }
}

