using MediatR;
using OrderHandler.DomainCommons.Services.Interfaces;
using OrderHandler.Server.Endpoints.Requests.Order;

namespace OrderHandler.Server.Endpoints.Handlers.Order;

public class GetOrderByIdHandler : IRequestHandler<GetOrderByIdRequest, IResult>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetOrderByIdHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }


    public async Task<IResult> Handle(GetOrderByIdRequest request, CancellationToken cancellationToken)
    {
        var response = await _unitOfWork.OrderRepository.GetByIdAsync(request.Id);

        if (!response.Success || response.Data is null)
            return Results.NotFound();

        return Results.Ok(response.Data);
    }
}