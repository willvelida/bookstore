using Bookstore.Data;
using Bookstore.Dtos;

namespace Bookstore.RequestProcessing.Features.GetCustomer;

public class GetCustomerRequest
{
    public int CustomerId { get; set; }
}

public class GetCustomerResponse
{
    public CustomerDto? Result { get; set; }
}

public class GetCustomerRequestProcessor
{
    private readonly IBookstoreRepository _repository;

    public GetCustomerRequestProcessor(IBookstoreRepository repository)
    {
        _repository = repository;
    }

    public Task<GetCustomerResponse> HandleAsync(GetCustomerRequest request, CancellationToken cancellationToken = default)
    {
        // For now, return a hardcoded response
        var customerDto = new CustomerDto
        {
            CustomerId = request.CustomerId,
            Name = $"Customer {request.CustomerId}",
            Email = $"customer{request.CustomerId}@example.com",
            Address = $"456 Customer Lane, City {request.CustomerId}",
            PhoneNumber = $"555-123-{request.CustomerId:D4}"
        };

        return Task.FromResult(new GetCustomerResponse { Result = customerDto });
    }
}