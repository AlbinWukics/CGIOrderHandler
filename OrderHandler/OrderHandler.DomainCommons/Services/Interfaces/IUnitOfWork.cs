namespace OrderHandler.DomainCommons.Services.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IArticleRepository ArticleRepository { get; }
    IColorRepository ColorRepository { get; }
    IOrderRepository OrderRepository { get; }
    IOrderRowRepository OrderRowRepository { get; }

    Task SaveAsync();
}