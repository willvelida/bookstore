using Bookstore.RequestProcessing.Features.GetAuthor;
using Bookstore.Dtos;
using Microsoft.OpenApi.Models;
using Microsoft.OpenApi.Any;

namespace Bookstore.Api.Endpoints;

public static class AuthorEndpoints
{
    public static WebApplication MapAuthorEndpoints(this WebApplication app)
    {
        // GET author by ID
        app.MapGet("/api/authors/{authorId}", async (int authorId, GetAuthorRequestProcessor processor) =>
        {
            var result = await processor.HandleAsync(new GetAuthorRequest { AuthorId = authorId });
            return result.Result != null ? Results.Ok(result.Result) : Results.NotFound();
        })
        .WithName("GetAuthor")
        .WithOpenApi(operation => 
        {
            operation.Summary = "Gets an author by ID";
            operation.Description = "Retrieves a specific author by their unique identifier";
            operation.Tags = new List<OpenApiTag> { new() { Name = "Authors" } };
            
            // Add parameter descriptions
            if (operation.Parameters?.Count > 0)
            {
                operation.Parameters[0].Description = "The unique identifier of the author";
                operation.Parameters[0].Example = new OpenApiInteger(1);
            }
            
            return operation;
        })
        .Produces<AuthorDto>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        return app;
    }
}