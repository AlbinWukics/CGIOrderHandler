namespace OrderHandler.Server.Endpoints.Requests.Article;

public class RemoveArticleRequest : IHttpRequest
{
    public Guid Id { get; set; }
}