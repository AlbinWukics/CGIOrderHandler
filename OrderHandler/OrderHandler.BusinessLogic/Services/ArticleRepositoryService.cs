using Microsoft.EntityFrameworkCore;
using OrderHandler.DataAccess.Contexts;
using OrderHandler.DomainCommons.DataModels;
using OrderHandler.DomainCommons.DataTransferObjects;
using OrderHandler.DomainCommons.Services;
using OrderHandler.DomainCommons.Services.Interfaces;

namespace OrderHandler.BusinessLogic.Services;

public class ArticleRepositoryService : IArticleRepository
{
    private readonly OrderHandlerContext _context;

    public ArticleRepositoryService(OrderHandlerContext context)
    {
        _context = context;
    }

    public async Task<ServiceResponse<ArticleDto>> AddAsync(ArticleDto dto)
    {
        var highestArticleNumber = await _context.Articles.MaxAsync(a => (int?)a.ArticleNumber) ?? 0;
        dto.ArticleNumber = highestArticleNumber + 1;



        await _context.Articles.AddAsync(ConvertToModel(dto));
        return new ServiceResponse<ArticleDto>(true, "Success.", dto);
    }

    public async Task<ServiceResponse<ArticleDto>> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<ServiceResponse<IReadOnlyCollection<ArticleDto>>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<ServiceResponse<ArticleDto>> UpdateAsync(ArticleDto dto)
    {
        throw new NotImplementedException();
    }

    public async Task<ServiceResponse<ArticleDto>> RemoveAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    private ArticleDto ConvertToDto(ArticleModel m)
    {
        var article = new ArticleDto()
        {
            ArticleName = m.ArticleName,
            ArticleNumber = m.ArticleNumber,
            Id = m.Id,
            UnitPrice = m.UnitPrice,
        };

        if (m.Color is null) return article;

        article.Color = m.Color;
        return article;
    }

    private ArticleModel ConvertToModel(ArticleDto d)
    {
        var article = new ArticleModel()
        {
            ArticleName = d.ArticleName,
            ArticleNumber = d.ArticleNumber,
            Id = d.Id,
            UnitPrice = d.UnitPrice
        };

        if (d.Color is null) return article;

        article.Color = d.Color;
        return article;
    }
}