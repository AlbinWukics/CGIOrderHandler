using Microsoft.EntityFrameworkCore;
using OrderHandler.DomainCommons.DataModels;

namespace OrderHandler.DataAccess.Contexts;

public class OrderHandlerContext : DbContext
{
    public OrderHandlerContext(DbContextOptions<OrderHandlerContext> options) : base(options)
    {
    }

    public DbSet<ArticleModel> Articles { get; set; }

    public DbSet<ColorModel> Colors { get; set; }

    public DbSet<OrderModel> Orders { get; set; }

    public DbSet<OrderRowModel> OrderRows { get; set; }
}