using KSS.Service.API.Requests.FuturesOrder.Commands;
using KSS.Service.Application.Features.FuturesOrder.Commands;
using Mapster;

namespace KSS.Service.API.Mappings;

public static class MapsterConfig
{
    public static void ConfigureMappings()
    {
        // Map OrderRequestDto to OrderRequest
        TypeAdapterConfig<OrderRequestDto, OrderRequest>.NewConfig();
        
        // Map OrderModificationRequest to OrderModification
        TypeAdapterConfig<OrderModificationRequest, OrderModification>.NewConfig();
    }
}

