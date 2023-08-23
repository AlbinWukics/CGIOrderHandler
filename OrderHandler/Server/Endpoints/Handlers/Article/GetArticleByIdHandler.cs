using MediatR;
using OrderHandler.DomainCommons.Services.Interfaces;
using OrderHandler.Server.Endpoints.Requests.Article;

namespace OrderHandler.Server.Endpoints.Handlers.Article;

public class GetArticleByIdHandler : IRequestHandler<GetArticleByIdRequest, IResult>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetArticleByIdHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }


    public async Task<IResult> Handle(GetArticleByIdRequest request, CancellationToken cancellationToken)
    {
        var response = await _unitOfWork.ArticleRepository.GetByIdAsync(request.Id);

        if (!response.Success || response.Data is null)
            return Results.BadRequest();

        return Results.Ok(response.Data);
    }
}