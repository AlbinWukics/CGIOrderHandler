namespace OrderHandler.Server.Endpoints.Requests.Article;

public class GetArticleByIdRequest : IHttpRequest
{
    public Guid Id { get; set; }
}