namespace KSS.Service.Application.DTOs;

public class FuturesOrderDto
{
    public long OrderId { get; set; }
    public string Symbol { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public string Side { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public decimal Quantity { get; set; }
    public decimal ExecutedQuantity { get; set; }
    public decimal? AveragePrice { get; set; }
    public DateTime CreateTime { get; set; }
    public DateTime? UpdateTime { get; set; }
    public string? ClientOrderId { get; set; }
    public decimal? StopPrice { get; set; }
    public string? TimeInForce { get; set; }
}

