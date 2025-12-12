using KSS.Service.Application.DTOs;

namespace KSS.Service.Application.Features.FuturesOrder.Queries;

public class GetAllOrdersResponse
{
    public List<FuturesOrderDto> Orders { get; set; } = new();
    public bool Success { get; set; }
    public string? ErrorMessage { get; set; }
}

