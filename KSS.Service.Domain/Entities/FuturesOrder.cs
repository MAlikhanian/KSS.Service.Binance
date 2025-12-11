using KSS.Service.Domain.Enums;

namespace KSS.Service.Domain.Entities;

public class FuturesOrder
{
    public long OrderId { get; private set; }
    public string Symbol { get; private set; }
    public OrderStatus Status { get; private set; }
    public OrderSide Side { get; private set; }
    public OrderType Type { get; private set; }
    public decimal Price { get; private set; }
    public decimal Quantity { get; private set; }
    public decimal ExecutedQuantity { get; private set; }
    public decimal? AveragePrice { get; private set; }
    public DateTime CreateTime { get; private set; }
    public DateTime? UpdateTime { get; private set; }
    public string? ClientOrderId { get; private set; }
    public decimal? StopPrice { get; private set; }
    public TimeInForce? TimeInForce { get; private set; }

    // Private constructor for EF Core or deserialization
    private FuturesOrder()
    {
        Symbol = string.Empty;
    }

    // Factory method for creating new orders
    public static FuturesOrder Create(
        long orderId,
        string symbol,
        OrderSide side,
        OrderType type,
        decimal quantity,
        decimal price,
        string? clientOrderId = null,
        decimal? stopPrice = null,
        TimeInForce? timeInForce = null)
    {
        if (string.IsNullOrWhiteSpace(symbol))
            throw new ArgumentException("Symbol cannot be null or empty", nameof(symbol));

        if (quantity <= 0)
            throw new ArgumentException("Quantity must be greater than zero", nameof(quantity));

        if (type == OrderType.Limit && price <= 0)
            throw new ArgumentException("Price must be greater than zero for Limit orders", nameof(price));

        return new FuturesOrder
        {
            OrderId = orderId,
            Symbol = symbol,
            Status = OrderStatus.New,
            Side = side,
            Type = type,
            Price = price,
            Quantity = quantity,
            ExecutedQuantity = 0,
            CreateTime = DateTime.UtcNow,
            ClientOrderId = clientOrderId,
            StopPrice = stopPrice,
            TimeInForce = timeInForce
        };
    }

    // Business logic methods
    public bool CanCancel()
    {
        return Status == OrderStatus.New || 
               Status == OrderStatus.PartiallyFilled ||
               Status == OrderStatus.PendingCancel;
    }

    public bool IsFilled()
    {
        return Status == OrderStatus.Filled;
    }

    public bool IsActive()
    {
        return Status == OrderStatus.New || 
               Status == OrderStatus.PartiallyFilled ||
               Status == OrderStatus.PendingCancel;
    }

    public void UpdateStatus(OrderStatus newStatus)
    {
        if (Status == OrderStatus.Filled || Status == OrderStatus.Canceled)
            throw new InvalidOperationException($"Cannot update status of a {Status} order");

        Status = newStatus;
        UpdateTime = DateTime.UtcNow;
    }

    public void UpdateExecution(decimal executedQuantity, decimal? averagePrice)
    {
        if (executedQuantity < 0 || executedQuantity > Quantity)
            throw new ArgumentException("Executed quantity is invalid", nameof(executedQuantity));

        ExecutedQuantity = executedQuantity;
        AveragePrice = averagePrice;
        UpdateTime = DateTime.UtcNow;

        if (ExecutedQuantity == Quantity)
        {
            Status = OrderStatus.Filled;
        }
        else if (ExecutedQuantity > 0)
        {
            Status = OrderStatus.PartiallyFilled;
        }
    }

    public decimal GetRemainingQuantity()
    {
        return Quantity - ExecutedQuantity;
    }

    public decimal? GetTotalValue()
    {
        if (AveragePrice.HasValue && ExecutedQuantity > 0)
        {
            return AveragePrice.Value * ExecutedQuantity;
        }
        return null;
    }

    // Method to set timestamps when mapping from external sources
    public void SetTimestamps(DateTime createTime, DateTime? updateTime = null)
    {
        CreateTime = createTime;
        if (updateTime.HasValue)
        {
            UpdateTime = updateTime.Value;
        }
    }
}

