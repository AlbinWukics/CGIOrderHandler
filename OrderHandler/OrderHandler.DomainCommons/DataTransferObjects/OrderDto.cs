using OrderHandler.DomainCommons.DataModels;
using System.ComponentModel.DataAnnotations;

namespace OrderHandler.DomainCommons.DataTransferObjects;

public class OrderDto
{
    public Guid Id { get; set; } = Guid.NewGuid();


    public ICollection<OrderRowModel> OrderRows = new List<OrderRowModel>();


    [Required, MaxLength(length: 50)]
    public string CustomerName { get; set; } = null!;
}