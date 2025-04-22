using Bookstore.RequestProcessing.Features.GetPublisher;
using Bookstore.RequestProcessing.Features.GetAllPublishers;
using Bookstore.Dtos;
using Microsoft.OpenApi.Models;
using Microsoft.OpenApi.Any;

namespace Bookstore.Api.Endpoints;

public static class PublisherEndpoints
{
    public static WebApplication MapPublisherEndpoints(this WebApplication app)
    {
        // GET all publishers
        app.MapGet("/api/publishers", async (GetAllPublishersRequestProcessor processor) =>
        {
            var result = await processor.HandleAsync(new GetAllPublishersRequest());
            return Results.Ok(result.Publishers);
        })
        .WithName("GetAllPublishers")
        .WithOpenApi(operation => 
        {
            operation.Summary = "Gets all publishers";
            operation.Description = "Retrieves a list of all book publishers in the bookstore";
            operation.Tags = new List<OpenApiTag> { new() { Name = "Publishers" } };
            return operation;
        })
        .Produces<List<PublisherDto>>(StatusCodes.Status200OK);

        // GET publisher by ID
        app.MapGet("/api/publishers/{publisherId}", async (int publisherId, GetPublisherRequestProcessor processor) =>
        {
            var result = await processor.HandleAsync(new GetPublisherRequest { PublisherId = publisherId });
            return result.Result != null ? Results.Ok(result.Result) : Results.NotFound();
        })
        .WithName("GetPublisher")
        .WithOpenApi(operation => 
        {
            operation.Summary = "Gets a publisher by ID";
            operation.Description = "Retrieves a specific publisher by its unique identifier";
            operation.Tags = new List<OpenApiTag> { new() { Name = "Publishers" } };
            
            // Add parameter descriptions
            if (operation.Parameters?.Count > 0)
            {
                operation.Parameters[0].Description = "The unique identifier of the publisher";
                operation.Parameters[0].Example = new OpenApiInteger(1);
            }
            
            return operation;
        })
        .Produces<PublisherDto>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        return app;
    }
}