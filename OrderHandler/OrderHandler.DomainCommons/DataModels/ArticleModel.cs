using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace OrderHandler.DomainCommons.DataModels;

public class ArticleModel
{
    public Guid Id { get; set; }


    public ColorModel? Color { get; set; }


    [Required, MinLength(0)]
    public int ArticleNumber { get; set; }


    [Required, MaxLength(length: 50)]
    public string ArticleName { get; set; } = string.Empty;


    [Required, Precision(precision: 11, scale: 2), MinLength(0)]
    public decimal UnitPrice { get; set; }
}