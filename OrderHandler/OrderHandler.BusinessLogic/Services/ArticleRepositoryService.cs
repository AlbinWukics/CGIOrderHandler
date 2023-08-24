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
    private readonly OrderHandlerContext _context;

    public ArticleRepositoryService(OrderHandlerContext context)
    {
        _context = context;
    }


    public async Task<ServiceResponse<ArticleDto>> AddAsync(ArticleDto dto)
    {
        var highestArticleNumber = await _context.Articles.MaxAsync(a => (int?)a.ArticleNumber) ?? 10000;
        dto.ArticleNumber = highestArticleNumber + 1;

        if (dto.Color is not null)
        {
            var colorEntity = await _context.Colors.FindAsync(dto.Color.Id);

            if (colorEntity is not null)
            {
                var model = ConvertToModel(dto);
                model.Color = colorEntity;
                var entity = await _context.Articles.AddAsync(model);
                return new ServiceResponse<ArticleDto>(true, "", ConvertToDto(entity.Entity));
            }
        }

        await _context.Articles.AddAsync(ConvertToModel(dto));
        return new ServiceResponse<ArticleDto>(true, "", dto);
    }


    public async Task<ServiceResponse<ArticleDto>> GetByIdAsync(Guid id)
    {
        var article = await _context
            .Articles.Include(x => x.Color)
            .FirstOrDefaultAsync(x => x.Id.Equals(id));

        return article is null
            ? new ServiceResponse<ArticleDto>(false, "Not found.", null)
            : new ServiceResponse<ArticleDto>(true, "", ConvertToDto(article));
    }


    public async Task<ServiceResponse<IReadOnlyCollection<ArticleDto>>> GetAllAsync()
    {
        var articles = await _context
            .Articles.Include(x => x.Color).ToListAsync();

        if (!articles.Any())
            return new ServiceResponse<IReadOnlyCollection<ArticleDto>>(false, "List is empty.", null);

        return new ServiceResponse<IReadOnlyCollection<ArticleDto>>(true, "",
            articles.Select(ConvertToDto).ToImmutableList());
    }


    public async Task<ServiceResponse<ArticleDto>> UpdateAsync(ArticleDto dto)
    {
        var a = await _context.Articles.FindAsync(dto.Id);
        if (a is null)
            return new ServiceResponse<ArticleDto>(false, "Article id not found.", null);

        a.ArticleName = dto.ArticleName;
        a.UnitPrice = dto.UnitPrice;
        a.LastUpdatedAt = DateTime.UtcNow;

        if (dto.Color is null)
        {
            _context.Articles.Update(a);
            return new ServiceResponse<ArticleDto>(true, "", ConvertToDto(a));
        }


        var color = await _context.Colors.FindAsync(dto.Color.Id);
        if (color is not null)
        {
            a.Color = color;
            _context.Articles.Update(a);
            return new ServiceResponse<ArticleDto>(true, "", ConvertToDto(a));
        }

        // Does not return the right Id for the created color. Not important to fix.
        a.Color = new ColorModel() { Color = dto.Color.Color };
        _context.Articles.Update(a);
        return new ServiceResponse<ArticleDto>(true, "", ConvertToDto(a));
    }


    public async Task<ServiceResponse<ArticleDto>> RemoveAsync(Guid id)
    {
        //Does not include nor delete paired color.

        var a = await _context.Articles.FindAsync(id);
        if (a is null)
            return new ServiceResponse<ArticleDto>(false, "Not found.", null);

        _context.Articles.Remove(a);
        return new ServiceResponse<ArticleDto>(true, "", ConvertToDto(a));
    }


    public async Task<ServiceResponse<IReadOnlyCollection<ArticleDto>>> GetManyByArticleNumber(int articleNumber)
    {
        var articles = await _context.Articles
            .Where(a => a.ArticleNumber.ToString()
                .Contains(articleNumber.ToString())).ToListAsync();

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