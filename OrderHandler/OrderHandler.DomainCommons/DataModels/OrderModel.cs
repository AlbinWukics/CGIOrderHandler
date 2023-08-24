using OrderHandler.DomainCommons.Services.Enums;
using System.ComponentModel.DataAnnotations;

namespace OrderHandler.DomainCommons.DataModels;

public class OrderModel
{
    public Guid Id { get; set; }


    public ICollection<OrderRowModel> OrderRows = new List<OrderRowModel>();


    [Required, MinLength(0)]
    public int OrderNumber { get; set; }


    [Required, MaxLength(length: 50)]
    public string CustomerName { get; set; } = string.Empty;


    [Required]
    public OrderStatus Status { get; set; }


    [Required]
    public DateTime CreatedAt { get; set; }


    [Required]
    public DateTime LastUpdatedAt { get; set; }
}