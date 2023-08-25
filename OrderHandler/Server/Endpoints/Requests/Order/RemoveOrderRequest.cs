namespace OrderHandler.Server.Endpoints.Requests.Order;

public class RemoveOrderRequest : IHttpRequest
{
    public Guid Id { get; set; }
}