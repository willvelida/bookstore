using Bookstore.Data;
using Bookstore.Dtos;

namespace Bookstore.RequestProcessing.Features.GetAllOrders;

public class GetAllOrdersRequest
{
    // No parameters needed for getting all orders
}

public class GetAllOrdersResponse
{
    public List<OrderDto> Orders { get; set; } = new List<OrderDto>();
}

public class GetAllOrdersRequestProcessor
{
    private readonly BookstoreRepository _repository;

    public GetAllOrdersRequestProcessor(BookstoreRepository repository)
    {
        _repository = repository;
    }

    public Task<GetAllOrdersResponse> HandleAsync(GetAllOrdersRequest request, CancellationToken cancellationToken = default)
    {
        // For now, return hardcoded data
        var orders = new List<OrderDto>
        {
            new OrderDto 
            { 
                OrderId = 1, 
                CustomerId = 1, 
                CustomerName = "John Doe", 
                OrderDate = DateTime.Now.AddDays(-1), 
                TotalAmount = 50.99m,
                Status = "Shipped",
                OrderItems = new List<OrderItemDto>
                {
                    new OrderItemDto { OrderItemId = 1, BookId = 1, BookTitle = "Programming in C#", Quantity = 1, UnitPrice = 29.99m },
                    new OrderItemDto { OrderItemId = 2, BookId = 2, BookTitle = "Advanced JavaScript", Quantity = 1, UnitPrice = 21.00m }
                }
            },
            new OrderDto 
            { 
                OrderId = 2, 
                CustomerId = 2, 
                CustomerName = "Jane Smith", 
                OrderDate = DateTime.Now.AddDays(-3), 
                TotalAmount = 34.95m,
                Status = "Delivered",
                OrderItems = new List<OrderItemDto>
                {
                    new OrderItemDto { OrderItemId = 3, BookId = 3, BookTitle = "The Great Novel", Quantity = 1, UnitPrice = 34.95m }
                }
            },
            new OrderDto 
            { 
                OrderId = 3, 
                CustomerId = 1, 
                CustomerName = "John Doe", 
                OrderDate = DateTime.Now.AddDays(-5), 
                TotalAmount = 47.98m,
                Status = "Delivered",
                OrderItems = new List<OrderItemDto>
                {
                    new OrderItemDto { OrderItemId = 4, BookId = 4, BookTitle = "Cooking for Beginners", Quantity = 2, UnitPrice = 23.99m }
                }
            }
        };

        return Task.FromResult(new GetAllOrdersResponse { Orders = orders });
    }
}