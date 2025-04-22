using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Bookstore.Api.OpenApi;

public class TagDescriptionsDocumentFilter : IDocumentFilter
{
    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        swaggerDoc.Tags = new List<OpenApiTag>
        {
            new OpenApiTag { Name = "Books", Description = "Operations for managing books in the bookstore" },
            new OpenApiTag { Name = "Authors", Description = "Operations for managing authors and their book publications" },
            new OpenApiTag { Name = "Categories", Description = "Operations for managing book categories" },
            new OpenApiTag { Name = "Publishers", Description = "Operations for managing book publishers" },
            new OpenApiTag { Name = "Customers", Description = "Operations for managing bookstore customers" },
            new OpenApiTag { Name = "Orders", Description = "Operations for managing customer orders" },
            new OpenApiTag { Name = "Reviews", Description = "Operations for managing book reviews" },
            new OpenApiTag { Name = "Cart", Description = "Operations for managing shopping carts" }
        };
    }
}