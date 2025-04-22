namespace Bookstore.Dtos;

public class AuthorDto
{
    public int AuthorId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Biography { get; set; } = string.Empty;
    public List<BookDto>? Books { get; set; }
}