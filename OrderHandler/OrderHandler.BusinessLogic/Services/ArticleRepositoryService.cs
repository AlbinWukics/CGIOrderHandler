using System.Collections.Immutable;
using Microsoft.EntityFrameworkCore;
using OrderHandler.DataAccess.Contexts;
using OrderHandler.DomainCommons.DataModels;
using OrderHandler.DomainCommons.DataTransferObjects;
using OrderHandler.DomainCommons.Services;
using OrderHandler.DomainCommons.Services.Interfaces;

namespace OrderHandler.BusinessLogic.Services;

public class ArticleRepositoryService : IArticleRepository
{
    #region Fields & Ctor
    private readonly OrderHandlerContext _ctx;

    public ArticleRepositoryService(OrderHandlerContext ctx)
    {
        _ctx = ctx;
    }
    #endregion


    public async Task<ServiceResponse<ArticleDto>> AddAsync(ArticleDto dto)
    {
        var highestArticleNumber = await _ctx.Articles.MaxAsync(a => (int?)a.ArticleNumber) ?? 10000;
        dto.ArticleNumber = highestArticleNumber + 1;

        if (dto.Color is null || string.IsNullOrWhiteSpace(dto.Color.Color))
        {
            dto.Color = null;
            await _ctx.Articles.AddAsync(ConvertToModel(dto));
            return new ServiceResponse<ArticleDto>(true, "", dto);
        }

        var colorEntity = await _ctx.Colors.FindAsync(dto.Color.Id);
        if (colorEntity is null)
        {

            colorEntity = await _ctx.Colors
                    .FirstOrDefaultAsync(x => x.Color.ToLower()
                    .Equals(dto.Color.Color.ToLower()));

            if (colorEntity is null)
            {
                dto.Color.Color = dto.Color.Color.ToLower();
                await _ctx.Articles.AddAsync(ConvertToModel(dto));
                return new ServiceResponse<ArticleDto>(true, "", dto);
            }
        }

        var model = ConvertToModel(dto);
        model.Color = colorEntity;
        model.Color.Color = model.Color.Color.ToLower();

        var entity = await _ctx.Articles.AddAsync(model);
        return new ServiceResponse<ArticleDto>(true, "", ConvertToDto(entity.Entity));
    }


    public async Task<ServiceResponse<ArticleDto>> GetByIdAsync(Guid id)
    {
        var article = await _ctx
            .Articles.Include(x => x.Color)
            .FirstOrDefaultAsync(x => x.Id.Equals(id));

        return article is null
            ? new ServiceResponse<ArticleDto>(false, "Not found.", null)
            : new ServiceResponse<ArticleDto>(true, "", ConvertToDto(article));
    }


    public async Task<ServiceResponse<IReadOnlyCollection<ArticleDto>>> GetAllAsync()
    {
        var articles = await _ctx
            .Articles.Include(x => x.Color).ToListAsync();

        if (!articles.Any())
            return new ServiceResponse<IReadOnlyCollection<ArticleDto>>(false, "List is empty.", null);

        return new ServiceResponse<IReadOnlyCollection<ArticleDto>>(true, "",
            articles.Select(ConvertToDto).ToImmutableList());
    }


    public async Task<ServiceResponse<ArticleDto>> UpdateAsync(ArticleDto dto)
    {
        //Om man ändar färg skall den skapa en ny färg, inte ändra nuvarande då den kan vara kopplad till flera som skall behålla sin nuvarande färg.
        var a = await _ctx.Articles.FindAsync(dto.Id);
        if (a is null)
            return new ServiceResponse<ArticleDto>(false, "Article not found.", null);

        a.ArticleName = dto.ArticleName;
        a.UnitPrice = dto.UnitPrice;
        a.LastUpdatedAt = DateTime.UtcNow;

        if (dto.Color is null || string.IsNullOrWhiteSpace(dto.Color.Color))
        {
            a.Color = null;
            _ctx.Articles.Update(a);
            return new ServiceResponse<ArticleDto>(true, "", ConvertToDto(a));
        }

        var color = await _ctx.Colors
            .FirstOrDefaultAsync(x => x.Color.ToLower()
            .Equals(dto.Color.Color.ToLower()));

        if (color is null)
        {
            a.Color = new ColorModel()
            {
                Color = dto.Color.Color.ToLower()
            };
            _ctx.Articles.Update(a);
            return new ServiceResponse<ArticleDto>(true, "", ConvertToDto(a));
        }

        a.Color = color;
        a.Color.Color = a.Color.Color.ToLower();
        _ctx.Articles.Update(a);
        return new ServiceResponse<ArticleDto>(true, "", ConvertToDto(a));
    }


    public async Task<ServiceResponse<ArticleDto>> RemoveAsync(Guid id)
    {
        //Does not include nor delete paired color.

        var a = await _ctx.Articles.FindAsync(id);
        if (a is null)
            return new ServiceResponse<ArticleDto>(false, "Not found.", null);

        _ctx.Articles.Remove(a);
        return new ServiceResponse<ArticleDto>(true, "", ConvertToDto(a));
    }


    public async Task<ServiceResponse<IReadOnlyCollection<ArticleDto>>> GetManyByArticleNumber(int articleNumber)
    {
        var articles = await _ctx.Articles
            .Include(x => x.Color)
            .Where(a => a.ArticleNumber.ToString()
            .Contains(articleNumber.ToString()))
            .ToListAsync();

        if (!articles.Any())
            return new ServiceResponse<IReadOnlyCollection<ArticleDto>>(false, "Not found.", null);

        return new ServiceResponse<IReadOnlyCollection<ArticleDto>>
        (true, "", articles.Select(ConvertToDto).ToImmutableList());
    }


    private ArticleDto ConvertToDto(ArticleModel m)
    {
        var article = new ArticleDto()
        {
            ArticleName = m.ArticleName,
            ArticleNumber = m.ArticleNumber,
            Id = m.Id,
            UnitPrice = m.UnitPrice,
            CreatedAt = m.CreatedAt,
            LastUpdatedAt = m.LastUpdatedAt
        };

        if (m.Color is not null)
            article.Color = new ColorDto() { Color = m.Color.Color, Id = m.Color.Id };

        return article;
    }


    private ArticleModel ConvertToModel(ArticleDto d)
    {
        var article = new ArticleModel()
        {
            ArticleName = d.ArticleName,
            ArticleNumber = d.ArticleNumber,
            Id = d.Id,
            UnitPrice = d.UnitPrice,
            CreatedAt = d.CreatedAt,
            LastUpdatedAt = d.LastUpdatedAt
        };

        if (d.Color is not null)
            article.Color = new ColorModel() { Color = d.Color.Color, Id = d.Color.Id };

        return article;
    }
}