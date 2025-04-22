using Bookstore.Dtos;
using Microsoft.OpenApi.Models;
using Microsoft.OpenApi.Any;

namespace Bookstore.Api.Endpoints;

public static class ReviewEndpoints
{
    public static WebApplication MapReviewEndpoints(this WebApplication app)
    {
        // GET reviews for a book
        app.MapGet("/api/books/{bookId}/reviews", (int bookId) =>
        {
            // Hardcoded sample reviews for now
            var reviews = new[]
            {
                new 
                {
                    ReviewId = 1,
                    BookId = bookId,
                    BookTitle = $"Book {bookId}",
                    CustomerId = 1,
                    CustomerName = "John Doe",
                    Rating = 5,
                    Comment = "Excellent book, highly recommended!",
                    ReviewDate = DateTime.Now.AddDays(-5)
                },
                new
                {
                    ReviewId = 2,
                    BookId = bookId,
                    BookTitle = $"Book {bookId}",
                    CustomerId = 2,
                    CustomerName = "Jane Smith",
                    Rating = 4,
                    Comment = "Very good read, enjoyed it.",
                    ReviewDate = DateTime.Now.AddDays(-10)
                }
            };
            
            return Results.Ok(reviews);
        })
        .WithName("GetBookReviews")
        .WithOpenApi(operation => 
        {
            operation.Summary = "Gets reviews for a book";
            operation.Description = "Retrieves all customer reviews for a specific book";
            operation.Tags = new List<OpenApiTag> { new() { Name = "Reviews" } };
            
            // Add parameter descriptions
            if (operation.Parameters?.Count > 0)
            {
                operation.Parameters[0].Description = "The unique identifier of the book to get reviews for";
                operation.Parameters[0].Example = new OpenApiInteger(1);
            }
            
            return operation;
        })
        .Produces<List<ReviewDto>>(StatusCodes.Status200OK);

        return app;
    }
}