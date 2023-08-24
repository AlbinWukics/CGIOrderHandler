using MediatR;
using OrderHandler.DomainCommons.Services.Interfaces;
using OrderHandler.Server.Endpoints.Requests.Article;

namespace OrderHandler.Server.Endpoints.Handlers.Article;

public class GetManyByArticleNumberHandler : IRequestHandler<GetManyByArticleNumberRequest, IResult>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetManyByArticleNumberHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }


    public async Task<IResult> Handle(GetManyByArticleNumberRequest request, CancellationToken cancellationToken)
    {
        var response = await _unitOfWork.ArticleRepository.GetManyByArticleNumber(request.ArticleNumber);

        if (!response.Success)
            return Results.NotFound();

        return Results.Ok(response.Data);
    }
}