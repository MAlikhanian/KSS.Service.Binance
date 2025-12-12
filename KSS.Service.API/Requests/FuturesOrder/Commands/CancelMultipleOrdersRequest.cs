namespace KSS.Service.API.Requests.FuturesOrder.Commands;

public class CancelMultipleOrdersRequest
{
    public string Symbol { get; set; } = string.Empty;
    public List<long> OrderIds { get; set; } = new();
}

