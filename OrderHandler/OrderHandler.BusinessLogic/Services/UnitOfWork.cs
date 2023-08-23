using OrderHandler.DataAccess.Contexts;
using OrderHandler.DomainCommons.Services.Interfaces;

namespace OrderHandler.BusinessLogic.Services;

public class UnitOfWork : IUnitOfWork
{
    private readonly OrderHandlerContext _ctx;

    public IArticleRepository ArticleRepository { get; }
    public IColorRepository ColorRepository { get; }
    public IOrderRepository OrderRepository { get; }
    public IOrderRowRepository OrderRowRepository { get; }


    public UnitOfWork(OrderHandlerContext ctx)
    {
        _ctx = ctx;
        ArticleRepository = new ArticleRepositoryService(_ctx);
    }


    public async Task SaveAsync()
    {
        await _ctx.SaveChangesAsync();
    }

    public void Dispose()
    {
        _ctx.Dispose();
    }
}