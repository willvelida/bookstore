namespace Bookstore.Dtos;

public class OrderItemDto
{
    public int OrderId { get; set; }
    public int BookId { get; set; }
    public string BookTitle { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Discount { get; set; }
    public decimal Subtotal => Quantity * UnitPrice * (1 - Discount);
}