using System.ComponentModel.DataAnnotations;

namespace OrderHandler.DomainCommons.DataTransferObjects;

public class OrderRowDto
{
    public Guid Id { get; set; } = Guid.NewGuid();


    public OrderDto OrderId { get; set; } = null!;


    public ArticleDto ArticleId { get; set; } = null!;


    [Required, MinLength(0)]
    public int RowNumber { get; set; }


    [Required, MinLength(0)]
    public int AmountOfArticles { get; set; }
}