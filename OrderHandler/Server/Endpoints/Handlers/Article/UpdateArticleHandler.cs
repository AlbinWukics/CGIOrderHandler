using MediatR;
using OrderHandler.DomainCommons.Services.Interfaces;
using OrderHandler.Server.Endpoints.Requests.Article;

namespace OrderHandler.Server.Endpoints.Handlers.Article;

public class UpdateArticleHandler : IRequestHandler<UpdateArticleRequest, IResult>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateArticleHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }


    public async Task<IResult> Handle(UpdateArticleRequest request, CancellationToken cancellationToken)
    {
        var response = await _unitOfWork.ArticleRepository.UpdateAsync(request.Article);

        if (!response.Success || response.Data is null)
            return Results.NotFound();

        await _unitOfWork.SaveAsync();
        return Results.Ok(response.Data);
    }
}