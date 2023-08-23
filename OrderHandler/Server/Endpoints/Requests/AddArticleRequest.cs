using OrderHandler.DomainCommons.DataTransferObjects;

namespace OrderHandler.Server.Endpoints.Requests;

public class AddArticleRequest : IHttpRequest
{
    public ArticleDto Article { get; set; } = null!;
}