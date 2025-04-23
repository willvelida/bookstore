using System.ComponentModel.DataAnnotations;

namespace Bookstore.Data;

public class Customer
{
    [Key]
    public int CustomerId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    
    public ICollection<Order> Orders { get; set; } = new List<Order>();
}