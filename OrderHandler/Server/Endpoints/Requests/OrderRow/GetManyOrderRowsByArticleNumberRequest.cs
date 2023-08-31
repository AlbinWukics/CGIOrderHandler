namespace OrderHandler.Server.Endpoints.Requests.OrderRow;

public class GetManyOrderRowsByArticleNumberRequest : IHttpRequest
{
    public int ArticleNumber { get; set; }
}