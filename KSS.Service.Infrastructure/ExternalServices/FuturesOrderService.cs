using Binance.Net.Clients;
using Binance.Net.Objects.Options;
using KSS.Service.Application.DTOs;
using KSS.Service.Application.Interfaces.Services;
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

    public async Task<FuturesOrderDto?> GetOrderAsync(
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

            var order = result.Data;
            return new FuturesOrderDto
            {
                OrderId = order.Id,
                Symbol = order.Symbol,
                Status = order.Status.ToString(),
                Side = order.Side.ToString(),
                Type = order.Type.ToString(),
                Price = order.Price,
                Quantity = order.Quantity,
                ExecutedQuantity = order.QuantityFilled,
                AveragePrice = order.AveragePrice,
                CreateTime = order.CreateTime,
                UpdateTime = order.UpdateTime,
                ClientOrderId = order.ClientOrderId,
                StopPrice = order.StopPrice,
                TimeInForce = order.TimeInForce.ToString()
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Exception occurred while getting order. Symbol: {Symbol}, OrderId: {OrderId}, ClientOrderId: {ClientOrderId}",
                symbol, orderId, clientOrderId);
            throw;
        }
    }
}

