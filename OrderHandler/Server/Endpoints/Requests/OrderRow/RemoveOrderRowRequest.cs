namespace OrderHandler.Server.Endpoints.Requests.OrderRow;

public class RemoveOrderRowRequest : IHttpRequest
{
    public Guid Id { get; set; }
}