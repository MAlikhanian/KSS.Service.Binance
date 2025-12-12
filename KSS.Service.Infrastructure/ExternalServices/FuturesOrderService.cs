using Binance.Net.Clients;
using Binance.Net.Objects.Options;
using KSS.Service.Application.Features.FuturesOrder.Commands;
using KSS.Service.Application.Features.FuturesOrder.Queries;
using KSS.Service.Application.Interfaces.Services;
using KSS.Service.Infrastructure.Mappings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace KSS.Service.Infrastructure.ExternalServices;

public class FuturesOrderService : IFuturesOrderService
{
    private readonly BinanceRestClient _exchangeClient;
    private readonly ILogger<FuturesOrderService> _logger;

    public FuturesOrderService(ILogger<FuturesOrderService> logger, IConfiguration configuration)
    {
        _logger = logger;
        
        var apiKey = configuration["Exchange:ApiKey"];
        var apiSecret = configuration["Exchange:ApiSecret"];
        
        _exchangeClient = new BinanceRestClient(options =>
        {
            if (!string.IsNullOrEmpty(apiKey) && !string.IsNullOrEmpty(apiSecret))
            {
                options.ApiCredentials = new CryptoExchange.Net.Authentication.ApiCredentials(apiKey, apiSecret);
            }
        });
    }

    public async Task<Domain.Entities.FuturesOrder?> GetOrderAsync(
        GetOrderQuery request,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var result = await _exchangeClient.UsdFuturesApi.Trading.GetOrderAsync(
                symbol: request.Symbol,
                orderId: request.OrderId,
                origClientOrderId: request.ClientOrderId,
                ct: cancellationToken);

            if (!result.Success || result.Data == null)
            {
                _logger.LogWarning("Failed to get order. Symbol: {Symbol}, OrderId: {OrderId}, ClientOrderId: {ClientOrderId}, Error: {Error}",
                    request.Symbol, request.OrderId, request.ClientOrderId, result.Error?.Message);
                return null;
            }

            return BinanceToDomainMapper.ToDomainEntity(result.Data);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Exception occurred while getting order. Symbol: {Symbol}, OrderId: {OrderId}, ClientOrderId: {ClientOrderId}",
                request.Symbol, request.OrderId, request.ClientOrderId);
            throw;
        }
    }

    public async Task<Domain.Entities.FuturesOrder?> NewOrderAsync(
        NewOrderCommand request,
        CancellationToken cancellationToken = default)
    {
        try
        {
            // Parse side enum
            var orderSide = request.Side.Equals("Buy", StringComparison.OrdinalIgnoreCase) 
                ? Binance.Net.Enums.OrderSide.Buy 
                : Binance.Net.Enums.OrderSide.Sell;

            // Parse order type enum
            Binance.Net.Enums.FuturesOrderType orderType;
            if (request.Type.Equals("Market", StringComparison.OrdinalIgnoreCase))
            {
                orderType = Binance.Net.Enums.FuturesOrderType.Market;
            }
            else if (request.Type.Equals("Limit", StringComparison.OrdinalIgnoreCase))
            {
                orderType = Binance.Net.Enums.FuturesOrderType.Limit;
            }
            else
            {
                _logger.LogWarning("Unsupported order type: {Type}", request.Type);
                return null;
            }

            // Place order - only include price for Limit orders
            var result = orderType == Binance.Net.Enums.FuturesOrderType.Limit
                ? await _exchangeClient.UsdFuturesApi.Trading.PlaceOrderAsync(
                    symbol: request.Symbol,
                    side: orderSide,
                    type: orderType,
                    quantity: request.Quantity,
                    price: request.Price ?? throw new ArgumentException("Price is required for Limit orders", nameof(request.Price)),
                    newClientOrderId: request.ClientOrderId,
                    timeInForce: Binance.Net.Enums.TimeInForce.GoodTillCanceled,
                    ct: cancellationToken)
                : await _exchangeClient.UsdFuturesApi.Trading.PlaceOrderAsync(
                    symbol: request.Symbol,
                    side: orderSide,
                    type: orderType,
                    quantity: request.Quantity,
                    newClientOrderId: request.ClientOrderId,
                    ct: cancellationToken);

            if (!result.Success || result.Data == null)
            {
                _logger.LogWarning("Failed to create order. Symbol: {Symbol}, Side: {Side}, Type: {Type}, Error: {Error}",
                    request.Symbol, request.Side, request.Type, result.Error?.Message);
                return null;
            }

            return BinanceToDomainMapper.ToDomainEntity(result.Data);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Exception occurred while creating order. Symbol: {Symbol}, Side: {Side}, Type: {Type}",
                request.Symbol, request.Side, request.Type);
            throw;
        }
    }
}

