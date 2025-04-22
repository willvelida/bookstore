using Bookstore.Dtos;
using Microsoft.OpenApi.Models;
using Microsoft.OpenApi.Any;

namespace Bookstore.Api.Endpoints;

public static class CartEndpoints
{
    public static WebApplication MapCartEndpoints(this WebApplication app)
    {
        // GET customer cart
        app.MapGet("/api/customers/{customerId}/cart", (int customerId) =>
        {
            // Hardcoded sample cart for now
            var cart = new
            {
                CartId = 1,
                CustomerId = customerId,
                Items = new[]
                {
                    new
                    {
                        CartItemId = 1,
                        BookId = 1,
                        BookTitle = "Clean Code",
                        UnitPrice = 39.99m,
                        Quantity = 1,
                        Subtotal = 39.99m
                    },
                    new
                    {
                        CartItemId = 2,
                        BookId = 3,
                        BookTitle = "Design Patterns",
                        UnitPrice = 49.99m,
                        Quantity = 2,
                        Subtotal = 99.98m
                    }
                },
                TotalAmount = 139.97m
            };
            
            return Results.Ok(cart);
        })
        .WithName("GetCustomerCart")
        .WithOpenApi(operation => 
        {
            operation.Summary = "Gets a customer's shopping cart";
            operation.Description = "Retrieves the current shopping cart for a specific customer";
            operation.Tags = new List<OpenApiTag> { new() { Name = "Cart" } };
            
            // Add parameter descriptions
            if (operation.Parameters?.Count > 0)
            {
                operation.Parameters[0].Description = "The unique identifier of the customer whose cart to retrieve";
                operation.Parameters[0].Example = new OpenApiInteger(1);
            }
            
            return operation;
        })
        .Produces<CartDto>(StatusCodes.Status200OK);

        return app;
    }
}