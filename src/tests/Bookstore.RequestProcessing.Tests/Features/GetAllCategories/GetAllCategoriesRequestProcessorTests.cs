using Bookstore.Data;
using Bookstore.Dtos;
using Bookstore.RequestProcessing.Features.GetAllCategories;
using Moq;
using Xunit;

namespace Bookstore.RequestProcessing.Tests.Features.GetAllCategories;

public class GetAllCategoriesRequestProcessorTests
{
    private readonly Mock<IBookstoreRepository> _mockRepository;
    private readonly GetAllCategoriesRequestProcessor _processor;

    public GetAllCategoriesRequestProcessorTests()
    {
        _mockRepository = new Mock<IBookstoreRepository>();
        _processor = new GetAllCategoriesRequestProcessor(_mockRepository.Object);
    }

    [Fact]
    public async Task HandleAsync_ReturnsAllCategories()
    {
        // Arrange
        var request = new GetAllCategoriesRequest();
        
        // Act
        var result = await _processor.HandleAsync(request);

        // Assert
        Assert.NotNull(result.Categories);
        Assert.Equal(4, result.Categories.Count);
        Assert.Contains(result.Categories, c => c.CategoryId == 1 && c.Name == "Fiction");
        Assert.Contains(result.Categories, c => c.CategoryId == 2 && c.Name == "Non-Fiction");
        Assert.Contains(result.Categories, c => c.CategoryId == 3 && c.Name == "Science Fiction");
        Assert.Contains(result.Categories, c => c.CategoryId == 4 && c.Name == "Fantasy");
    }

    [Fact]
    public async Task HandleAsync_ReturnsCategoriesWithDescriptions()
    {
        // Arrange
        var request = new GetAllCategoriesRequest();
        
        // Act
        var result = await _processor.HandleAsync(request);

        // Assert
        Assert.NotNull(result.Categories);
        Assert.All(result.Categories, category => 
        {
            Assert.NotNull(category.Description);
            Assert.NotEmpty(category.Description);
        });
    }
}