using MediatR;
using OrderHandler.DomainCommons.Services.Interfaces;
using OrderHandler.Server.Endpoints.Requests.Article;

namespace OrderHandler.Server.Endpoints.Handlers.Article;

public class AddArticleHandler : IRequestHandler<AddArticleRequest, IResult>
{
    private readonly IUnitOfWork _unitOfWork;

    public AddArticleHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IResult> Handle(AddArticleRequest request, CancellationToken cancellationToken)
    {
        var response = await _unitOfWork.ArticleRepository.AddAsync(request.Article);

        if (!response.Success || response.Data is null)
            return Results.BadRequest();

        await _unitOfWork.SaveAsync();
        return Results.Ok(response.Data);
    }
}