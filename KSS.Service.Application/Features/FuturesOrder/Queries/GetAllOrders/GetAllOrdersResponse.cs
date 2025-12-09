using KSS.Service.Application.DTOs;

namespace KSS.Service.Application.Features.FuturesOrder.Queries.GetAllOrders;

public class GetAllOrdersResponse
{
    public List<FuturesOrderDto> Orders { get; set; } = new();
    public bool Success { get; set; }
    public string? ErrorMessage { get; set; }
}

