namespace OrderHandler.DomainCommons.Services.Enums;

public enum OrderStatus
{
    New = 0,
    Processing = 1,
    AwaitingShipment = 2,
    Shipped = 3
}