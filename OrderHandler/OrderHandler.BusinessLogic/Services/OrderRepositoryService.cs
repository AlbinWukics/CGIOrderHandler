using System.Collections.Immutable;
using Microsoft.EntityFrameworkCore;
using OrderHandler.DataAccess.Contexts;
using OrderHandler.DomainCommons.DataModels;
using OrderHandler.DomainCommons.DataTransferObjects;
using OrderHandler.DomainCommons.Services;
using OrderHandler.DomainCommons.Services.Interfaces;

namespace OrderHandler.BusinessLogic.Services;

public class OrderRepositoryService : IOrderRepository
{
    #region Fields & Ctor
    private readonly OrderHandlerContext _ctx;

    public OrderRepositoryService(OrderHandlerContext ctx)
    {
        _ctx = ctx;
    }
    #endregion


    public async Task<ServiceResponse<OrderDto>> AddAsync(OrderDto dto)
    {
        var highestOrderNumber = await _ctx.Orders.MaxAsync(a => (int?)a.OrderNumber) ?? 10000;
        dto.OrderNumber = highestOrderNumber + 1;

        await _ctx.Orders.AddAsync(ConvertToModel(dto));
        return new ServiceResponse<OrderDto>(true, "", dto);
    }


    public async Task<ServiceResponse<OrderDto>> GetByIdAsync(Guid id)
    {
        var order = await _ctx.Orders.FindAsync(id);

        return order
            is null ? new ServiceResponse<OrderDto>(false, "Not found.", null)
            : new ServiceResponse<OrderDto>(true, "", ConvertToDto(order));
    }


    public async Task<ServiceResponse<IReadOnlyCollection<OrderDto>>> GetAllAsync()
    {
        var orders = await _ctx.Orders.ToListAsync();

        if (!orders.Any())
            return new ServiceResponse<IReadOnlyCollection<OrderDto>>(false, "List is empty.", null);

        return new ServiceResponse<IReadOnlyCollection<OrderDto>>(true, "",
            orders.Select(ConvertToDto).ToImmutableList());
    }


    public async Task<ServiceResponse<OrderDto>> UpdateAsync(OrderDto dto)
    {
        var o = await _ctx.Orders.FindAsync(dto.Id);
        if (o is null)
            return new ServiceResponse<OrderDto>(false, "Order not found.", null);

        o.Status = dto.Status;
        o.LastUpdatedAt = DateTime.UtcNow;
        o.CustomerName = dto.CustomerName;

        _ctx.Orders.Update(o);
        return new ServiceResponse<OrderDto>(true, "", ConvertToDto(o));

    }


    public async Task<ServiceResponse<OrderDto>> RemoveAsync(Guid id)
    {
        var o = await _ctx.Orders.FindAsync(id);
        if (o is null)
            return new ServiceResponse<OrderDto>(false, "Not found.", null);

        _ctx.Orders.Remove(o);
        return new ServiceResponse<OrderDto>(true, "", ConvertToDto(o));
    }


    public async Task<ServiceResponse<IReadOnlyCollection<OrderDto>>> GetManyByOrderNumber(int orderNumber)
    {
        var orders = await _ctx.Orders
            .Where(x => x.OrderNumber.ToString()
            .Contains(orderNumber.ToString()))
            .ToListAsync();

        if (!orders.Any())
            return new ServiceResponse<IReadOnlyCollection<OrderDto>>(false, "Not found.", null);

        return new ServiceResponse<IReadOnlyCollection<OrderDto>>
            (true, "", orders.Select(ConvertToDto).ToImmutableList());
    }


    private OrderDto ConvertToDto(OrderModel m)
    {
        var o = new OrderDto()
        {
            Id = m.Id,
            Status = m.Status,
            OrderNumber = m.OrderNumber,
            CustomerName = m.CustomerName,
            LastUpdatedAt = m.LastUpdatedAt,
            CreatedAt = m.CreatedAt
        };

        if (m.OrderRows is null || !m.OrderRows.Any())
            return o;

        o.OrderRows = m.OrderRows.Select(x => new OrderRowDto()
        {
            Id = x.Id,
            AmountOfArticles = x.AmountOfArticles,
            RowNumber = x.RowNumber,
            ArticleId = new ArticleDto()
            {
                Id = x.ArticleId.Id
            },
            OrderId = new OrderDto()
            {
                Id = x.OrderId.Id
            }
        }).ToList();

        return o;
    }


    private OrderModel ConvertToModel(OrderDto o)
    {
        var m = new OrderModel()
        {
            Id = o.Id,
            Status = o.Status,
            OrderNumber = o.OrderNumber,
            CustomerName = o.CustomerName,
            LastUpdatedAt = o.LastUpdatedAt,
            CreatedAt = o.CreatedAt
        };
        if (o.OrderRows is null || !o.OrderRows.Any())
            return m;

        m.OrderRows = o.OrderRows.Select(x => new OrderRowModel()
        {
            Id = x.Id,
            AmountOfArticles = x.AmountOfArticles,
            RowNumber = x.RowNumber,
            ArticleId = new ArticleModel()
            {
                Id = x.ArticleId.Id
            },
            OrderId = new OrderModel()
            {
                Id = x.OrderId.Id
            }
        }).ToList();

        return m;
    }
}