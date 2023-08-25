namespace OrderHandler.Server.Endpoints.Requests.Order;

public class GetOrderByIdRequest : IHttpRequest
{
    public Guid Id { get; set; }
}