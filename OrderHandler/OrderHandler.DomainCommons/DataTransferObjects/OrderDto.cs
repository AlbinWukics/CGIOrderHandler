using System.ComponentModel.DataAnnotations;
using OrderHandler.DomainCommons.Services.Enums;

namespace OrderHandler.DomainCommons.DataTransferObjects;

public class OrderDto
{
    public Guid Id { get; set; } = Guid.NewGuid();


    public ICollection<OrderRowDto> OrderRows = new List<OrderRowDto>();


    [Required, MinLength(0)]
    public int OrderNumber { get; set; }


    [Required, MaxLength(length: 50)]
    public string CustomerName { get; set; } = null!;


    [Required]
    public OrderStatus Status { get; set; } = OrderStatus.NewOrder;


    [Required]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;


    [Required]
    public DateTime LastUpdatedAt { get; set; } = DateTime.UtcNow;
}