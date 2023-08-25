using OrderHandler.DomainCommons.DataTransferObjects;

namespace OrderHandler.Server.Endpoints.Requests.Order;

public class UpdateOrderRequest : IHttpRequest
{
    public OrderDto Order { get; set; } = null!;
}