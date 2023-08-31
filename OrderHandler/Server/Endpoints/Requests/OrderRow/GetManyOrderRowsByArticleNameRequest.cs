namespace OrderHandler.Server.Endpoints.Requests.OrderRow;

public class GetManyOrderRowsByArticleNameRequest : IHttpRequest
{
    public string ArticleName { get; set; } = string.Empty;
}