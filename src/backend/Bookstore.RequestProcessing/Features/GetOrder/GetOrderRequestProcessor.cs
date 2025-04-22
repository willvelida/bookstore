using Bookstore.Data;
using Bookstore.Dtos;

namespace Bookstore.RequestProcessing.Features.GetOrder;

public class GetOrderRequest
{
    public int OrderId { get; set; }
}

public class GetOrderResponse
{
    public OrderDto? Result { get; set; }
}

public class GetOrderRequestProcessor
{
    private readonly BookstoreRepository _repository;

    public GetOrderRequestProcessor(BookstoreRepository repository)
    {
        _repository = repository;
    }

    public async Task<GetOrderResponse> HandleAsync(GetOrderRequest request, CancellationToken cancellationToken = default)
    {
        // For now, return a hardcoded response
        var orderDto = new OrderDto
        {
            OrderId = request.OrderId,
            CustomerId = 1,
            CustomerName = "John Doe",
            OrderDate = DateTime.Now.AddDays(-request.OrderId),
            TotalAmount = 50.99m * request.OrderId,
            Status = "Shipped",
            OrderItems = new List<OrderItemDto>
            {
                new OrderItemDto
                {
                    OrderItemId = 1,
                    BookId = 1,
                    BookTitle = "Sample Book 1",
                    Quantity = 2,
                    UnitPrice = 15.99m
                },
                new OrderItemDto
                {
                    OrderItemId = 2,
                    BookId = 3,
                    BookTitle = "Sample Book 3",
                    Quantity = 1,
                    UnitPrice = 19.01m
                }
            }
        };

        return new GetOrderResponse { Result = orderDto };
    }
}