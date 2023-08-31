using OrderHandler.DomainCommons.DataTransferObjects;

namespace OrderHandler.DomainCommons.Services.Interfaces;

public interface IOrderRowRepository : IRepository<OrderRowDto>
{
    Task<ServiceResponse<IReadOnlyCollection<OrderRowDto>>> GetManyByOrderRowNumber(int orderRowNumber);

    Task<ServiceResponse<IReadOnlyCollection<OrderRowDto>>> GetManyByArticleNumber(int articleNumber);

    Task<ServiceResponse<IReadOnlyCollection<OrderRowDto>>> GetManyByArticleName(string articleName);
}