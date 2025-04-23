using Bookstore.Data;
using Bookstore.Dtos;

namespace Bookstore.RequestProcessing.Features.GetAllCategories;

public interface IGetAllCategoriesRequestProcessor
{
    Task<GetAllCategoriesResponse> HandleAsync(GetAllCategoriesRequest request, CancellationToken cancellationToken = default);
}

public class GetAllCategoriesRequest
{
    // No parameters needed for getting all categories
}

public class GetAllCategoriesResponse
{
    public List<CategoryDto> Categories { get; set; } = new List<CategoryDto>();
}

public class GetAllCategoriesRequestProcessor : IGetAllCategoriesRequestProcessor
{
    private readonly IBookstoreRepository _repository;

    public GetAllCategoriesRequestProcessor(IBookstoreRepository repository)
    {
        _repository = repository;
    }

    public Task<GetAllCategoriesResponse> HandleAsync(GetAllCategoriesRequest request, CancellationToken cancellationToken = default)
    {
        // For now, return hardcoded data
        var categories = new List<CategoryDto>
        {
            new CategoryDto { CategoryId = 1, Name = "Fiction", Description = "Fictional literature" },
            new CategoryDto { CategoryId = 2, Name = "Non-Fiction", Description = "Educational and informative books" },
            new CategoryDto { CategoryId = 3, Name = "Science Fiction", Description = "Books about futuristic science and technology" },
            new CategoryDto { CategoryId = 4, Name = "Fantasy", Description = "Books featuring magic and supernatural elements" }
        };

        return Task.FromResult(new GetAllCategoriesResponse { Categories = categories });
    }
}