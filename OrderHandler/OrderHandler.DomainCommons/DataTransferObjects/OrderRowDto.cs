using System.ComponentModel.DataAnnotations;

namespace OrderHandler.DomainCommons.DataTransferObjects;

public class OrderRowDto
{
    public Guid Id { get; set; } = Guid.NewGuid();


    public OrderDto Order { get; set; } = null!;


    public ArticleDto Article { get; set; } = null!;


    [Required]
    public int RowNumber { get; set; }


    [Required]
    public int AmountOfArticles { get; set; }
}