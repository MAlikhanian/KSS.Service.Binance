using KSS.Service.Application.DTOs;

namespace KSS.Service.Application.Features.FuturesOrder.Commands;

public class ModifyMultipleOrdersResponse
{
    public List<FuturesOrderDto> Orders { get; set; } = new();
    public bool Success { get; set; }
    public string? ErrorMessage { get; set; }
}

