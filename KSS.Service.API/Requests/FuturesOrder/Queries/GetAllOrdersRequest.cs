namespace KSS.Service.API.Requests.FuturesOrder.Queries;

public class GetAllOrdersRequest
{
    public string Symbol { get; set; } = string.Empty;
    public int? Limit { get; set; }
}

