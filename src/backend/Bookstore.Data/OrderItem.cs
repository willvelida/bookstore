using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.Data;

[PrimaryKey(nameof(OrderId), nameof(BookId))]
public class OrderItem
{
    [ForeignKey("Order")]
    public int OrderId { get; set; }
    public Order? Order { get; set; }
    
    [ForeignKey("Book")]
    public int BookId { get; set; }
    public Book? Book { get; set; }
    
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Discount { get; set; }
    
    [NotMapped]
    public decimal Subtotal => Quantity * UnitPrice * (1 - Discount);
}