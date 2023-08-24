namespace OrderHandler.DomainCommons.Services.Enums;

public enum OrderStatus
{
    NewOrder = 0,
    Processing = 1,
    AwaitingShipment = 2,
    Shipped = 3
}