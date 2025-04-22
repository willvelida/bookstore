using Bookstore.Data;
using Bookstore.Dtos;

namespace Bookstore.RequestProcessing.Features.GetAllCustomers;

public class GetAllCustomersRequest
{
    // No parameters needed for getting all customers
}

public class GetAllCustomersResponse
{
    public List<CustomerDto> Customers { get; set; } = new List<CustomerDto>();
}

public class GetAllCustomersRequestProcessor
{
    private readonly BookstoreRepository _repository;

    public GetAllCustomersRequestProcessor(BookstoreRepository repository)
    {
        _repository = repository;
    }

    public async Task<GetAllCustomersResponse> HandleAsync(GetAllCustomersRequest request, CancellationToken cancellationToken = default)
    {
        // For now, return hardcoded data
        var customers = new List<CustomerDto>
        {
            new CustomerDto { CustomerId = 1, Name = "John Doe", Email = "john.doe@example.com", Address = "123 Main St, Anytown", PhoneNumber = "555-123-4567" },
            new CustomerDto { CustomerId = 2, Name = "Jane Smith", Email = "jane.smith@example.com", Address = "456 Oak Ave, Somewhere", PhoneNumber = "555-234-5678" },
            new CustomerDto { CustomerId = 3, Name = "Bob Johnson", Email = "bob.johnson@example.com", Address = "789 Pine Rd, Nowhere", PhoneNumber = "555-345-6789" },
            new CustomerDto { CustomerId = 4, Name = "Alice Williams", Email = "alice.williams@example.com", Address = "101 Maple Ln, Anywhere", PhoneNumber = "555-456-7890" }
        };

        return new GetAllCustomersResponse { Customers = customers };
    }
}