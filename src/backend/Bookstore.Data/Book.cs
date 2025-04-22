using System.ComponentModel.DataAnnotations;

namespace Bookstore.Data;

public class Book
{
    [Key]
    public int BookId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string ISBN { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int AuthorId { get; set; }
    public Author? Author { get; set; }
}