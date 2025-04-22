using Bookstore.Data;
using Bookstore.Dtos;
using Bookstore.RequestProcessing.Features.GetAuthor;
using Moq;
using Xunit;

namespace Bookstore.RequestProcessing.Tests.Features.GetAuthor;

public class GetAuthorRequestProcessorTests
{
    private readonly Mock<IBookstoreRepository> _mockRepository;
    private readonly GetAuthorRequestProcessor _processor;

    public GetAuthorRequestProcessorTests()
    {
        _mockRepository = new Mock<IBookstoreRepository>();
        _processor = new GetAuthorRequestProcessor(_mockRepository.Object);
    }

    [Fact]
    public async Task HandleAsync_ExistingAuthorId_ReturnsAuthorDto()
    {
        // Arrange
        var authorId = 1;
        var books = new List<Book>
        {
            new Book
            {
                BookId = 1,
                Title = "Test Book 1",
                ISBN = "1234567890",
                Price = 19.99m,
                AuthorId = authorId
            },
            new Book
            {
                BookId = 2,
                Title = "Test Book 2",
                ISBN = "0987654321",
                Price = 29.99m,
                AuthorId = authorId
            }
        };

        var author = new Author
        {
            AuthorId = authorId,
            Name = "Test Author",
            Biography = "This is a test author biography",
            Books = books
        };

        _mockRepository.Setup(repo => repo.GetAuthorByIdAsync(authorId))
            .ReturnsAsync(author);

        // Act
        var result = await _processor.HandleAsync(new GetAuthorRequest { AuthorId = authorId });

        // Assert
        Assert.NotNull(result.Result);
        Assert.Equal(authorId, result.Result.AuthorId);
        Assert.Equal("Test Author", result.Result.Name);
        Assert.Equal("This is a test author biography", result.Result.Biography);
        Assert.NotNull(result.Result.Books);
        Assert.Equal(2, result.Result.Books.Count);
        Assert.Equal("Test Book 1", result.Result.Books[0].Title);
        Assert.Equal("Test Book 2", result.Result.Books[1].Title);
    }

    [Fact]
    public async Task HandleAsync_NonExistingAuthorId_ReturnsNull()
    {
        // Arrange
        var nonExistingAuthorId = 999;
        
        _mockRepository.Setup(repo => repo.GetAuthorByIdAsync(nonExistingAuthorId))
            .ReturnsAsync((Author)null);

        // Act
        var result = await _processor.HandleAsync(new GetAuthorRequest { AuthorId = nonExistingAuthorId });

        // Assert
        Assert.Null(result.Result);
    }
}