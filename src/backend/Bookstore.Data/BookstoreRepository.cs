namespace Bookstore.Data;

public class BookstoreRepository : IBookstoreRepository
{
    private static readonly List<Book> _books = new()
    {
        new Book
        {
            BookId = 1,
            Title = "To Kill a Mockingbird",
            ISBN = "978-0-06-112008-4",
            Price = 12.99m,
            AuthorId = 1
        },
        new Book
        {
            BookId = 2,
            Title = "1984",
            ISBN = "978-0-452-28423-4",
            Price = 10.99m,
            AuthorId = 2
        },
        new Book
        {
            BookId = 3,
            Title = "The Great Gatsby",
            ISBN = "978-0-7432-7356-5",
            Price = 14.99m,
            AuthorId = 3
        },
        new Book
        {
            BookId = 4,
            Title = "Animal Farm",
            ISBN = "978-0-452-28424-1",
            Price = 9.99m,
            AuthorId = 2
        }
    };

    private static readonly List<Author> _authors = new()
    {
        new Author
        {
            AuthorId = 1,
            Name = "Harper Lee",
            Biography = "Nelle Harper Lee was an American novelist best known for her 1960 novel To Kill a Mockingbird."
        },
        new Author
        {
            AuthorId = 2,
            Name = "George Orwell",
            Biography = "Eric Arthur Blair, known by his pen name George Orwell, was an English novelist, essayist, journalist, and critic."
        },
        new Author
        {
            AuthorId = 3,
            Name = "F. Scott Fitzgerald",
            Biography = "Francis Scott Key Fitzgerald was an American novelist, essayist, and short story writer."
        }
    };

    public Task<Book?> GetBookByIdAsync(int bookId)
    {
        var book = _books.FirstOrDefault(b => b.BookId == bookId);
        if (book != null)
        {
            book.Author = _authors.FirstOrDefault(a => a.AuthorId == book.AuthorId);
        }
        return Task.FromResult(book);
    }

    public Task<Author?> GetAuthorByIdAsync(int authorId)
    {
        var author = _authors.FirstOrDefault(a => a.AuthorId == authorId);
        if (author != null)
        {
            author.Books = _books.Where(b => b.AuthorId == author.AuthorId).ToList();
        }
        return Task.FromResult(author);
    }
}