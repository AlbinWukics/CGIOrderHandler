using Microsoft.EntityFrameworkCore;
using OrderHandler.DomainCommons.DataModels;
using System.ComponentModel.DataAnnotations;

namespace OrderHandler.DomainCommons.DataTransferObjects;

public class ArticleDto
{
    public Guid Id { get; set; } = Guid.NewGuid();


    public ColorModel? Color { get; set; }


    [Required, MinLength(0)]
    public int ArticleNumber { get; set; }


    [Required, MaxLength(length: 50)]
    public string ArticleName { get; set; } = null!;


    [Required, Precision(precision: 11, scale: 2), MinLength(0)]
    public decimal UnitPrice { get; set; }
}