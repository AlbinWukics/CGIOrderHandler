using MediatR;
using Microsoft.AspNetCore.Connections.Features;
using OrderHandler.DomainCommons.Services.Interfaces;
using OrderHandler.Server.Endpoints.Requests.Article;

namespace OrderHandler.Server.Endpoints.Handlers.Article;

public class GetAllArticlesHandler : IRequestHandler<GetAllArticlesRequest, IResult>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllArticlesHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }


    public async Task<IResult> Handle(GetAllArticlesRequest request, CancellationToken cancellationToken)
    {
        var response = await _unitOfWork.ArticleRepository.GetAllAsync();

        if (!response.Success)
            return Results.BadRequest();

        return Results.Ok(response.Data);
    }
}