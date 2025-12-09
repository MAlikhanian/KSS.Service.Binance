using MediatR;

namespace KSS.Service.Application.Features.FuturesOrder.Queries.GetOrder;

public class GetOrderQuery : IRequest<GetOrderResponse>
{
    public string Symbol { get; set; } = string.Empty;
    public long? OrderId { get; set; }
    public string? ClientOrderId { get; set; }
}

