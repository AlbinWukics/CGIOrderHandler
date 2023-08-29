using MediatR;
using OrderHandler.DomainCommons.Services.Interfaces;
using OrderHandler.Server.Endpoints.Requests.OrderRow;

namespace OrderHandler.Server.Endpoints.Handlers.OrderRow;

public class GetAllOrderRowsHandler : IRequestHandler<GetAllOrderRowsRequest, IResult>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllOrderRowsHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IResult> Handle(GetAllOrderRowsRequest request, CancellationToken cancellationToken)
    {
        var response = await _unitOfWork.OrderRowRepository.GetAllAsync();

        if (!response.Success || response.Data is null)
            return Results.NotFound();

        return Results.Ok(response);
    }
}