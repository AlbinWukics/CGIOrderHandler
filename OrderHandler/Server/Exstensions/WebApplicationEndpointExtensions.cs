using OrderHandler.Server.Exstensions.EndpointsGrouped;

namespace OrderHandler.Server.Exstensions;

public static class WebApplicationEndpointExtensions
{
    public static WebApplication MapEndpoints(this WebApplication app)
    {
        app.MapGroup("/article").MapArticleGroup().WithTags("Article");

        return app;
    }
}