using MediatR;
using OrderHandler.DomainCommons.Services.Interfaces;
using OrderHandler.Server.Endpoints.Requests.Order;

namespace OrderHandler.Server.Endpoints.Handlers.Order;

public class RemoveOrderHandler : IRequestHandler<RemoveOrderRequest, IResult>
{
    private readonly IUnitOfWork _unitOfWork;

    public RemoveOrderHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }


    public async Task<IResult> Handle(RemoveOrderRequest request, CancellationToken cancellationToken)
    {
        var response = await _unitOfWork.OrderRepository.RemoveAsync(request.Id);


        if (!response.Success || response.Data is null)
            return Results.NotFound();

        await _unitOfWork.SaveAsync();
        return Results.Ok(response);
    }
}