using MediatR;
using OrderHandler.DomainCommons.Services.Interfaces;
using OrderHandler.Server.Endpoints.Requests.Order;

namespace OrderHandler.Server.Endpoints.Handlers.Order;

public class UpdateOrderHandler : IRequestHandler<UpdateOrderRequest, IResult>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateOrderHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }


    public async Task<IResult> Handle(UpdateOrderRequest request, CancellationToken cancellationToken)
    {
        var response = await _unitOfWork.OrderRepository.UpdateAsync(request.Order);

        if (!response.Success || response.Data is null)
            return Results.NotFound();

        await _unitOfWork.SaveAsync();
        return Results.Ok(response);
    }
}