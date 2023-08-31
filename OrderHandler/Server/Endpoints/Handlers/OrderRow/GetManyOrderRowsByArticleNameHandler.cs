using MediatR;
using OrderHandler.DomainCommons.Services.Interfaces;
using OrderHandler.Server.Endpoints.Requests.OrderRow;

namespace OrderHandler.Server.Endpoints.Handlers.OrderRow;

public class GetManyOrderRowsByArticleNameHandler : IRequestHandler<GetManyOrderRowsByArticleNameRequest, IResult>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetManyOrderRowsByArticleNameHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }


    public async Task<IResult> Handle(GetManyOrderRowsByArticleNameRequest request, CancellationToken cancellationToken)
    {
        var response = await _unitOfWork.OrderRowRepository.GetManyByArticleName(request.ArticleName);

        if (!response.Success)
            return Results.NotFound();

        return Results.Ok(response);
    }
}