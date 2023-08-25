using OrderHandler.Server.Extensions.EndpointsGrouped;

namespace OrderHandler.Server.Extensions;

public static class WebApplicationEndpointExtensions
{
    public static WebApplication MapEndpoints(this WebApplication app)
    {
        app.MapGroup("/article").MapArticleGroup().WithTags("Articles");
        app.MapGroup("/order").MapOrderGroup().WithTags("Orders");
        app.MapGroup("/orderRow").MapOrderRowGroup().WithTags("OrderRows");

        return app;
    }
}