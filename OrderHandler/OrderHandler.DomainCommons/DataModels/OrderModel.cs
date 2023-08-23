using System.ComponentModel.DataAnnotations;

namespace OrderHandler.DomainCommons.DataModels;

public class OrderModel
{
    public Guid Id { get; set; }


    public ICollection<OrderRowModel> OrderRows = new List<OrderRowModel>();


    [Required, MaxLength(length: 50)]
    public string CustomerName { get; set; } = string.Empty;
}