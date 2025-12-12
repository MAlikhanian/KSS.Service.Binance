using MediatR;

namespace KSS.Service.Application.Features.FuturesOrder.Queries;

public class GetAllOrdersQuery : IRequest<GetAllOrdersResponse>
{
    public string Symbol { get; set; } = string.Empty;
    public int? Limit { get; set; }
}

