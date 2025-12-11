namespace KSS.Service.Domain.Enums;

public enum OrderStatus
{
    New,
    PartiallyFilled,
    Filled,
    Canceled,
    PendingCancel,
    Rejected,
    Expired,
    Unknown
}

