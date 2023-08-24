using System.ComponentModel.DataAnnotations;

namespace OrderHandler.DomainCommons.DataModels;

// Initial thought was to use an enum because colors don't change, but customer might want to add in their own colors, so I made this possible by instead creating a db table.
public class ColorModel
{
    public Guid Id { get; set; }


    public ICollection<ArticleModel> Articles { get; set; } = new List<ArticleModel>();


    [Required, MaxLength(length: 50)]
    public string Color { get; set; } = string.Empty;
}