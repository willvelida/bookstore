using Bookstore.RequestProcessing.Features.GetCustomer;
using Bookstore.RequestProcessing.Features.GetAllCustomers;
using Bookstore.Dtos;
using Microsoft.OpenApi.Models;
using Microsoft.OpenApi.Any;

namespace Bookstore.Api.Endpoints;

public static class CustomerEndpoints
{
    public static WebApplication MapCustomerEndpoints(this WebApplication app)
    {
        // GET all customers
        app.MapGet("/api/customers", async (GetAllCustomersRequestProcessor processor) =>
        {
            var result = await processor.HandleAsync(new GetAllCustomersRequest());
            return Results.Ok(result.Customers);
        })
        .WithName("GetAllCustomers")
        .WithOpenApi(operation => 
        {
            operation.Summary = "Gets all customers";
            operation.Description = "Retrieves a list of all registered customers in the bookstore";
            operation.Tags = new List<OpenApiTag> { new() { Name = "Customers" } };
            return operation;
        })
        .Produces<List<CustomerDto>>(StatusCodes.Status200OK);

        // GET customer by ID
        app.MapGet("/api/customers/{customerId}", async (int customerId, GetCustomerRequestProcessor processor) =>
        {
            var result = await processor.HandleAsync(new GetCustomerRequest { CustomerId = customerId });
            return result.Result != null ? Results.Ok(result.Result) : Results.NotFound();
        })
        .WithName("GetCustomer")
        .WithOpenApi(operation => 
        {
            operation.Summary = "Gets a customer by ID";
            operation.Description = "Retrieves a specific customer by their unique identifier";
            operation.Tags = new List<OpenApiTag> { new() { Name = "Customers" } };
            
            // Add parameter descriptions
            if (operation.Parameters?.Count > 0)
            {
                operation.Parameters[0].Description = "The unique identifier of the customer";
                operation.Parameters[0].Example = new OpenApiInteger(1);
            }
            
            return operation;
        })
        .Produces<CustomerDto>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        return app;
    }
}