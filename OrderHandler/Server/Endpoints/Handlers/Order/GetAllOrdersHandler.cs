using MediatR;
using OrderHandler.DomainCommons.Services.Interfaces;
using OrderHandler.Server.Endpoints.Requests.Order;

namespace OrderHandler.Server.Endpoints.Handlers.Order;

public class GetAllOrdersHandler : IRequestHandler<GetAllOrdersRequest, IResult>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllOrdersHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }


    public async Task<IResult> Handle(GetAllOrdersRequest request, CancellationToken cancellationToken)
    {
        var response = await _unitOfWork.OrderRepository.GetAllAsync();

        if (!response.Success || response.Data is null)
            return Results.NotFound();

        return Results.Ok(response.Data);
    }
}