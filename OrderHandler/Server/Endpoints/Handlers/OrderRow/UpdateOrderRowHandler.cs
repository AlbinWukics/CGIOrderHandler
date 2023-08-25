using MediatR;
using OrderHandler.DomainCommons.Services.Interfaces;
using OrderHandler.Server.Endpoints.Requests.OrderRow;

namespace OrderHandler.Server.Endpoints.Handlers.OrderRow;

public class UpdateOrderRowHandler : IRequestHandler<UpdateOrderRowRequest, IResult>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateOrderRowHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IResult> Handle(UpdateOrderRowRequest request, CancellationToken cancellationToken)
    {

    }
}