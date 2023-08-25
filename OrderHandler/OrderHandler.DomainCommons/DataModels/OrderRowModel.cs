using System.ComponentModel.DataAnnotations;

namespace OrderHandler.DomainCommons.DataModels;

public class OrderRowModel
{
    public Guid Id { get; set; }


    public OrderModel Order { get; set; } = null!;


    public ArticleModel Article { get; set; } = null!;


    [Required, MinLength(0)]
    public int RowNumber { get; set; }


    [Required, MinLength(0)]
    public int AmountOfArticles { get; set; }
}