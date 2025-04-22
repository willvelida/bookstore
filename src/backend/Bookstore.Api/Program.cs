using Bookstore.Api.Endpoints;
using Bookstore.Api.OpenApi;
using Bookstore.Data;
using Bookstore.RequestProcessing.Features.GetBook;
using Bookstore.RequestProcessing.Features.GetAuthor;
using Bookstore.RequestProcessing.Features.GetCategory;
using Bookstore.RequestProcessing.Features.GetAllCategories;
using Bookstore.RequestProcessing.Features.GetPublisher;
using Bookstore.RequestProcessing.Features.GetAllPublishers;
using Bookstore.RequestProcessing.Features.GetCustomer;
using Bookstore.RequestProcessing.Features.GetAllCustomers;
using Bookstore.RequestProcessing.Features.GetOrder;
using Bookstore.RequestProcessing.Features.GetAllOrders;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Configure OpenAPI/Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Bookstore API",
        Version = "v1",
        Description = "An API for managing a bookstore with books, authors, categories, publishers, customers, orders, reviews, and carts",
        Contact = new OpenApiContact
        {
            Name = "Will Velida",
            Email = "will.velida@example.com"
        },
        License = new OpenApiLicense
        {
            Name = "MIT License",
            Url = new Uri("https://opensource.org/licenses/MIT")
        }
    });
    
    // Add custom tag descriptions document filter
    options.DocumentFilter<TagDescriptionsDocumentFilter>();
});

// Register repository
builder.Services.AddTransient<BookstoreRepository>();

// Register request processors
// Book processors
builder.Services.AddTransient<GetBookRequestProcessor>();

// Author processors
builder.Services.AddTransient<GetAuthorRequestProcessor>();

// Category processors
builder.Services.AddTransient<GetCategoryRequestProcessor>();
builder.Services.AddTransient<GetAllCategoriesRequestProcessor>();

// Publisher processors
builder.Services.AddTransient<GetPublisherRequestProcessor>();
builder.Services.AddTransient<GetAllPublishersRequestProcessor>();

// Customer processors
builder.Services.AddTransient<GetCustomerRequestProcessor>();
builder.Services.AddTransient<GetAllCustomersRequestProcessor>();

// Order processors
builder.Services.AddTransient<GetOrderRequestProcessor>();
builder.Services.AddTransient<GetAllOrdersRequestProcessor>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Bookstore API v1");
        options.RoutePrefix = string.Empty; // Set Swagger UI as the root page
        options.DocumentTitle = "Bookstore API Documentation";
    });
}

// Map all endpoints using extension methods
app.MapBookEndpoints();
app.MapAuthorEndpoints();
app.MapCategoryEndpoints();
app.MapPublisherEndpoints();
app.MapCustomerEndpoints();
app.MapOrderEndpoints();
app.MapReviewEndpoints();
app.MapCartEndpoints();

app.Run();
