namespace Bookstore.Data;

public interface IBookstoreRepository
{
    Task<Book?> GetBookByIdAsync(int bookId);
    Task<Author?> GetAuthorByIdAsync(int authorId);
    
    // Order related methods
    Task<IEnumerable<Order>> GetAllOrdersAsync();
    Task<Order?> GetOrderByIdAsync(int orderId);
    Task<Order> AddOrderAsync(Order order);
    Task UpdateOrderAsync(Order order);
    Task DeleteOrderAsync(int orderId);
    
    // OrderItem related methods
    Task<OrderItem?> GetOrderItemAsync(int orderId, int bookId);
    Task<IEnumerable<OrderItem>> GetOrderItemsByOrderIdAsync(int orderId);
    Task<OrderItem> AddOrderItemAsync(OrderItem orderItem);
    Task UpdateOrderItemAsync(OrderItem orderItem);
    Task DeleteOrderItemAsync(int orderId, int bookId);
}