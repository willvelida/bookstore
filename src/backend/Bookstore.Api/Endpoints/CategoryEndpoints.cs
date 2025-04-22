using Bookstore.RequestProcessing.Features.GetCategory;
using Bookstore.RequestProcessing.Features.GetAllCategories;
using Microsoft.AspNetCore.Http.HttpResults;
using Bookstore.Dtos;
using Microsoft.OpenApi.Models;
using Microsoft.OpenApi.Any;

namespace Bookstore.Api.Endpoints;

public static class CategoryEndpoints
{
    public static WebApplication MapCategoryEndpoints(this WebApplication app)
    {
        // GET all categories
        app.MapGet("/api/categories", async (GetAllCategoriesRequestProcessor processor) =>
        {
            var result = await processor.HandleAsync(new GetAllCategoriesRequest());
            return Results.Ok(result.Categories);
        })
        .WithName("GetAllCategories")
        .WithOpenApi(operation => 
        {
            operation.Summary = "Gets all categories";
            operation.Description = "Retrieves a list of all book categories available in the bookstore";
            operation.Tags = new List<OpenApiTag> { new() { Name = "Categories" } };
            return operation;
        })
        .Produces<List<CategoryDto>>(StatusCodes.Status200OK);

        // GET category by ID
        app.MapGet("/api/categories/{categoryId}", async (int categoryId, GetCategoryRequestProcessor processor) =>
        {
            var result = await processor.HandleAsync(new GetCategoryRequest { CategoryId = categoryId });
            return result.Result != null ? Results.Ok(result.Result) : Results.NotFound();
        })
        .WithName("GetCategory")
        .WithOpenApi(operation => 
        {
            operation.Summary = "Gets a category by ID";
            operation.Description = "Retrieves a specific book category by its unique identifier";
            operation.Tags = new List<OpenApiTag> { new() { Name = "Categories" } };
            
            // Add parameter descriptions
            if (operation.Parameters?.Count > 0)
            {
                operation.Parameters[0].Description = "The unique identifier of the category";
                operation.Parameters[0].Example = new OpenApiInteger(1);
            }
            
            return operation;
        })
        .Produces<CategoryDto>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        return app;
    }
}