using Bookstore.Data;
using Bookstore.Dtos;

namespace Bookstore.RequestProcessing.Features.GetAuthor;

public interface IGetAuthorRequestProcessor
{
    Task<GetAuthorResponse> HandleAsync(GetAuthorRequest request, CancellationToken cancellationToken = default);
}

public class GetAuthorRequest
{
    public int AuthorId { get; set; }
}

public class GetAuthorResponse
{
    public AuthorDto? Result { get; set; }
}

public class GetAuthorRequestProcessor : IGetAuthorRequestProcessor
{
    private readonly IBookstoreRepository _repository;

    public GetAuthorRequestProcessor(IBookstoreRepository repository)
    {
        _repository = repository;
    }

    public async Task<GetAuthorResponse> HandleAsync(GetAuthorRequest request, CancellationToken cancellationToken = default)
    {
        var author = await _repository.GetAuthorByIdAsync(request.AuthorId);
        
        if (author == null)
        {
            return new GetAuthorResponse { Result = null };
        }

        var authorDto = new AuthorDto
        {
            AuthorId = author.AuthorId,
            Name = author.Name,
            Biography = author.Biography,
            Books = author.Books?.Select(book => new BookDto
            {
                BookId = book.BookId,
                Title = book.Title,
                ISBN = book.ISBN,
                Price = book.Price,
                AuthorId = book.AuthorId,
                AuthorName = author.Name
            }).ToList()
        };

        return new GetAuthorResponse { Result = authorDto };
    }
}