using System.ComponentModel.DataAnnotations;

namespace Bookstore.Data;

public class Author
{
    [Key]
    public int AuthorId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Biography { get; set; } = string.Empty;
    public List<Book>? Books { get; set; }
}