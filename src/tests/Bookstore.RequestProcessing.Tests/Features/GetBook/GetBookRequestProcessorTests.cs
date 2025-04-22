using Bookstore.Data;
using Bookstore.Dtos;
using Bookstore.RequestProcessing.Features.GetBook;
using Moq;
using Xunit;

namespace Bookstore.RequestProcessing.Tests.Features.GetBook;

public class GetBookRequestProcessorTests
{
    private readonly Mock<IBookstoreRepository> _mockRepository;
    private readonly GetBookRequestProcessor _processor;

    public GetBookRequestProcessorTests()
    {
        _mockRepository = new Mock<IBookstoreRepository>();
        _processor = new GetBookRequestProcessor(_mockRepository.Object);
    }

    [Fact]
    public async Task HandleAsync_ExistingBookId_ReturnsBookDto()
    {
        // Arrange
        var bookId = 1;
        var book = new Book
        {
            BookId = bookId,
            Title = "Test Book",
            ISBN = "1234567890",
            Price = 19.99m,
            AuthorId = 1,
            Author = new Author { Name = "Test Author" }
        };

        _mockRepository.Setup(repo => repo.GetBookByIdAsync(bookId))
            .ReturnsAsync(book);

        // Act
        var result = await _processor.HandleAsync(new GetBookRequest { BookId = bookId });

        // Assert
        Assert.NotNull(result.Result);
        Assert.Equal(bookId, result.Result.BookId);
        Assert.Equal("Test Book", result.Result.Title);
        Assert.Equal("1234567890", result.Result.ISBN);
        Assert.Equal(19.99m, result.Result.Price);
        Assert.Equal(1, result.Result.AuthorId);
        Assert.Equal("Test Author", result.Result.AuthorName);
    }

    [Fact]
    public async Task HandleAsync_NonExistingBookId_ReturnsNull()
    {
        // Arrange
        var nonExistingBookId = 999;
        
        _mockRepository.Setup(repo => repo.GetBookByIdAsync(nonExistingBookId))
            .ReturnsAsync((Book)null);

        // Act
        var result = await _processor.HandleAsync(new GetBookRequest { BookId = nonExistingBookId });

        // Assert
        Assert.Null(result.Result);
    }
}