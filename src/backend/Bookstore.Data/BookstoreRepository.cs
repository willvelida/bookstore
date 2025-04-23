namespace Bookstore.Data;

public class BookstoreRepository : IBookstoreRepository
{
    private static readonly List<Book> _books = new()
    {
        new Book
        {
            BookId = 1,
            Title = "To Kill a Mockingbird",
            ISBN = "978-0-06-112008-4",
            Price = 12.99m,
            AuthorId = 1
        },
        new Book
        {
            BookId = 2,
            Title = "1984",
            ISBN = "978-0-452-28423-4",
            Price = 10.99m,
            AuthorId = 2
        },
        new Book
        {
            BookId = 3,
            Title = "The Great Gatsby",
            ISBN = "978-0-7432-7356-5",
            Price = 14.99m,
            AuthorId = 3
        },
        new Book
        {
            BookId = 4,
            Title = "Animal Farm",
            ISBN = "978-0-452-28424-1",
            Price = 9.99m,
            AuthorId = 2
        }
    };

    private static readonly List<Author> _authors = new()
    {
        new Author
        {
            AuthorId = 1,
            Name = "Harper Lee",
            Biography = "Nelle Harper Lee was an American novelist best known for her 1960 novel To Kill a Mockingbird."
        },
        new Author
        {
            AuthorId = 2,
            Name = "George Orwell",
            Biography = "Eric Arthur Blair, known by his pen name George Orwell, was an English novelist, essayist, journalist, and critic."
        },
        new Author
        {
            AuthorId = 3,
            Name = "F. Scott Fitzgerald",
            Biography = "Francis Scott Key Fitzgerald was an American novelist, essayist, and short story writer."
        }
    };

    private static readonly List<Customer> _customers = new()
    {
        new Customer
        {
            CustomerId = 1,
            Name = "John Doe",
            Email = "john.doe@example.com",
            Address = "123 Main St, Anytown, USA",
            PhoneNumber = "555-123-4567"
        },
        new Customer
        {
            CustomerId = 2,
            Name = "Jane Smith",
            Email = "jane.smith@example.com",
            Address = "456 Oak Ave, Somewhere, USA",
            PhoneNumber = "555-987-6543"
        }
    };

    private static readonly List<Order> _orders = new()
    {
        new Order
        {
            OrderId = 1,
            CustomerId = 1,
            OrderDate = DateTime.Now.AddDays(-5),
            TotalAmount = 23.98m,
            Status = "Delivered"
        },
        new Order
        {
            OrderId = 2,
            CustomerId = 2,
            OrderDate = DateTime.Now.AddDays(-2),
            TotalAmount = 14.99m,
            Status = "Processing"
        }
    };

    private static readonly List<OrderItem> _orderItems = new()
    {
        new OrderItem
        {
            OrderId = 1,
            BookId = 1,
            Quantity = 1,
            UnitPrice = 12.99m,
            Discount = 0
        },
        new OrderItem
        {
            OrderId = 1,
            BookId = 2,
            Quantity = 1,
            UnitPrice = 10.99m,
            Discount = 0
        },
        new OrderItem
        {
            OrderId = 2,
            BookId = 3,
            Quantity = 1,
            UnitPrice = 14.99m,
            Discount = 0
        }
    };

    public Task<Book?> GetBookByIdAsync(int bookId)
    {
        var book = _books.FirstOrDefault(b => b.BookId == bookId);
        if (book != null)
        {
            book.Author = _authors.FirstOrDefault(a => a.AuthorId == book.AuthorId);
        }
        return Task.FromResult(book);
    }

    public Task<Author?> GetAuthorByIdAsync(int authorId)
    {
        var author = _authors.FirstOrDefault(a => a.AuthorId == authorId);
        if (author != null)
        {
            author.Books = _books.Where(b => b.AuthorId == author.AuthorId).ToList();
        }
        return Task.FromResult(author);
    }

    public Task<IEnumerable<Order>> GetAllOrdersAsync()
    {
        foreach (var order in _orders)
        {
            order.Customer = _customers.FirstOrDefault(c => c.CustomerId == order.CustomerId);
            order.OrderItems = _orderItems.Where(oi => oi.OrderId == order.OrderId).ToList();
        }
        
        return Task.FromResult<IEnumerable<Order>>(_orders);
    }

    public Task<Order?> GetOrderByIdAsync(int orderId)
    {
        var order = _orders.FirstOrDefault(o => o.OrderId == orderId);
        if (order != null)
        {
            order.Customer = _customers.FirstOrDefault(c => c.CustomerId == order.CustomerId);
            order.OrderItems = _orderItems.Where(oi => oi.OrderId == order.OrderId).ToList();
            
            foreach (var item in order.OrderItems)
            {
                item.Book = _books.FirstOrDefault(b => b.BookId == item.BookId);
                item.Order = order;
            }
        }
        return Task.FromResult(order);
    }

    public Task<Order> AddOrderAsync(Order order)
    {
        if (order.OrderId <= 0)
        {
            order.OrderId = _orders.Count > 0 ? _orders.Max(o => o.OrderId) + 1 : 1;
        }
        
        _orders.Add(order);
        return Task.FromResult(order);
    }

    public Task UpdateOrderAsync(Order order)
    {
        var existingOrder = _orders.FirstOrDefault(o => o.OrderId == order.OrderId);
        if (existingOrder != null)
        {
            existingOrder.CustomerId = order.CustomerId;
            existingOrder.OrderDate = order.OrderDate;
            existingOrder.TotalAmount = order.TotalAmount;
            existingOrder.Status = order.Status;
        }
        return Task.CompletedTask;
    }

    public Task DeleteOrderAsync(int orderId)
    {
        var orderToRemove = _orders.FirstOrDefault(o => o.OrderId == orderId);
        if (orderToRemove != null)
        {
            _orders.Remove(orderToRemove);
            
            var itemsToRemove = _orderItems.Where(oi => oi.OrderId == orderId).ToList();
            foreach (var item in itemsToRemove)
            {
                _orderItems.Remove(item);
            }
        }
        return Task.CompletedTask;
    }

    public Task<OrderItem?> GetOrderItemAsync(int orderId, int bookId)
    {
        var orderItem = _orderItems.FirstOrDefault(oi => oi.OrderId == orderId && oi.BookId == bookId);
        if (orderItem != null)
        {
            orderItem.Book = _books.FirstOrDefault(b => b.BookId == orderItem.BookId);
            orderItem.Order = _orders.FirstOrDefault(o => o.OrderId == orderItem.OrderId);
        }
        return Task.FromResult(orderItem);
    }

    public Task<IEnumerable<OrderItem>> GetOrderItemsByOrderIdAsync(int orderId)
    {
        var orderItems = _orderItems.Where(oi => oi.OrderId == orderId).ToList();
        foreach (var item in orderItems)
        {
            item.Book = _books.FirstOrDefault(b => b.BookId == item.BookId);
            item.Order = _orders.FirstOrDefault(o => o.OrderId == item.OrderId);
        }
        return Task.FromResult<IEnumerable<OrderItem>>(orderItems);
    }

    public Task<OrderItem> AddOrderItemAsync(OrderItem orderItem)
    {
        var existingItem = _orderItems.FirstOrDefault(oi => 
            oi.OrderId == orderItem.OrderId && oi.BookId == orderItem.BookId);
        
        if (existingItem == null)
        {
            _orderItems.Add(orderItem);
            
            var order = _orders.FirstOrDefault(o => o.OrderId == orderItem.OrderId);
            if (order != null)
            {
                order.TotalAmount += orderItem.Quantity * orderItem.UnitPrice * (1 - orderItem.Discount);
            }
            
            return Task.FromResult(orderItem);
        }
        
        existingItem.Quantity += orderItem.Quantity;
        
        var existingOrder = _orders.FirstOrDefault(o => o.OrderId == orderItem.OrderId);
        if (existingOrder != null)
        {
            existingOrder.TotalAmount += orderItem.Quantity * orderItem.UnitPrice * (1 - orderItem.Discount);
        }
        
        return Task.FromResult(existingItem);
    }

    public Task UpdateOrderItemAsync(OrderItem orderItem)
    {
        var existingItem = _orderItems.FirstOrDefault(oi => 
            oi.OrderId == orderItem.OrderId && oi.BookId == orderItem.BookId);
        
        if (existingItem != null)
        {
            var priceDifference = 
                (orderItem.Quantity * orderItem.UnitPrice * (1 - orderItem.Discount)) - 
                (existingItem.Quantity * existingItem.UnitPrice * (1 - existingItem.Discount));
            
            existingItem.Quantity = orderItem.Quantity;
            existingItem.UnitPrice = orderItem.UnitPrice;
            existingItem.Discount = orderItem.Discount;
            
            var order = _orders.FirstOrDefault(o => o.OrderId == orderItem.OrderId);
            if (order != null && priceDifference != 0)
            {
                order.TotalAmount += priceDifference;
            }
        }
        
        return Task.CompletedTask;
    }

    public Task DeleteOrderItemAsync(int orderId, int bookId)
    {
        var itemToRemove = _orderItems.FirstOrDefault(oi => 
            oi.OrderId == orderId && oi.BookId == bookId);
        
        if (itemToRemove != null)
        {
            var order = _orders.FirstOrDefault(o => o.OrderId == orderId);
            if (order != null)
            {
                order.TotalAmount -= itemToRemove.Quantity * itemToRemove.UnitPrice * (1 - itemToRemove.Discount);
            }
            
            _orderItems.Remove(itemToRemove);
        }
        
        return Task.CompletedTask;
    }
}