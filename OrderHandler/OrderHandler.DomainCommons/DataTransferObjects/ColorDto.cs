using System.ComponentModel.DataAnnotations;

namespace OrderHandler.DomainCommons.DataTransferObjects;

public class ColorDto
{
    public Guid Id { get; set; } = Guid.NewGuid();


    [Required, MaxLength(length: 50)]
    public string Color { get; set; } = null!;
}