using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bookstore.Data;

public class Order
{
    [Key]
    public int OrderId { get; set; }
    
    [ForeignKey("Customer")]
    public int CustomerId { get; set; }
    public Customer? Customer { get; set; }
    
    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get; set; }
    public string Status { get; set; } = string.Empty;
    
    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}