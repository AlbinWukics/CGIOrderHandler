using OrderHandler.DomainCommons.DataTransferObjects;

namespace OrderHandler.DomainCommons.Services.Interfaces;

public interface IArticleRepository : IRepository<ArticleDto>
{
    Task<ServiceResponse<IReadOnlyCollection<ArticleDto>>> GetManyByArticleNumber(int articleNumber);
}