namespace OrderHandler.Server.Endpoints.Requests.Article;

public class GetManyByArticleNumberRequest : IHttpRequest
{
    public int ArticleNumber { get; set; }
}