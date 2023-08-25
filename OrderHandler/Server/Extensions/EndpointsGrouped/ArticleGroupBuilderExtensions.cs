using OrderHandler.Server.Endpoints.Requests.Article;

namespace OrderHandler.Server.Extensions.EndpointsGrouped;

public static class ArticleGroupBuilderExtensions
{
    public static RouteGroupBuilder MapArticleGroup(this RouteGroupBuilder builder)
    {
        builder.MediatePost<AddArticleRequest>("/");
        builder.MediateGet<GetArticleByIdRequest>("/{id}");
        builder.MediateGet<GetAllArticlesRequest>("/");
        builder.MediateGet<GetManyByArticleNumberRequest>("/articleNumber/{articleNumber}");
        builder.MediatePut<UpdateArticleRequest>("/");
        builder.MediateDelete<RemoveArticleRequest>("/{id}");

        return builder;
    }
}