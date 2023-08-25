namespace OrderHandler.Server.Endpoints.Requests.OrderRow;

public class GetManyByOrderRowNumberRequest : IHttpRequest
{
    public int OrderRowNumber { get; set; }
}