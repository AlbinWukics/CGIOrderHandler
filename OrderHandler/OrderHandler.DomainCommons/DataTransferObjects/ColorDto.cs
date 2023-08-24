using OrderHandler.DomainCommons.DataModels;
using System.ComponentModel.DataAnnotations;

namespace OrderHandler.DomainCommons.DataTransferObjects;

public class ColorDto
{
    public Guid Id { get; set; } = Guid.NewGuid();


    public ICollection<ArticleDto> Articles { get; set; } = new List<ArticleDto>();


    [Required, MaxLength(length: 50)]
    public string Color { get; set; } = string.Empty;
}