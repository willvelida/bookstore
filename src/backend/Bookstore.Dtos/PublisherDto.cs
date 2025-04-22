namespace Bookstore.Dtos;

public class PublisherDto
{
    public int PublisherId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string ContactEmail { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
}