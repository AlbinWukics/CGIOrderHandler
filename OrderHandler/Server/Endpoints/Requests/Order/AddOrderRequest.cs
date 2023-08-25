using OrderHandler.DomainCommons.DataTransferObjects;

namespace OrderHandler.Server.Endpoints.Requests.Order;

public class AddOrderRequest : IHttpRequest
{
    public OrderDto Order { get; set; } = null!;
}