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

        var

        await _ctx.OrderRows.AddAsync(ConvertToModel(dto));
        return new ServiceResponse<OrderRowDto>(true, "", dto);
    }

    public async Task<ServiceResponse<OrderRowDto>> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<ServiceResponse<IReadOnlyCollection<OrderRowDto>>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<ServiceResponse<OrderRowDto>> UpdateAsync(OrderRowDto dto)
    {
        throw new NotImplementedException();
    }

    public async Task<ServiceResponse<OrderRowDto>> RemoveAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<ServiceResponse<IReadOnlyCollection<OrderRowDto>>> GetManyByOrderRowNumber(int orderRowNumber)
    {
        throw new NotImplementedException();
    }


    private OrderRowDto ConvertToDto(OrderRowModel m)
    {
        return new OrderRowDto()
        {
            Id = m.Id,
            AmountOfArticles = m.AmountOfArticles,
            RowNumber = m.RowNumber,
            Article = new ArticleDto()
            {
                Id = m.Article.Id
            },
            Order = new OrderDto()
            {
                Id = m.Order.Id,
            }
        };
    }


    private OrderRowModel ConvertToModel(OrderRowDto d)
    {
        return new OrderRowModel()
        {
            Id = d.Id,
            AmountOfArticles = d.AmountOfArticles,
            RowNumber = d.RowNumber,
            Article = new ArticleModel()
            {
                Id = d.Article.Id
            },
            Order = new OrderModel()
            {
                Id = d.Order.Id,
            }
        };
    }
}