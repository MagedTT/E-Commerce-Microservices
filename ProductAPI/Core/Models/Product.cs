using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductAPI.Core.Models;

public class Product
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [Required]
    public string? Name { get; set; }
    [Required, Range(1, int.MaxValue)]
    public int Quantity { get; set; }
    [Required, DataType(DataType.Currency)]
    public decimal Price { get; set; }
}