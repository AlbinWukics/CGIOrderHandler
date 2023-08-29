using MediatR;
using OrderHandler.DomainCommons.Services.Interfaces;
using OrderHandler.Server.Endpoints.Requests.Order;

namespace OrderHandler.Server.Endpoints.Handlers.Order;

public class AddOrderHandler : IRequestHandler<AddOrderRequest, IResult>
{
    private readonly IUnitOfWork _unitOfWork;

    public AddOrderHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }


    public async Task<IResult> Handle(AddOrderRequest request, CancellationToken cancellationToken)
    {
        var response = await _unitOfWork.OrderRepository.AddAsync(request.Order);

        if (!response.Success || response.Data is null)
            return Results.NotFound();

        await _unitOfWork.SaveAsync();
        return Results.Ok(response);
    }
}