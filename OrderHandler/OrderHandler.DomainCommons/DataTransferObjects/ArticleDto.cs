using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace OrderHandler.DomainCommons.DataTransferObjects;

public class ArticleDto
{
    public Guid Id { get; set; } = Guid.NewGuid();


    public ColorDto? Color { get; set; }


    [Required, MinLength(0)]
    public int ArticleNumber { get; set; }


    [Required, MaxLength(length: 50)]
    public string ArticleName { get; set; } = null!;


    [Required, Precision(precision: 11, scale: 2), MinLength(0)]
    public decimal UnitPrice { get; set; }


    [Required]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;


    [Required]
    public DateTime LastUpdatedAt { get; set; } = DateTime.UtcNow;
}