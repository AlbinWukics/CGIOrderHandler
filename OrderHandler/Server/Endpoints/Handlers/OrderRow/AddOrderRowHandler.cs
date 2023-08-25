using MediatR;
using OrderHandler.DomainCommons.Services.Interfaces;
using OrderHandler.Server.Endpoints.Requests.OrderRow;

namespace OrderHandler.Server.Endpoints.Handlers.OrderRow;

public class AddOrderRowHandler : IRequestHandler<AddOrderRowRequest, IResult>
{
    private readonly IUnitOfWork _unitOfWork;

    public AddOrderRowHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }


    public async Task<IResult> Handle(AddOrderRowRequest request, CancellationToken cancellationToken)
    {
        
    }
}