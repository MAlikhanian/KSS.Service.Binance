using Binance.Net.Objects.Models.Futures;
using KSS.Service.Domain.Entities;
using KSS.Service.Domain.Enums;

namespace KSS.Service.Infrastructure.Mappings;

public static class BinanceToDomainMapper
{
    public static FuturesOrder ToDomainEntity(BinanceFuturesOrder binanceOrder)
    {
        if (binanceOrder == null)
            throw new ArgumentNullException(nameof(binanceOrder));

        var order = FuturesOrder.Create(
            orderId: binanceOrder.Id,
            symbol: binanceOrder.Symbol,
            side: ToDomainSide(binanceOrder.Side),
            type: ToDomainType(binanceOrder.Type),
            quantity: binanceOrder.Quantity,
            price: binanceOrder.Price,
            clientOrderId: binanceOrder.ClientOrderId,
            stopPrice: binanceOrder.StopPrice,
            timeInForce: ToDomainTimeInForce(binanceOrder.TimeInForce));

        // Update status and execution details first
        order.UpdateStatus(ToDomainStatus(binanceOrder.Status));
        order.UpdateExecution(binanceOrder.QuantityFilled, binanceOrder.AveragePrice);
        
        // Set timestamps from Binance (preserving the original CreateTime and UpdateTime from exchange)
        order.SetTimestamps(binanceOrder.CreateTime, binanceOrder.UpdateTime);

        return order;
    }

    private static OrderSide ToDomainSide(Binance.Net.Enums.OrderSide binanceSide)
    {
        return binanceSide == Binance.Net.Enums.OrderSide.Buy
            ? OrderSide.Buy
            : OrderSide.Sell;
    }

    private static OrderType ToDomainType(Binance.Net.Enums.FuturesOrderType binanceType)
    {
        return binanceType switch
        {
            Binance.Net.Enums.FuturesOrderType.Market => OrderType.Market,
            Binance.Net.Enums.FuturesOrderType.Limit => OrderType.Limit,
            Binance.Net.Enums.FuturesOrderType.Stop => OrderType.Stop,
            Binance.Net.Enums.FuturesOrderType.StopMarket => OrderType.StopMarket,
            Binance.Net.Enums.FuturesOrderType.TakeProfit => OrderType.TakeProfit,
            Binance.Net.Enums.FuturesOrderType.TakeProfitMarket => OrderType.TakeProfitMarket,
            Binance.Net.Enums.FuturesOrderType.TrailingStopMarket => OrderType.TrailingStopMarket,
            _ => OrderType.Unknown
        };
    }

    private static OrderStatus ToDomainStatus(Binance.Net.Enums.OrderStatus binanceStatus)
    {
        return binanceStatus switch
        {
            Binance.Net.Enums.OrderStatus.New => OrderStatus.New,
            Binance.Net.Enums.OrderStatus.PartiallyFilled => OrderStatus.PartiallyFilled,
            Binance.Net.Enums.OrderStatus.Filled => OrderStatus.Filled,
            Binance.Net.Enums.OrderStatus.Canceled => OrderStatus.Canceled,
            Binance.Net.Enums.OrderStatus.PendingCancel => OrderStatus.PendingCancel,
            Binance.Net.Enums.OrderStatus.Rejected => OrderStatus.Rejected,
            Binance.Net.Enums.OrderStatus.Expired => OrderStatus.Expired,
            _ => OrderStatus.Unknown
        };
    }

    private static TimeInForce? ToDomainTimeInForce(Binance.Net.Enums.TimeInForce? binanceTimeInForce)
    {
        if (!binanceTimeInForce.HasValue)
            return null;

        return binanceTimeInForce.Value switch
        {
            Binance.Net.Enums.TimeInForce.GoodTillCanceled => TimeInForce.GoodTillCanceled,
            Binance.Net.Enums.TimeInForce.ImmediateOrCancel => TimeInForce.ImmediateOrCancel,
            Binance.Net.Enums.TimeInForce.FillOrKill => TimeInForce.FillOrKill,
            Binance.Net.Enums.TimeInForce.GoodTillCrossing => TimeInForce.GoodTillCrossing,
            Binance.Net.Enums.TimeInForce.GoodTillExpiredOrCanceled => TimeInForce.GoodTillExpiredOrCanceled,
            _ => TimeInForce.Unknown
        };
    }
}

