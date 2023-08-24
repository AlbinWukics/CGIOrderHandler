using MediatR;
using OrderHandler.DomainCommons.Services.Interfaces;
using OrderHandler.Server.Endpoints.Requests.Article;

namespace OrderHandler.Server.Endpoints.Handlers.Article;

public class RemoveArticleHandler : IRequestHandler<RemoveArticleRequest, IResult>
{
    private readonly IUnitOfWork _unitOfWork;

    public RemoveArticleHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }


    public async Task<IResult> Handle(RemoveArticleRequest request, CancellationToken cancellationToken)
    {
        var response = await _unitOfWork.ArticleRepository.RemoveAsync(request.Id);

        if (!response.Success)
            return Results.NotFound();

        await _unitOfWork.SaveAsync();
        return Results.Ok(response.Data);
    }
}