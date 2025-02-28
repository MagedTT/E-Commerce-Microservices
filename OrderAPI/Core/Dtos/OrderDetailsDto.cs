namespace OrderAPI.Core.Dtos;

public class OrderDetailsDto
{
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public int Clientid { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? ProductName { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public DateTime OrderDate { get; set; } = DateTime.Now;
}