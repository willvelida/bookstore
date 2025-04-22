namespace Bookstore.Dtos;

public class CartItemDto
{
    public int CartItemId { get; set; }
    public int BookId { get; set; }
    public string BookTitle { get; set; } = string.Empty;
    public decimal UnitPrice { get; set; }
    public int Quantity { get; set; }
    public decimal Subtotal => Quantity * UnitPrice;
}