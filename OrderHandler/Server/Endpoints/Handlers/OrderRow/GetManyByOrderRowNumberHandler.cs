using MediatR;
using OrderHandler.DomainCommons.Services.Interfaces;
using OrderHandler.Server.Endpoints.Requests.OrderRow;

namespace OrderHandler.Server.Endpoints.Handlers.OrderRow;

public class GetManyByOrderRowNumberHandler : IRequestHandler<GetManyByOrderRowNumberRequest, IResult>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetManyByOrderRowNumberHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IResult> Handle(GetManyByOrderRowNumberRequest request, CancellationToken cancellationToken)
    {
        
    }
}