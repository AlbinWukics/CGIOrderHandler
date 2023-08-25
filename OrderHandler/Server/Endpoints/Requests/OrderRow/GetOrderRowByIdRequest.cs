namespace OrderHandler.Server.Endpoints.Requests.OrderRow;

public class GetOrderRowByIdRequest : IHttpRequest
{
    public Guid Id { get; set; }
}