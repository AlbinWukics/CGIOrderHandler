using MediatR;
using OrderHandler.Server.Endpoints.Requests;

namespace OrderHandler.Server.Extensions;

public static class MinimalatRExtensions
{
    public static RouteGroupBuilder MediatePost<TRequest>(
        this RouteGroupBuilder builder,
        string pattern) where TRequest : IHttpRequest
    {
        builder.MapPost(pattern,
            async (IMediator mediator, [AsParameters] TRequest request) => await mediator.Send(request));
        return builder;
    }

    public static RouteGroupBuilder MediateGet<TRequest>(
        this RouteGroupBuilder builder,
        string pattern) where TRequest : IHttpRequest
    {
        builder.MapGet(pattern,
            async (IMediator mediator, [AsParameters] TRequest request) => await mediator.Send(request));
        return builder;
    }

    public static RouteGroupBuilder MediatePut<TRequest>(
        this RouteGroupBuilder builder,
        string pattern) where TRequest : IHttpRequest
    {
        builder.MapPut(pattern,
            async (IMediator mediator, [AsParameters] TRequest request) => await mediator.Send(request));
        return builder;
    }

    public static RouteGroupBuilder MediateDelete<TRequest>(
        this RouteGroupBuilder builder,
        string pattern) where TRequest : IHttpRequest
    {
        builder.MapDelete(pattern,
            async (IMediator mediator, [AsParameters] TRequest request) => await mediator.Send(request));
        return builder;
    }
}