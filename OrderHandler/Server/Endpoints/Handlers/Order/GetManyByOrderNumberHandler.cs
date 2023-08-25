using MediatR;
using OrderHandler.DomainCommons.Services.Interfaces;
using OrderHandler.Server.Endpoints.Requests.Order;

namespace OrderHandler.Server.Endpoints.Handlers.Order;

public class GetManyByOrderNumberHandler : IRequestHandler<GetManyByOrderNumberRequest, IResult>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetManyByOrderNumberHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }


    public async Task<IResult> Handle(GetManyByOrderNumberRequest request, CancellationToken cancellationToken)
    {
        var response = await _unitOfWork.OrderRepository.GetManyByOrderNumber(request.OrderNumber);

        if (!response.Success || response.Data is null)
            return Results.NotFound();

        return Results.Ok(response.Data);
    }
}