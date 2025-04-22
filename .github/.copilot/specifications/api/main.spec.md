# Specification: Bookstore API

**Version:** 1.0

**Last Updated:** 2025-04-22

**Owner:** Will Velida

## 1. Purpose & Scope

This specification defines the structure, architecture, and implementation details for the Bookstore API using ASP.NET Core Minimal API. The API will provide all necessary endpoints to manage books, authors, categories, publishers, customers, orders, reviews, and shopping carts in the bookstore application.

## 2. Core Principles & Guidelines

### Project Organization

* The solution will follow Clean Architecture principles with clear separation of concerns.
* Project will be organized into the following layers:
  * `Bookstore.Api` - API endpoints and configuration
  * `Bookstore.Data` - Data access and entity models
  * `Bookstore.RequestProcessing` - Business logic and use cases
  * `Bookstore.Dtos` - Data transfer objects

### API Design

* Use Minimal API approach for all endpoints to reduce boilerplate code.
* Organize endpoints into extension methods by entity type for better maintainability.
* Follow RESTful principles for resource naming and HTTP methods.
* Implement proper HTTP status codes for all responses.
* Implement validation for all request models.
* Ensure all endpoints are documented with OpenAPI/Swagger.

### Naming Conventions

* Use plural nouns for resource collections (e.g., `/api/books`, `/api/authors`).
* Use consistent casing: camelCase for JSON properties, PascalCase for C# properties.
* Use descriptive names for endpoints, classes, and methods.
* Follow feature folder organization for RequestProcessors (e.g., `Features/GetBook`, `Features/CreateBook`).

## 3. Rationale & Context

ASP.NET Core Minimal API provides a more concise and performant way to build APIs compared to the traditional controller-based approach. By organizing endpoints into extension methods, we maintain clean code structure while benefiting from the simplicity of Minimal APIs.

The separation of concerns through distinct project layers ensures that business logic is decoupled from data access and API concerns, making the application more maintainable and testable.

## 4. API Endpoints

### Books

```csharp
// GET all books
app.MapGet("api/books", async (GetAllBooksRequestProcessor processor) => 
    Results.Ok(await processor.HandleAsync(new GetAllBooksRequest())));

// GET book by ID
app.MapGet("api/books/{id}", async (int id, GetBookRequestProcessor processor) =>
{
    var result = await processor.HandleAsync(new GetBookRequest { BookId = id });
    return result.Result != null ? Results.Ok(result.Result) : Results.NotFound();
});

// POST new book
app.MapPost("api/books", async (BookDto bookDto, CreateBookRequestProcessor processor) =>
{
    var result = await processor.HandleAsync(new CreateBookRequest { Book = bookDto });
    return Results.Created($"/api/books/{result.BookId}", result);
});

// PUT update book
app.MapPut("api/books/{id}", async (int id, BookDto bookDto, UpdateBookRequestProcessor processor) =>
{
    var result = await processor.HandleAsync(new UpdateBookRequest { BookId = id, Book = bookDto });
    return result.WasUpdated ? Results.NoContent() : Results.NotFound();
});

// DELETE book
app.MapDelete("api/books/{id}", async (int id, DeleteBookRequestProcessor processor) =>
{
    var result = await processor.HandleAsync(new DeleteBookRequest { BookId = id });
    return result.WasDeleted ? Results.NoContent() : Results.NotFound();
});
```

### Authors

```csharp
// GET all authors
app.MapGet("api/authors", async (GetAllAuthorsRequestProcessor processor) => 
    Results.Ok(await processor.HandleAsync(new GetAllAuthorsRequest())));

// GET author by ID
app.MapGet("api/authors/{id}", async (int id, GetAuthorRequestProcessor processor) =>
{
    var result = await processor.HandleAsync(new GetAuthorRequest { AuthorId = id });
    return result.Result != null ? Results.Ok(result.Result) : Results.NotFound();
});

// GET books by author
app.MapGet("api/authors/{id}/books", async (int id, GetAuthorBooksRequestProcessor processor) =>
{
    var result = await processor.HandleAsync(new GetAuthorBooksRequest { AuthorId = id });
    return result.Result != null ? Results.Ok(result.Result) : Results.NotFound();
});

// POST new author
app.MapPost("api/authors", async (AuthorDto authorDto, CreateAuthorRequestProcessor processor) =>
{
    var result = await processor.HandleAsync(new CreateAuthorRequest { Author = authorDto });
    return Results.Created($"/api/authors/{result.AuthorId}", result);
});

// PUT update author
app.MapPut("api/authors/{id}", async (int id, AuthorDto authorDto, UpdateAuthorRequestProcessor processor) =>
{
    var result = await processor.HandleAsync(new UpdateAuthorRequest { AuthorId = id, Author = authorDto });
    return result.WasUpdated ? Results.NoContent() : Results.NotFound();
});

// DELETE author
app.MapDelete("api/authors/{id}", async (int id, DeleteAuthorRequestProcessor processor) =>
{
    var result = await processor.HandleAsync(new DeleteAuthorRequest { AuthorId = id });
    return result.WasDeleted ? Results.NoContent() : Results.NotFound();
});
```

### Categories

```csharp
// GET all categories
app.MapGet("api/categories", async (GetAllCategoriesRequestProcessor processor) => 
    Results.Ok(await processor.HandleAsync(new GetAllCategoriesRequest())));

// GET category by ID
app.MapGet("api/categories/{id}", async (int id, GetCategoryRequestProcessor processor) =>
{
    var result = await processor.HandleAsync(new GetCategoryRequest { CategoryId = id });
    return result.Result != null ? Results.Ok(result.Result) : Results.NotFound();
});

// GET books by category
app.MapGet("api/categories/{id}/books", async (int id, GetCategoryBooksRequestProcessor processor) =>
{
    var result = await processor.HandleAsync(new GetCategoryBooksRequest { CategoryId = id });
    return result.Result != null ? Results.Ok(result.Result) : Results.NotFound();
});

// POST new category
app.MapPost("api/categories", async (CategoryDto categoryDto, CreateCategoryRequestProcessor processor) =>
{
    var result = await processor.HandleAsync(new CreateCategoryRequest { Category = categoryDto });
    return Results.Created($"/api/categories/{result.CategoryId}", result);
});

// PUT update category
app.MapPut("api/categories/{id}", async (int id, CategoryDto categoryDto, UpdateCategoryRequestProcessor processor) =>
{
    var result = await processor.HandleAsync(new UpdateCategoryRequest { CategoryId = id, Category = categoryDto });
    return result.WasUpdated ? Results.NoContent() : Results.NotFound();
});

// DELETE category
app.MapDelete("api/categories/{id}", async (int id, DeleteCategoryRequestProcessor processor) =>
{
    var result = await processor.HandleAsync(new DeleteCategoryRequest { CategoryId = id });
    return result.WasDeleted ? Results.NoContent() : Results.NotFound();
});
```

### Publishers

```csharp
// GET all publishers
app.MapGet("api/publishers", async (GetAllPublishersRequestProcessor processor) => 
    Results.Ok(await processor.HandleAsync(new GetAllPublishersRequest())));

// GET publisher by ID
app.MapGet("api/publishers/{id}", async (int id, GetPublisherRequestProcessor processor) =>
{
    var result = await processor.HandleAsync(new GetPublisherRequest { PublisherId = id });
    return result.Result != null ? Results.Ok(result.Result) : Results.NotFound();
});

// GET books by publisher
app.MapGet("api/publishers/{id}/books", async (int id, GetPublisherBooksRequestProcessor processor) =>
{
    var result = await processor.HandleAsync(new GetPublisherBooksRequest { PublisherId = id });
    return result.Result != null ? Results.Ok(result.Result) : Results.NotFound();
});

// POST new publisher
app.MapPost("api/publishers", async (PublisherDto publisherDto, CreatePublisherRequestProcessor processor) =>
{
    var result = await processor.HandleAsync(new CreatePublisherRequest { Publisher = publisherDto });
    return Results.Created($"/api/publishers/{result.PublisherId}", result);
});

// PUT update publisher
app.MapPut("api/publishers/{id}", async (int id, PublisherDto publisherDto, UpdatePublisherRequestProcessor processor) =>
{
    var result = await processor.HandleAsync(new UpdatePublisherRequest { PublisherId = id, Publisher = publisherDto });
    return result.WasUpdated ? Results.NoContent() : Results.NotFound();
});

// DELETE publisher
app.MapDelete("api/publishers/{id}", async (int id, DeletePublisherRequestProcessor processor) =>
{
    var result = await processor.HandleAsync(new DeletePublisherRequest { PublisherId = id });
    return result.WasDeleted ? Results.NoContent() : Results.NotFound();
});
```

### Customers

```csharp
// GET all customers
app.MapGet("api/customers", async (GetAllCustomersRequestProcessor processor) => 
    Results.Ok(await processor.HandleAsync(new GetAllCustomersRequest())));

// GET customer by ID
app.MapGet("api/customers/{id}", async (int id, GetCustomerRequestProcessor processor) =>
{
    var result = await processor.HandleAsync(new GetCustomerRequest { CustomerId = id });
    return result.Result != null ? Results.Ok(result.Result) : Results.NotFound();
});

// GET customer orders
app.MapGet("api/customers/{id}/orders", async (int id, GetCustomerOrdersRequestProcessor processor) =>
{
    var result = await processor.HandleAsync(new GetCustomerOrdersRequest { CustomerId = id });
    return result.Result != null ? Results.Ok(result.Result) : Results.NotFound();
});

// GET customer reviews
app.MapGet("api/customers/{id}/reviews", async (int id, GetCustomerReviewsRequestProcessor processor) =>
{
    var result = await processor.HandleAsync(new GetCustomerReviewsRequest { CustomerId = id });
    return result.Result != null ? Results.Ok(result.Result) : Results.NotFound();
});

// POST new customer
app.MapPost("api/customers", async (CustomerDto customerDto, CreateCustomerRequestProcessor processor) =>
{
    var result = await processor.HandleAsync(new CreateCustomerRequest { Customer = customerDto });
    return Results.Created($"/api/customers/{result.CustomerId}", result);
});

// PUT update customer
app.MapPut("api/customers/{id}", async (int id, CustomerDto customerDto, UpdateCustomerRequestProcessor processor) =>
{
    var result = await processor.HandleAsync(new UpdateCustomerRequest { CustomerId = id, Customer = customerDto });
    return result.WasUpdated ? Results.NoContent() : Results.NotFound();
});

// DELETE customer
app.MapDelete("api/customers/{id}", async (int id, DeleteCustomerRequestProcessor processor) =>
{
    var result = await processor.HandleAsync(new DeleteCustomerRequest { CustomerId = id });
    return result.WasDeleted ? Results.NoContent() : Results.NotFound();
});
```

### Orders

```csharp
// GET all orders
app.MapGet("api/orders", async (GetAllOrdersRequestProcessor processor) => 
    Results.Ok(await processor.HandleAsync(new GetAllOrdersRequest())));

// GET order by ID
app.MapGet("api/orders/{id}", async (int id, GetOrderRequestProcessor processor) =>
{
    var result = await processor.HandleAsync(new GetOrderRequest { OrderId = id });
    return result.Result != null ? Results.Ok(result.Result) : Results.NotFound();
});

// GET order items
app.MapGet("api/orders/{id}/items", async (int id, GetOrderItemsRequestProcessor processor) =>
{
    var result = await processor.HandleAsync(new GetOrderItemsRequest { OrderId = id });
    return result.Result != null ? Results.Ok(result.Result) : Results.NotFound();
});

// POST new order
app.MapPost("api/orders", async (OrderDto orderDto, CreateOrderRequestProcessor processor) =>
{
    var result = await processor.HandleAsync(new CreateOrderRequest { Order = orderDto });
    return Results.Created($"/api/orders/{result.OrderId}", result);
});

// PUT update order status
app.MapPut("api/orders/{id}/status", async (int id, string status, UpdateOrderStatusRequestProcessor processor) =>
{
    var result = await processor.HandleAsync(new UpdateOrderStatusRequest { OrderId = id, Status = status });
    return result.WasUpdated ? Results.NoContent() : Results.NotFound();
});

// DELETE order (cancel)
app.MapDelete("api/orders/{id}", async (int id, CancelOrderRequestProcessor processor) =>
{
    var result = await processor.HandleAsync(new CancelOrderRequest { OrderId = id });
    return result.WasCancelled ? Results.NoContent() : Results.NotFound();
});
```

### Reviews

```csharp
// GET all reviews
app.MapGet("api/reviews", async (GetAllReviewsRequestProcessor processor) => 
    Results.Ok(await processor.HandleAsync(new GetAllReviewsRequest())));

// GET review by ID
app.MapGet("api/reviews/{id}", async (int id, GetReviewRequestProcessor processor) =>
{
    var result = await processor.HandleAsync(new GetReviewRequest { ReviewId = id });
    return result.Result != null ? Results.Ok(result.Result) : Results.NotFound();
});

// GET reviews by book
app.MapGet("api/books/{id}/reviews", async (int id, GetBookReviewsRequestProcessor processor) =>
{
    var result = await processor.HandleAsync(new GetBookReviewsRequest { BookId = id });
    return result.Result != null ? Results.Ok(result.Result) : Results.NotFound();
});

// POST new review
app.MapPost("api/reviews", async (ReviewDto reviewDto, CreateReviewRequestProcessor processor) =>
{
    var result = await processor.HandleAsync(new CreateReviewRequest { Review = reviewDto });
    return Results.Created($"/api/reviews/{result.ReviewId}", result);
});

// PUT update review
app.MapPut("api/reviews/{id}", async (int id, ReviewDto reviewDto, UpdateReviewRequestProcessor processor) =>
{
    var result = await processor.HandleAsync(new UpdateReviewRequest { ReviewId = id, Review = reviewDto });
    return result.WasUpdated ? Results.NoContent() : Results.NotFound();
});

// DELETE review
app.MapDelete("api/reviews/{id}", async (int id, DeleteReviewRequestProcessor processor) =>
{
    var result = await processor.HandleAsync(new DeleteReviewRequest { ReviewId = id });
    return result.WasDeleted ? Results.NoContent() : Results.NotFound();
});
```

### Cart Management

```csharp
// GET customer cart
app.MapGet("api/customers/{id}/cart", async (int id, GetCustomerCartRequestProcessor processor) =>
{
    var result = await processor.HandleAsync(new GetCustomerCartRequest { CustomerId = id });
    return result.Result != null ? Results.Ok(result.Result) : Results.NotFound();
});

// POST add item to cart
app.MapPost("api/carts/{id}/items", async (int id, CartItemDto cartItemDto, AddCartItemRequestProcessor processor) =>
{
    var result = await processor.HandleAsync(new AddCartItemRequest { CartId = id, CartItem = cartItemDto });
    return Results.Created($"/api/carts/{id}/items/{result.CartItemId}", result);
});

// PUT update cart item quantity
app.MapPut("api/carts/{cartId}/items/{itemId}", async (int cartId, int itemId, int quantity, UpdateCartItemQuantityRequestProcessor processor) =>
{
    var result = await processor.HandleAsync(new UpdateCartItemQuantityRequest { CartId = cartId, CartItemId = itemId, Quantity = quantity });
    return result.WasUpdated ? Results.NoContent() : Results.NotFound();
});

// DELETE cart item
app.MapDelete("api/carts/{cartId}/items/{itemId}", async (int cartId, int itemId, RemoveCartItemRequestProcessor processor) =>
{
    var result = await processor.HandleAsync(new RemoveCartItemRequest { CartId = cartId, CartItemId = itemId });
    return result.WasRemoved ? Results.NoContent() : Results.NotFound();
});

// POST checkout cart
app.MapPost("api/carts/{id}/checkout", async (int id, CheckoutCartRequestProcessor processor) =>
{
    var result = await processor.HandleAsync(new CheckoutCartRequest { CartId = id });
    return Results.Created($"/api/orders/{result.OrderId}", result);
});

// DELETE clear cart
app.MapDelete("api/carts/{id}", async (int id, ClearCartRequestProcessor processor) =>
{
    var result = await processor.HandleAsync(new ClearCartRequest { CartId = id });
    return result.WasCleared ? Results.NoContent() : Results.NotFound();
});
```

## 5. Project Structure

### API Layer (`Bookstore.Api`)

This project contains:
- Program.cs with API configuration and middleware setup
- Extension methods for endpoint definitions (e.g., BookEndpoints.cs)
- Swagger/OpenAPI configuration
- Authentication and authorization middleware

### Data Layer (`Bookstore.Data`)

This project contains:
- BookstoreDbContext with Entity Framework configuration
- Entity models (Book.cs, Author.cs, Category.cs, etc.)
- Data access logic and repositories if needed
- Database migrations and seed data

### Request Processing Layer (`Bookstore.RequestProcessing`)

This project contains:
- Feature folders organized by entity and action (GetBook, CreateBook, etc.)
- RequestProcessor classes that implement business logic
- Request/Response models for each feature
- Validation logic

### DTOs Layer (`Bookstore.Dtos`)

This project contains:
- Data Transfer Objects for API communication
- Simple POCO classes with no business logic

## 6. Examples

### Good Example (Do)

Organizing endpoints into extension methods:

```csharp
// Program.cs
var app = builder.Build();
// Configure app...

app.MapBookEndpoints();
app.MapAuthorEndpoints();
app.MapCategoryEndpoints();
// Other endpoint mappings...

app.Run();

// BookEndpoints.cs
public static class BookEndpointsExtensions
{
    public static WebApplication MapBookEndpoints(this WebApplication app)
    {
        app.MapGet("api/books", async (GetAllBooksRequestProcessor processor) =>
            Results.Ok(await processor.HandleAsync(new GetAllBooksRequest())))
            .WithName("GetAllBooks")
            .WithOpenApi();

        // Other book endpoints...
        
        return app;
    }
}
```

### Bad Example (Don't / Avoid)

Having all endpoints in Program.cs making it difficult to maintain:

```csharp
// Program.cs
var app = builder.Build();
// Configure app...

// Books
app.MapGet("api/books", async (GetAllBooksRequestProcessor processor) => 
    Results.Ok(await processor.HandleAsync(new GetAllBooksRequest())));
app.MapGet("api/books/{id}", async (int id, GetBookRequestProcessor processor) => 
    // implementation...);
// many more endpoints...

// Authors
app.MapGet("api/authors", async (GetAllAuthorsRequestProcessor processor) => 
    Results.Ok(await processor.HandleAsync(new GetAllAuthorsRequest())));
// many more endpoints...

// Categories, Orders, and other entities...
// dozens more endpoints making the file very long and hard to maintain...

app.Run();
```

## 7. Related Specifications / Further Reading

- ASP.NET Core Minimal APIs Documentation: https://docs.microsoft.com/en-us/aspnet/core/fundamentals/minimal-apis
- Clean Architecture Principles: https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html
- RESTful API Design Best Practices: https://restfulapi.net/

## 8. Keywords

ASP.NET Core, Minimal API, Clean Architecture, REST, API Design, Bookstore API