namespace Bookstore.Data;

public interface IBookstoreRepository
{
    Task<Book?> GetBookByIdAsync(int bookId);
    Task<Author?> GetAuthorByIdAsync(int authorId);
}