using Bookstore.RequestProcessing.Features.GetOrder;
using Bookstore.RequestProcessing.Features.GetAllOrders;
using Bookstore.Dtos;
using Microsoft.OpenApi.Models;
using Microsoft.OpenApi.Any;

namespace Bookstore.Api.Endpoints;

public static class OrderEndpoints
{
    public static WebApplication MapOrderEndpoints(this WebApplication app)
    {
        // GET all orders
        app.MapGet("/api/orders", async (GetAllOrdersRequestProcessor processor) =>
        {
            var result = await processor.HandleAsync(new GetAllOrdersRequest());
            return Results.Ok(result.Orders);
        })
        .WithName("GetAllOrders")
        .WithOpenApi(operation => 
        {
            operation.Summary = "Gets all orders";
            operation.Description = "Retrieves a list of all orders in the bookstore";
            operation.Tags = new List<OpenApiTag> { new() { Name = "Orders" } };
            return operation;
        })
        .Produces<List<OrderDto>>(StatusCodes.Status200OK);

        // GET order by ID
        app.MapGet("/api/orders/{orderId}", async (int orderId, GetOrderRequestProcessor processor) =>
        {
            var result = await processor.HandleAsync(new GetOrderRequest { OrderId = orderId });
            return result.Result != null ? Results.Ok(result.Result) : Results.NotFound();
        })
        .WithName("GetOrder")
        .WithOpenApi(operation => 
        {
            operation.Summary = "Gets an order by ID";
            operation.Description = "Retrieves a specific order by its unique identifier";
            operation.Tags = new List<OpenApiTag> { new() { Name = "Orders" } };
            
            // Add parameter descriptions
            if (operation.Parameters?.Count > 0)
            {
                operation.Parameters[0].Description = "The unique identifier of the order";
                operation.Parameters[0].Example = new OpenApiInteger(1);
            }
            
            return operation;
        })
        .Produces<OrderDto>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        return app;
    }
}