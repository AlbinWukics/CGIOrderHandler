using MediatR;
using OrderHandler.DomainCommons.Services.Interfaces;
using OrderHandler.Server.Endpoints.Requests.OrderRow;

namespace OrderHandler.Server.Endpoints.Handlers.OrderRow;

public class GetManyOrderRowsByArticleNumberHandler : IRequestHandler<GetManyOrderRowsByArticleNumberRequest, IResult>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetManyOrderRowsByArticleNumberHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }


    public async Task<IResult> Handle(GetManyOrderRowsByArticleNumberRequest request, CancellationToken cancellationToken)
    {
        var response = await _unitOfWork.OrderRowRepository.GetManyByArticleNumber(request.ArticleNumber);

        if (!response.Success)
            return Results.NotFound();

        return Results.Ok(response);
    }
}