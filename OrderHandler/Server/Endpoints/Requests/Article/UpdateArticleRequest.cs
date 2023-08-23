using OrderHandler.DomainCommons.DataTransferObjects;

namespace OrderHandler.Server.Endpoints.Requests.Article;

public class UpdateArticleRequest : IHttpRequest
{
    public ArticleDto Article { get; set; } = null!;
}