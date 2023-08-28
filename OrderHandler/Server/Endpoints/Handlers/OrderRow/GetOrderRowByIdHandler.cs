using MediatR;
using OrderHandler.DomainCommons.Services.Interfaces;
using OrderHandler.Server.Endpoints.Requests.OrderRow;

namespace OrderHandler.Server.Endpoints.Handlers.OrderRow;

public class GetOrderRowByIdHandler : IRequestHandler<GetOrderRowByIdRequest, IResult>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetOrderRowByIdHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IResult> Handle(GetOrderRowByIdRequest request, CancellationToken cancellationToken)
    {
        var response = await _unitOfWork.OrderRowRepository.GetByIdAsync(request.Id);

        if (!response.Success || response.Data is null)
            return Results.NotFound();

        return Results.Ok(response.Data);

    }
}