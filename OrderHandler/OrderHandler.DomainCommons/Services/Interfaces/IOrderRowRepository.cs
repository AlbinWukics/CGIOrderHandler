using OrderHandler.DomainCommons.DataTransferObjects;

namespace OrderHandler.DomainCommons.Services.Interfaces;

public interface IOrderRowRepository : IRepository<OrderRowDto>
{
    Task<ServiceResponse<IReadOnlyCollection<OrderRowDto>>> GetManyByOrderRowNumber(int orderRowNumber);
}