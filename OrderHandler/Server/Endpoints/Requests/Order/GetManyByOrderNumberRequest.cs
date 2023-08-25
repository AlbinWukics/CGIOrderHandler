namespace OrderHandler.Server.Endpoints.Requests.Order;

public class GetManyByOrderNumberRequest : IHttpRequest
{
    public int OrderNumber { get; set; }
}