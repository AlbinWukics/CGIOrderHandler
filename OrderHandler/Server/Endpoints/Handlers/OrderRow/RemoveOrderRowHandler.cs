using MediatR;
using OrderHandler.DomainCommons.Services.Interfaces;
using OrderHandler.Server.Endpoints.Requests.OrderRow;

namespace OrderHandler.Server.Endpoints.Handlers.OrderRow;

public class RemoveOrderRowHandler : IRequestHandler<RemoveOrderRowRequest, IResult>
{
    private readonly IUnitOfWork _unitOfWork;

    public RemoveOrderRowHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IResult> Handle(RemoveOrderRowRequest request, CancellationToken cancellationToken)
    {

    }
}