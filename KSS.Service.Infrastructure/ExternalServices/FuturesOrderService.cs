using Binance.Net.Clients;
using Binance.Net.Objects.Options;
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
        string symbol,
        long? orderId = null,
        string? clientOrderId = null,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var result = await _exchangeClient.UsdFuturesApi.Trading.GetOrderAsync(
                symbol: symbol,
                orderId: orderId,
                origClientOrderId: clientOrderId,
                ct: cancellationToken);

            if (!result.Success || result.Data == null)
            {
                _logger.LogWarning("Failed to get order. Symbol: {Symbol}, OrderId: {OrderId}, ClientOrderId: {ClientOrderId}, Error: {Error}",
                    symbol, orderId, clientOrderId, result.Error?.Message);
                return null;
            }

            return BinanceToDomainMapper.ToDomainEntity(result.Data);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Exception occurred while getting order. Symbol: {Symbol}, OrderId: {OrderId}, ClientOrderId: {ClientOrderId}",
                symbol, orderId, clientOrderId);
            throw;
        }
    }

    public async Task<Domain.Entities.FuturesOrder?> NewOrderAsync(
        string symbol,
        string side,
        string type,
        decimal quantity,
        decimal? price = null,
        string? clientOrderId = null,
        CancellationToken cancellationToken = default)
    {
        try
        {
            // Parse side enum
            var orderSide = side.Equals("Buy", StringComparison.OrdinalIgnoreCase) 
                ? Binance.Net.Enums.OrderSide.Buy 
                : Binance.Net.Enums.OrderSide.Sell;

            // Parse order type enum
            Binance.Net.Enums.FuturesOrderType orderType;
            if (type.Equals("Market", StringComparison.OrdinalIgnoreCase))
            {
                orderType = Binance.Net.Enums.FuturesOrderType.Market;
            }
            else if (type.Equals("Limit", StringComparison.OrdinalIgnoreCase))
            {
                orderType = Binance.Net.Enums.FuturesOrderType.Limit;
            }
            else
            {
                _logger.LogWarning("Unsupported order type: {Type}", type);
                return null;
            }

            // Place order - only include price for Limit orders
            var result = orderType == Binance.Net.Enums.FuturesOrderType.Limit
                ? await _exchangeClient.UsdFuturesApi.Trading.PlaceOrderAsync(
                    symbol: symbol,
                    side: orderSide,
                    type: orderType,
                    quantity: quantity,
                    price: price ?? throw new ArgumentException("Price is required for Limit orders", nameof(price)),
                    newClientOrderId: clientOrderId,
                    timeInForce: Binance.Net.Enums.TimeInForce.GoodTillCanceled,
                    ct: cancellationToken)
                : await _exchangeClient.UsdFuturesApi.Trading.PlaceOrderAsync(
                    symbol: symbol,
                    side: orderSide,
                    type: orderType,
                    quantity: quantity,
                    newClientOrderId: clientOrderId,
                    ct: cancellationToken);

            if (!result.Success || result.Data == null)
            {
                _logger.LogWarning("Failed to create order. Symbol: {Symbol}, Side: {Side}, Type: {Type}, Error: {Error}",
                    symbol, side, type, result.Error?.Message);
                return null;
            }

            return BinanceToDomainMapper.ToDomainEntity(result.Data);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Exception occurred while creating order. Symbol: {Symbol}, Side: {Side}, Type: {Type}",
                symbol, side, type);
            throw;
        }
    }
}

