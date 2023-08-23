using MediatR;
using OrderHandler.DomainCommons.Services.Interfaces;
using OrderHandler.Server.Endpoints.Requests;

namespace OrderHandler.Server.Endpoints.Handlers;

public class AddArticleHandler : IRequestHandler<AddArticleRequest, IResult>
{
    private readonly IUnitOfWork _unitOfWork;

    public AddArticleHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IResult> Handle(AddArticleRequest request, CancellationToken cancellationToken)
    {
        await _unitOfWork.ArticleRepository.AddAsync(request.Article);

        await _unitOfWork.SaveAsync();

        return Results.Ok(request.Article);
    }
}