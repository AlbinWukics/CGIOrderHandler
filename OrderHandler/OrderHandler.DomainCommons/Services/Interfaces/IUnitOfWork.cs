namespace OrderHandler.DomainCommons.Services.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IArticleRepository ArticleRepository { get; }
    IOrderRepository OrderRepository { get; }
    IOrderRowRepository OrderRowRepository { get; }

    Task SaveAsync();
}