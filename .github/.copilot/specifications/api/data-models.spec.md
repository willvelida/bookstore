# Specification: Bookstore Data Models

**Version:** 1.0

**Last Updated:** 2025-04-22

**Owner:** Will Velida

## 1. Purpose & Scope

This specification defines the data models and entity relationships for the Bookstore application. These models form the foundation of the database schema and will be used across all layers of the application.

## 2. Core Principles & Guidelines

### Entity Design

* Follow Entity Framework Core best practices for entity design.
* Use clear and consistent naming for all entities and properties.
* Include appropriate data annotations for validations and constraints.
* Define navigation properties to express relationships between entities.
* Use shadow properties where appropriate (e.g., for audit fields).

### Database Configuration

* Configure relationships using Fluent API in the OnModelCreating method.
* Define indexes for frequently queried fields.
* Use appropriate data types for all properties.
* Configure cascade delete behavior explicitly.

### Data Access Patterns

* Use async/await for all database operations.
* Implement efficient query patterns using Include and projection.
* Consider using the repository pattern for complex data access scenarios.

## 3. Entity Models

### Book

```csharp
public class Book
{
    public int BookId { get; set; }
    
    [Required]
    [MaxLength(255)]
    public string Title { get; set; }
    
    [MaxLength(20)]
    public string ISBN { get; set; }
    
    public string Description { get; set; }
    
    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; set; }
    
    public DateTime PublicationDate { get; set; }
    
    public int StockQuantity { get; set; }
    
    public string CoverImageURL { get; set; }
    
    public int PublisherId { get; set; }
    public Publisher Publisher { get; set; }
    
    public ICollection<Author> Authors { get; set; }
    public ICollection<Category> Categories { get; set; }
    public ICollection<Order> Orders { get; set; }
    public ICollection<Review> Reviews { get; set; }
    public ICollection<CartItem> CartItems { get; set; }
}
```

### Author

```csharp
public class Author
{
    public int AuthorId { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string Name { get; set; }
    
    public string Bio { get; set; }
    
    public DateTime? DOB { get; set; }
    
    public ICollection<Book> Books { get; set; }
}
```

### Category

```csharp
public class Category
{
    public int CategoryId { get; set; }
    
    [Required]
    [MaxLength(50)]
    public string Name { get; set; }
    
    public string Description { get; set; }
    
    public ICollection<Book> Books { get; set; }
}
```

### Publisher

```csharp
public class Publisher
{
    public int PublisherId { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string Name { get; set; }
    
    [EmailAddress]
    public string ContactEmail { get; set; }
    
    public string Address { get; set; }
    
    public ICollection<Book> Books { get; set; }
}
```

### Customer

```csharp
public class Customer
{
    public int CustomerId { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string Name { get; set; }
    
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    
    [Required]
    public string PasswordHash { get; set; }
    
    public string Address { get; set; }
    
    [Phone]
    public string PhoneNumber { get; set; }
    
    public ICollection<Order> Orders { get; set; }
    public ICollection<Review> Reviews { get; set; }
    public Cart Cart { get; set; }
}
```

### Order

```csharp
public class Order
{
    public int OrderId { get; set; }
    
    public int CustomerId { get; set; }
    public Customer Customer { get; set; }
    
    public DateTime OrderDate { get; set; }
    
    [Column(TypeName = "decimal(18,2)")]
    public decimal TotalAmount { get; set; }
    
    public OrderStatus Status { get; set; }
    
    public ICollection<OrderItem> OrderItems { get; set; }
}

public enum OrderStatus
{
    Pending,
    Shipped,
    Completed,
    Cancelled
}
```

### OrderItem

```csharp
public class OrderItem
{
    public int OrderItemId { get; set; }
    
    public int OrderId { get; set; }
    public Order Order { get; set; }
    
    public int BookId { get; set; }
    public Book Book { get; set; }
    
    public int Quantity { get; set; }
    
    [Column(TypeName = "decimal(18,2)")]
    public decimal UnitPrice { get; set; }
}
```

### Review

```csharp
public class Review
{
    public int ReviewId { get; set; }
    
    public int CustomerId { get; set; }
    public Customer Customer { get; set; }
    
    public int BookId { get; set; }
    public Book Book { get; set; }
    
    [Range(1, 5)]
    public int Rating { get; set; }
    
    public string Comment { get; set; }
    
    public DateTime ReviewDate { get; set; }
}
```

### Cart

```csharp
public class Cart
{
    public int CartId { get; set; }
    
    public int CustomerId { get; set; }
    public Customer Customer { get; set; }
    
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    
    public ICollection<CartItem> CartItems { get; set; }
}
```

### CartItem

```csharp
public class CartItem
{
    public int CartItemId { get; set; }
    
    public int CartId { get; set; }
    public Cart Cart { get; set; }
    
    public int BookId { get; set; }
    public Book Book { get; set; }
    
    public int Quantity { get; set; }
}
```

## 4. Database Context

```csharp
public class BookstoreDbContext : DbContext
{
    public BookstoreDbContext(DbContextOptions<BookstoreDbContext> options)
        : base(options)
    {
    }
    
    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Publisher> Publishers { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<Cart> Carts { get; set; }
    public DbSet<CartItem> CartItems { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Book - Author (Many-to-Many)
        modelBuilder.Entity<Book>()
            .HasMany(b => b.Authors)
            .WithMany(a => a.Books)
            .UsingEntity(j => j.ToTable("BookAuthors"));
            
        // Book - Category (Many-to-Many)
        modelBuilder.Entity<Book>()
            .HasMany(b => b.Categories)
            .WithMany(c => c.Books)
            .UsingEntity(j => j.ToTable("BookCategories"));
            
        // Book - Publisher (Many-to-One)
        modelBuilder.Entity<Book>()
            .HasOne(b => b.Publisher)
            .WithMany(p => p.Books)
            .HasForeignKey(b => b.PublisherId);
            
        // Customer - Order (One-to-Many)
        modelBuilder.Entity<Order>()
            .HasOne(o => o.Customer)
            .WithMany(c => c.Orders)
            .HasForeignKey(o => o.CustomerId);
            
        // Order - OrderItem (One-to-Many)
        modelBuilder.Entity<OrderItem>()
            .HasOne(oi => oi.Order)
            .WithMany(o => o.OrderItems)
            .HasForeignKey(oi => oi.OrderId);
            
        // OrderItem - Book (Many-to-One)
        modelBuilder.Entity<OrderItem>()
            .HasOne(oi => oi.Book)
            .WithMany(b => b.Orders)
            .HasForeignKey(oi => oi.BookId);
            
        // Customer - Review (One-to-Many)
        modelBuilder.Entity<Review>()
            .HasOne(r => r.Customer)
            .WithMany(c => c.Reviews)
            .HasForeignKey(r => r.CustomerId);
            
        // Book - Review (One-to-Many)
        modelBuilder.Entity<Review>()
            .HasOne(r => r.Book)
            .WithMany(b => b.Reviews)
            .HasForeignKey(r => r.BookId);
            
        // Customer - Cart (One-to-One)
        modelBuilder.Entity<Cart>()
            .HasOne(c => c.Customer)
            .WithOne(c => c.Cart)
            .HasForeignKey<Cart>(c => c.CustomerId);
            
        // Cart - CartItem (One-to-Many)
        modelBuilder.Entity<CartItem>()
            .HasOne(ci => ci.Cart)
            .WithMany(c => c.CartItems)
            .HasForeignKey(ci => ci.CartId);
            
        // CartItem - Book (Many-to-One)
        modelBuilder.Entity<CartItem>()
            .HasOne(ci => ci.Book)
            .WithMany(b => b.CartItems)
            .HasForeignKey(ci => ci.BookId);
    }
}
```

## 5. DTO Models

### BookDto

```csharp
public class BookDto
{
    public int BookId { get; set; }
    public string Title { get; set; }
    public string ISBN { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public DateTime PublicationDate { get; set; }
    public int StockQuantity { get; set; }
    public string CoverImageURL { get; set; }
    public int PublisherId { get; set; }
    public string PublisherName { get; set; }
    public List<AuthorDto> Authors { get; set; }
    public List<CategoryDto> Categories { get; set; }
}
```

### AuthorDto

```csharp
public class AuthorDto
{
    public int AuthorId { get; set; }
    public string Name { get; set; }
    public string Bio { get; set; }
    public DateTime? DOB { get; set; }
}
```

### CategoryDto

```csharp
public class CategoryDto
{
    public int CategoryId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}
```

### PublisherDto

```csharp
public class PublisherDto
{
    public int PublisherId { get; set; }
    public string Name { get; set; }
    public string ContactEmail { get; set; }
    public string Address { get; set; }
}
```

### CustomerDto

```csharp
public class CustomerDto
{
    public int CustomerId { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Address { get; set; }
    public string PhoneNumber { get; set; }
}
```

### OrderDto

```csharp
public class OrderDto
{
    public int OrderId { get; set; }
    public int CustomerId { get; set; }
    public string CustomerName { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get; set; }
    public string Status { get; set; }
    public List<OrderItemDto> OrderItems { get; set; }
}
```

### OrderItemDto

```csharp
public class OrderItemDto
{
    public int OrderItemId { get; set; }
    public int BookId { get; set; }
    public string BookTitle { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Subtotal => Quantity * UnitPrice;
}
```

### ReviewDto

```csharp
public class ReviewDto
{
    public int ReviewId { get; set; }
    public int CustomerId { get; set; }
    public string CustomerName { get; set; }
    public int BookId { get; set; }
    public string BookTitle { get; set; }
    public int Rating { get; set; }
    public string Comment { get; set; }
    public DateTime ReviewDate { get; set; }
}
```

### CartDto

```csharp
public class CartDto
{
    public int CartId { get; set; }
    public int CustomerId { get; set; }
    public List<CartItemDto> Items { get; set; }
    public decimal TotalAmount => Items?.Sum(i => i.Subtotal) ?? 0;
}
```

### CartItemDto

```csharp
public class CartItemDto
{
    public int CartItemId { get; set; }
    public int BookId { get; set; }
    public string BookTitle { get; set; }
    public decimal UnitPrice { get; set; }
    public int Quantity { get; set; }
    public decimal Subtotal => Quantity * UnitPrice;
}
```

## 6. Related Specifications / Further Reading

- Entity Framework Core Documentation: https://docs.microsoft.com/en-us/ef/core/
- Data Annotations Reference: https://docs.microsoft.com/en-us/dotnet/api/system.componentmodel.dataannotations
- EF Core Relationships: https://docs.microsoft.com/en-us/ef/core/modeling/relationships

## 7. Keywords

Entity Framework Core, Data Models, DTOs, Database Schema, Relationships