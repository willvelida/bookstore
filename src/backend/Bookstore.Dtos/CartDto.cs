namespace Bookstore.Dtos;

public class CartDto
{
    public int CartId { get; set; }
    public int CustomerId { get; set; }
    public List<CartItemDto> Items { get; set; } = new List<CartItemDto>();
    public decimal TotalAmount => Items?.Sum(i => i.Subtotal) ?? 0;
}