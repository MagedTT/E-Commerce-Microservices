using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrderAPI.Core.Dtos;

public class OrderDto
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [Required, Range(1, int.MaxValue)]
    public int ProductId { get; set; }
    [Required, Range(1, int.MaxValue)]
    public int ClientId { get; set; }
    [Required, Range(1, int.MaxValue)]
    public int Quantity { get; set; }
    public DateTime OrderDate { get; set; } = DateTime.Now;
}
