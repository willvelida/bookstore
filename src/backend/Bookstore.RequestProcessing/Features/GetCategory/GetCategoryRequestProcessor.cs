using Bookstore.Data;
using Bookstore.Dtos;

namespace Bookstore.RequestProcessing.Features.GetCategory;

public interface IGetCategoryRequestProcessor
{
    Task<GetCategoryResponse> HandleAsync(GetCategoryRequest request, CancellationToken cancellationToken = default);
}

public class GetCategoryRequest
{
    public int CategoryId { get; set; }
}

public class GetCategoryResponse
{
    public CategoryDto? Result { get; set; }
}

public class GetCategoryRequestProcessor : IGetCategoryRequestProcessor
{
    private readonly IBookstoreRepository _repository;

    public GetCategoryRequestProcessor(IBookstoreRepository repository)
    {
        _repository = repository;
    }

    public async Task<GetCategoryResponse> HandleAsync(GetCategoryRequest request, CancellationToken cancellationToken = default)
    {
        // For now, return a hardcoded response
        var categoryDto = new CategoryDto
        {
            CategoryId = request.CategoryId,
            Name = $"Category {request.CategoryId}",
            Description = $"Description for category {request.CategoryId}"
        };

        return new GetCategoryResponse { Result = categoryDto };
    }
}