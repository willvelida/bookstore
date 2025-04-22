using Bookstore.Data;
using Bookstore.Dtos;

namespace Bookstore.RequestProcessing.Features.GetBook;

public class GetBookRequest
{
    public int BookId { get; set; }
}

public class GetBookResponse
{
    public BookDto? Result { get; set; }
}

public class GetBookRequestProcessor
{
    private readonly BookstoreRepository _repository;

    public GetBookRequestProcessor(BookstoreRepository repository)
    {
        _repository = repository;
    }

    public async Task<GetBookResponse> HandleAsync(GetBookRequest request, CancellationToken cancellationToken = default)
    {
        var book = await _repository.GetBookByIdAsync(request.BookId);
        
        if (book == null)
        {
            return new GetBookResponse { Result = null };
        }

        var bookDto = new BookDto
        {
            BookId = book.BookId,
            Title = book.Title,
            ISBN = book.ISBN,
            Price = book.Price,
            AuthorId = book.AuthorId,
            AuthorName = book.Author?.Name ?? string.Empty
        };

        return new GetBookResponse { Result = bookDto };
    }
}