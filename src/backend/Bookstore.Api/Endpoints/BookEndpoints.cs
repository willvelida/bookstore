using Bookstore.RequestProcessing.Features.GetBook;
using Bookstore.Dtos;
using Microsoft.OpenApi.Models;
using Microsoft.OpenApi.Any;

namespace Bookstore.Api.Endpoints;

public static class BookEndpoints
{
    public static WebApplication MapBookEndpoints(this WebApplication app)
    {
        // GET book by ID
        app.MapGet("/api/books/{bookId}", async (int bookId, GetBookRequestProcessor processor) =>
        {
            var result = await processor.HandleAsync(new GetBookRequest { BookId = bookId });
            return result.Result != null ? Results.Ok(result.Result) : Results.NotFound();
        })
        .WithName("GetBook")
        .WithOpenApi(operation => 
        {
            operation.Summary = "Gets a book by ID";
            operation.Description = "Retrieves a specific book by its unique identifier";
            operation.Tags = new List<OpenApiTag> { new() { Name = "Books" } };
            
            // Add parameter descriptions
            if (operation.Parameters?.Count > 0)
            {
                operation.Parameters[0].Description = "The unique identifier of the book";
                operation.Parameters[0].Example = new OpenApiInteger(1);
            }
            
            return operation;
        })
        .Produces<BookDto>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        return app;
    }
}