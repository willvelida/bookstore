using Bookstore.Data;
using Bookstore.Dtos;
using Bookstore.RequestProcessing.Features.GetCategory;
using Moq;
using Xunit;

namespace Bookstore.RequestProcessing.Tests.Features.GetCategory;

public class GetCategoryRequestProcessorTests
{
    private readonly Mock<IBookstoreRepository> _mockRepository;
    private readonly GetCategoryRequestProcessor _processor;

    public GetCategoryRequestProcessorTests()
    {
        _mockRepository = new Mock<IBookstoreRepository>();
        _processor = new GetCategoryRequestProcessor(_mockRepository.Object);
    }

    [Fact]
    public async Task HandleAsync_ValidCategoryId_ReturnsCategoryDto()
    {
        // Arrange
        var categoryId = 1;
        
        // Act
        var result = await _processor.HandleAsync(new GetCategoryRequest { CategoryId = categoryId });

        // Assert
        Assert.NotNull(result.Result);
        Assert.Equal(categoryId, result.Result.CategoryId);
        Assert.Equal($"Category {categoryId}", result.Result.Name);
        Assert.Equal($"Description for category {categoryId}", result.Result.Description);
    }

    [Fact]
    public async Task HandleAsync_MultipleCalls_ReturnsDifferentCategories()
    {
        // Arrange
        var categoryId1 = 1;
        var categoryId2 = 2;
        
        // Act
        var result1 = await _processor.HandleAsync(new GetCategoryRequest { CategoryId = categoryId1 });
        var result2 = await _processor.HandleAsync(new GetCategoryRequest { CategoryId = categoryId2 });

        // Assert
        Assert.NotNull(result1.Result);
        Assert.NotNull(result2.Result);
        Assert.Equal(categoryId1, result1.Result.CategoryId);
        Assert.Equal(categoryId2, result2.Result.CategoryId);
        Assert.Equal($"Category {categoryId1}", result1.Result.Name);
        Assert.Equal($"Category {categoryId2}", result2.Result.Name);
        Assert.Equal($"Description for category {categoryId1}", result1.Result.Description);
        Assert.Equal($"Description for category {categoryId2}", result2.Result.Description);
    }
}