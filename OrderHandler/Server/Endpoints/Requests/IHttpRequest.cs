using MediatR;

namespace OrderHandler.Server.Endpoints.Requests;

public interface IHttpRequest : IRequest<IResult>
{
}