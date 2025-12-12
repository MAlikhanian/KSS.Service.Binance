namespace KSS.Service.API.Requests.FuturesOrder.Commands;

public record CancelMultipleOrdersRequest(
    string Symbol,
    List<long> OrderIds);

