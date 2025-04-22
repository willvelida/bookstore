using Bookstore.Dtos;
using Bookstore.RequestProcessing.Features.GetCategory;
using Bookstore.RequestProcessing.Features.GetAllCategories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Moq;
using System.Net;
using System.Text.Json;
using Xunit;

namespace Bookstore.Api.Tests.Endpoints;

public class CategoryEndpointsTests
{
    [Fact]
    public async Task GetCategory_WithExistingId_ReturnsOkWithCategoryDto()
    {
        // Arrange
        var mockProcessor = new Mock<IGetCategoryRequestProcessor>();
        mockProcessor.Setup(p => p.HandleAsync(It.Is<GetCategoryRequest>(r => r.CategoryId == 1), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new GetCategoryResponse 
            { 
                Result = new CategoryDto 
                { 
                    CategoryId = 1, 
                    Name = "Fiction", 
                    Description = "Fictional literature and novels"
                } 
            });

        using var host = await new HostBuilder()
            .ConfigureWebHost(webBuilder =>
            {
                webBuilder
                    .UseTestServer()
                    .ConfigureServices(services =>
                    {
                        services.AddRouting(); 
                        services.AddEndpointsApiExplorer();
                        services.AddSwaggerGen();
                        services.AddSingleton<IGetCategoryRequestProcessor>(mockProcessor.Object);
                    })
                    .Configure(app =>
                    {
                        app.UseRouting();
                        
                        app.UseEndpoints(endpoints =>
                        {
                            endpoints.MapGet("/api/categories/{categoryId}", async context =>
                            {
                                var categoryIdStr = context.Request.RouteValues["categoryId"]?.ToString();
                                if (int.TryParse(categoryIdStr, out var categoryId))
                                {
                                    var processor = context.RequestServices.GetRequiredService<IGetCategoryRequestProcessor>();
                                    var result = await processor.HandleAsync(new GetCategoryRequest { CategoryId = categoryId });
                                    
                                    if (result.Result != null)
                                    {
                                        await context.Response.WriteAsJsonAsync(result.Result);
                                    }
                                    else
                                    {
                                        context.Response.StatusCode = StatusCodes.Status404NotFound;
                                    }
                                }
                                else
                                {
                                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                                }
                            });
                        });
                    });
            })
            .StartAsync();

        var client = host.GetTestClient();

        // Act
        var response = await client.GetAsync("/api/categories/1");
        var content = await response.Content.ReadAsStringAsync();
        var category = JsonSerializer.Deserialize<CategoryDto>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(category);
        Assert.Equal(1, category.CategoryId);
        Assert.Equal("Fiction", category.Name);
        Assert.Equal("Fictional literature and novels", category.Description);
    }

    [Fact]
    public async Task GetCategory_WithNonExistingId_ReturnsNotFound()
    {
        // Arrange
        var mockProcessor = new Mock<IGetCategoryRequestProcessor>();
        mockProcessor.Setup(p => p.HandleAsync(It.Is<GetCategoryRequest>(r => r.CategoryId == 999), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new GetCategoryResponse { Result = null });

        using var host = await new HostBuilder()
            .ConfigureWebHost(webBuilder =>
            {
                webBuilder
                    .UseTestServer()
                    .ConfigureServices(services =>
                    {
                        services.AddRouting();
                        services.AddEndpointsApiExplorer();
                        services.AddSwaggerGen();
                        services.AddSingleton<IGetCategoryRequestProcessor>(mockProcessor.Object);
                    })
                    .Configure(app =>
                    {
                        app.UseRouting();
                        
                        app.UseEndpoints(endpoints =>
                        {
                            endpoints.MapGet("/api/categories/{categoryId}", async context =>
                            {
                                var categoryIdStr = context.Request.RouteValues["categoryId"]?.ToString();
                                if (int.TryParse(categoryIdStr, out var categoryId))
                                {
                                    var processor = context.RequestServices.GetRequiredService<IGetCategoryRequestProcessor>();
                                    var result = await processor.HandleAsync(new GetCategoryRequest { CategoryId = categoryId });
                                    
                                    if (result.Result != null)
                                    {
                                        await context.Response.WriteAsJsonAsync(result.Result);
                                    }
                                    else
                                    {
                                        context.Response.StatusCode = StatusCodes.Status404NotFound;
                                    }
                                }
                                else
                                {
                                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                                }
                            });
                        });
                    });
            })
            .StartAsync();

        var client = host.GetTestClient();

        // Act
        var response = await client.GetAsync("/api/categories/999");

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task GetAllCategories_ReturnsOkWithCategoryList()
    {
        // Arrange
        var mockProcessor = new Mock<IGetAllCategoriesRequestProcessor>();
        mockProcessor.Setup(p => p.HandleAsync(It.IsAny<GetAllCategoriesRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new GetAllCategoriesResponse 
            { 
                Categories = new List<CategoryDto> 
                { 
                    new CategoryDto 
                    { 
                        CategoryId = 1, 
                        Name = "Fiction", 
                        Description = "Fictional literature and novels" 
                    },
                    new CategoryDto 
                    { 
                        CategoryId = 2, 
                        Name = "Non-Fiction", 
                        Description = "Factual books and literature" 
                    }
                }
            });

        using var host = await new HostBuilder()
            .ConfigureWebHost(webBuilder =>
            {
                webBuilder
                    .UseTestServer()
                    .ConfigureServices(services =>
                    {
                        services.AddRouting(); 
                        services.AddEndpointsApiExplorer();
                        services.AddSwaggerGen();
                        services.AddSingleton<IGetAllCategoriesRequestProcessor>(mockProcessor.Object);
                    })
                    .Configure(app =>
                    {
                        app.UseRouting();
                        
                        app.UseEndpoints(endpoints =>
                        {
                            endpoints.MapGet("/api/categories", async context =>
                            {
                                var processor = context.RequestServices.GetRequiredService<IGetAllCategoriesRequestProcessor>();
                                var result = await processor.HandleAsync(new GetAllCategoriesRequest());
                                await context.Response.WriteAsJsonAsync(result.Categories);
                            });
                        });
                    });
            })
            .StartAsync();

        var client = host.GetTestClient();

        // Act
        var response = await client.GetAsync("/api/categories");
        var content = await response.Content.ReadAsStringAsync();
        var categories = JsonSerializer.Deserialize<List<CategoryDto>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(categories);
        Assert.Equal(2, categories.Count);
        Assert.Equal(1, categories[0].CategoryId);
        Assert.Equal("Fiction", categories[0].Name);
        Assert.Equal(2, categories[1].CategoryId);
        Assert.Equal("Non-Fiction", categories[1].Name);
    }
}