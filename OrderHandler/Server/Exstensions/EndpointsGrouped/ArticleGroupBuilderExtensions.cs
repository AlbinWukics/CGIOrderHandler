using OrderHandler.Server.Endpoints.Requests.Article;

namespace OrderHandler.Server.Exstensions.EndpointsGrouped;

public static class ArticleGroupBuilderExtensions
{
    public static RouteGroupBuilder MapArticleGroup(this RouteGroupBuilder builder)
    {
        builder.MediatePost<AddArticleRequest>("/");
        builder.MediateGet<GetArticleByIdRequest>("/{id}");
        builder.MediateGet<GetAllArticlesRequest>("/");
        builder.MediatePut<UpdateArticleRequest>("/");

        return builder;
    }
}