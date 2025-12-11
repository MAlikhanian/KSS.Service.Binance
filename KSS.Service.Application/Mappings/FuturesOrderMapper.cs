using KSS.Service.Application.DTOs;
using KSS.Service.Domain.Entities;
using KSS.Service.Domain.Enums;

namespace KSS.Service.Application.Mappings;

public static class FuturesOrderMapper
{
    public static FuturesOrderDto ToDto(FuturesOrder order)
    {
        if (order == null)
            throw new ArgumentNullException(nameof(order));

        return new FuturesOrderDto
        {
            OrderId = order.OrderId,
            Symbol = order.Symbol,
            Status = order.Status.ToString(),
            Side = order.Side.ToString(),
            Type = order.Type.ToString(),
            Price = order.Price,
            Quantity = order.Quantity,
            ExecutedQuantity = order.ExecutedQuantity,
            AveragePrice = order.AveragePrice,
            CreateTime = order.CreateTime,
            UpdateTime = order.UpdateTime,
            ClientOrderId = order.ClientOrderId,
            StopPrice = order.StopPrice,
            TimeInForce = order.TimeInForce?.ToString()
        };
    }

    public static List<FuturesOrderDto> ToDtoList(IEnumerable<FuturesOrder> orders)
    {
        return orders.Select(ToDto).ToList();
    }

    public static OrderSide ToDomainSide(string side)
    {
        return side.Equals("Buy", StringComparison.OrdinalIgnoreCase)
            ? OrderSide.Buy
            : OrderSide.Sell;
    }

    public static OrderType ToDomainType(string type)
    {
        return type.ToUpperInvariant() switch
        {
            "MARKET" => OrderType.Market,
            "LIMIT" => OrderType.Limit,
            "STOP" => OrderType.Stop,
            "STOPMARKET" or "STOP_MARKET" => OrderType.StopMarket,
            "TAKEPROFIT" or "TAKE_PROFIT" => OrderType.TakeProfit,
            "TAKEPROFITMARKET" or "TAKE_PROFIT_MARKET" => OrderType.TakeProfitMarket,
            "TRAILINGSTOPMARKET" or "TRAILING_STOP_MARKET" => OrderType.TrailingStopMarket,
            _ => OrderType.Unknown
        };
    }

    public static OrderStatus ToDomainStatus(string status)
    {
        return status.ToUpperInvariant() switch
        {
            "NEW" => OrderStatus.New,
            "PARTIALLY_FILLED" or "PARTIALLYFILLED" => OrderStatus.PartiallyFilled,
            "FILLED" => OrderStatus.Filled,
            "CANCELED" or "CANCELLED" => OrderStatus.Canceled,
            "PENDING_CANCEL" or "PENDINGCANCEL" => OrderStatus.PendingCancel,
            "REJECTED" => OrderStatus.Rejected,
            "EXPIRED" => OrderStatus.Expired,
            _ => OrderStatus.Unknown
        };
    }

    public static TimeInForce? ToDomainTimeInForce(string? timeInForce)
    {
        if (string.IsNullOrWhiteSpace(timeInForce))
            return null;

        return timeInForce.ToUpperInvariant() switch
        {
            "GTC" or "GOODTILLCANCELED" or "GOOD_TILL_CANCELED" => TimeInForce.GoodTillCanceled,
            "IOC" or "IMMEDIATEORCANCEL" or "IMMEDIATE_OR_CANCEL" => TimeInForce.ImmediateOrCancel,
            "FOK" or "FILLORKILL" or "FILL_OR_KILL" => TimeInForce.FillOrKill,
            "POST_ONLY" or "POSTONLY" => TimeInForce.PostOnly,
            "GTX" or "GOODTILLCROSSING" or "GOOD_TILL_CROSSING" => TimeInForce.GoodTillCrossing,
            "GTE" or "GOODTILLEXPIREDORCANCELED" or "GOOD_TILL_EXPIRED_OR_CANCELED" => TimeInForce.GoodTillExpiredOrCanceled,
            _ => TimeInForce.Unknown
        };
    }
}

