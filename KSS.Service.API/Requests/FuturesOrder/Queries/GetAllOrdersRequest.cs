namespace KSS.Service.API.Requests.FuturesOrder.Queries;

public record GetAllOrdersRequest(
    string Symbol,
    int? Limit = null);

