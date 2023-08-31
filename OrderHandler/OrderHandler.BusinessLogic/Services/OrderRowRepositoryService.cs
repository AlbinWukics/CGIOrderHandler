using System.Collections.Immutable;
using Microsoft.EntityFrameworkCore;
using OrderHandler.DataAccess.Contexts;
using OrderHandler.DomainCommons.DataModels;
using OrderHandler.DomainCommons.DataTransferObjects;
using OrderHandler.DomainCommons.Services;
using OrderHandler.DomainCommons.Services.Interfaces;

namespace OrderHandler.BusinessLogic.Services;

public class OrderRowRepositoryService : IOrderRowRepository
{
    private readonly OrderHandlerContext _ctx;

    public OrderRowRepositoryService(OrderHandlerContext ctx)
    {
        _ctx = ctx;
    }


    public async Task<ServiceResponse<OrderRowDto>> AddAsync(OrderRowDto dto)
    {
        var highestRowNumber = await _ctx.OrderRows.MaxAsync(a => (int?)a.RowNumber) ?? 10000;
        dto.RowNumber = highestRowNumber + 1;

        var order = await _ctx.Orders.FindAsync(dto.Order.Id);
        if (order is null)
            return new ServiceResponse<OrderRowDto>(false, "Fk order not found", null);

        var article = await _ctx.Articles.FindAsync(dto.Article.Id);
        if (article is null)
            return new ServiceResponse<OrderRowDto>(false, "Fk article not found", null);

        var model = ConvertToModel(dto);
        model.Order = order;
        model.Article = article;

        await _ctx.OrderRows.AddAsync(model);
        return new ServiceResponse<OrderRowDto>(true, "", ConvertToDto(model));
    }

    public async Task<ServiceResponse<OrderRowDto>> GetByIdAsync(Guid id)
    {
        var orderRow = await _ctx.OrderRows
            .Include(x => x.Article)
            .Include(x => x.Order)
            .FirstOrDefaultAsync(x => x.Id.Equals(id));

        if (orderRow is null)
            return new ServiceResponse<OrderRowDto>(false, "OrderRow not found", null);

        return new ServiceResponse<OrderRowDto>(true, "", ConvertToDto(orderRow));
    }

    public async Task<ServiceResponse<IReadOnlyCollection<OrderRowDto>>> GetAllAsync()
    {
        var orderRows = await _ctx.OrderRows
            .Include(x => x.Article)
            .Include(x => x.Order)
            .ToListAsync();

        if (!orderRows.Any())
            return new ServiceResponse<IReadOnlyCollection<OrderRowDto>>(false, "None found.", null);

        return new ServiceResponse<IReadOnlyCollection<OrderRowDto>>(true, "",
            orderRows.Select(ConvertToDto).ToImmutableList());
    }

    public async Task<ServiceResponse<OrderRowDto>> UpdateAsync(OrderRowDto dto)
    {
        var orderRow = await _ctx.OrderRows
            .Include(x => x.Article)
            .Include(x => x.Order)
            .FirstOrDefaultAsync(x => x.Id.Equals(dto.Id));

        if (orderRow is null)
            return new ServiceResponse<OrderRowDto>(false, "Not found", null);

        orderRow.AmountOfArticles = dto.AmountOfArticles;
        orderRow.Article.Id = dto.Article.Id;
        orderRow.Order.Id = dto.Order.Id;

        _ctx.Update(orderRow);
        return new ServiceResponse<OrderRowDto>(true, "", ConvertToDto(orderRow));
    }

    public async Task<ServiceResponse<OrderRowDto>> RemoveAsync(Guid id)
    {
        var orderRow = await _ctx.OrderRows
            .Include(x => x.Article)
            .Include(x => x.Order)
            .FirstOrDefaultAsync(x => x.Id.Equals(id));
        if (orderRow is null)
            return new ServiceResponse<OrderRowDto>(false, "Not found", null);

        _ctx.OrderRows.Remove(orderRow);
        return new ServiceResponse<OrderRowDto>(true, "", ConvertToDto(orderRow));
    }

    public async Task<ServiceResponse<IReadOnlyCollection<OrderRowDto>>> GetManyByOrderRowNumber(int orderRowNumber)
    {
        var orderRows = await _ctx.OrderRows
            .Include(x => x.Article)
            .Include(x => x.Order)
            .Where(x => x.Id.ToString()
            .Contains(orderRowNumber.ToString()))
            .ToListAsync();

        if (!orderRows.Any())
            return new ServiceResponse<IReadOnlyCollection<OrderRowDto>>(true, "None found.", null);

        return new ServiceResponse<IReadOnlyCollection<OrderRowDto>>(true, "",
            orderRows.Select(ConvertToDto).ToImmutableList());
    }


    public async Task<ServiceResponse<IReadOnlyCollection<OrderRowDto>>> GetManyByArticleNumber(int articleNumber)
    {
        var orderRows = await _ctx.OrderRows
            .Include(x => x.Article)
            .Include(x => x.Order)
            .Where(x => x.Article.ArticleNumber.ToString()
            .Contains(articleNumber.ToString()))
            .ToListAsync();

        if (!orderRows.Any())
            return new ServiceResponse<IReadOnlyCollection<OrderRowDto>>(true, "None found.", null);

        return new ServiceResponse<IReadOnlyCollection<OrderRowDto>>(true, "",
            orderRows.Select(ConvertToDto).ToImmutableList());
    }


    public async Task<ServiceResponse<IReadOnlyCollection<OrderRowDto>>> GetManyByArticleName(string articleName)
    {
        var orderRows = await _ctx.OrderRows
            .Include(x => x.Article)
            .Include(x => x.Order)
            .Where(x => x.Article.ArticleName.ToLower()
            .Contains(articleName.ToLower()))
            .ToListAsync();

        if (!orderRows.Any())
            return new ServiceResponse<IReadOnlyCollection<OrderRowDto>>(true, "None found.", null);

        return new ServiceResponse<IReadOnlyCollection<OrderRowDto>>(true, "",
            orderRows.Select(ConvertToDto).ToImmutableList());
    }


    private OrderRowDto ConvertToDto(OrderRowModel m)
    {
        var dto = new OrderRowDto()
        {
            Id = m.Id,
            AmountOfArticles = m.AmountOfArticles,
            RowNumber = m.RowNumber,
            Article = new ArticleDto()
            {
                Id = m.Article.Id,
                ArticleName = m.Article.ArticleName,
                ArticleNumber = m.Article.ArticleNumber,
                UnitPrice = m.Article.UnitPrice,
                CreatedAt = m.Article.CreatedAt,
                LastUpdatedAt = m.Article.LastUpdatedAt
            },
            Order = new OrderDto()
            {
                Id = m.Order.Id,
                CustomerName = m.Order.CustomerName,
                OrderNumber = m.Order.OrderNumber,
                CreatedAt = m.Order.CreatedAt,
                LastUpdatedAt = m.Order.LastUpdatedAt,
                Status = m.Order.Status
            }
        };

        if (m.Article.Color is not null)
        {
            dto.Article.Color = new ColorDto()
            {
                Id = m.Article.Color.Id,
                Color = m.Article.Color.Color
            };
        }

        return dto;
    }


    private OrderRowModel ConvertToModel(OrderRowDto d)
    {
        var model = new OrderRowModel()
        {
            Id = d.Id,
            AmountOfArticles = d.AmountOfArticles,
            RowNumber = d.RowNumber,
            Article = new ArticleModel()
            {
                Id = d.Article.Id,
                ArticleName = d.Article.ArticleName,
                ArticleNumber = d.Article.ArticleNumber,
                UnitPrice = d.Article.UnitPrice,
                CreatedAt = d.Article.CreatedAt,
                LastUpdatedAt = d.Article.LastUpdatedAt
            },
            Order = new OrderModel()
            {
                Id = d.Order.Id,
                CustomerName = d.Order.CustomerName,
                OrderNumber = d.Order.OrderNumber,
                CreatedAt = d.Order.CreatedAt,
                LastUpdatedAt = d.Order.LastUpdatedAt,
                Status = d.Order.Status
            }
        };

        if (d.Article.Color is not null)
        {
            model.Article.Color = new ColorModel()
            {
                Id = d.Article.Color.Id,
                Color = d.Article.Color.Color
            };
        }

        return model;
    }
}