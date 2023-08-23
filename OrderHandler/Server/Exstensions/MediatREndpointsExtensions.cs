using MediatR;
using OrderHandler.Server.Endpoints.Requests;

namespace OrderHandler.Server.Exstensions;

public static class MediatREndpointsExtensions
{
    public static WebApplication MediatePost<TRequest>(
        this WebApplication app,
        string path)
        where TRequest : IHttpRequest
    {
        app.MapPost(path, async (IMediator mediator, [AsParameters] TRequest request) => await mediator.Send(request));
        return app;
    }

    public static WebApplication MediateGet<TRequest>(
        this WebApplication app,
        string path)
        where TRequest : IHttpRequest
    {
        app.MapGet(path, async (IMediator mediator, [AsParameters] TRequest request) => await mediator.Send(request));
        return app;
    }

    public static WebApplication MediatePut<TRequest>(
        this WebApplication app,
        string path)
        where TRequest : IHttpRequest
    {
        app.MapPut(path, async (IMediator mediator, [AsParameters] TRequest request) => await mediator.Send(request));
        return app;
    }

    public static WebApplication MediateDelete<TRequest>(
        this WebApplication app,
        string path)
        where TRequest : IHttpRequest
    {
        app.MapDelete(path, async (IMediator mediator, [AsParameters] TRequest request) => await mediator.Send(request));
        return app;
    }
}