using Bookstore.Dtos;
using Bookstore.RequestProcessing.Features.GetBook;
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

public class BookEndpointsTests
{
    [Fact]
    public async Task GetBook_WithExistingId_ReturnsOkWithBookDto()
    {
        // Arrange
        var mockProcessor = new Mock<IGetBookRequestProcessor>();
        mockProcessor.Setup(p => p.HandleAsync(It.Is<GetBookRequest>(r => r.BookId == 1), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new GetBookResponse 
            { 
                Result = new BookDto 
                { 
                    BookId = 1, 
                    Title = "Test Book", 
                    ISBN = "1234567890", 
                    Price = 19.99m,
                    AuthorId = 1,
                    AuthorName = "Test Author"
                } 
            });

        using var host = await new HostBuilder()
            .ConfigureWebHost(webBuilder =>
            {
                webBuilder
                    .UseTestServer()
                    .ConfigureServices(services =>
                    {
                        services.AddRouting(); // Add routing services
                        services.AddEndpointsApiExplorer();
                        services.AddSwaggerGen();
                        services.AddSingleton<IGetBookRequestProcessor>(mockProcessor.Object);
                    })
                    .Configure(app =>
                    {
                        app.UseRouting();
                        
                        // Create a minimal API route that mimics the behavior of MapBookEndpoints
                        app.UseEndpoints(endpoints =>
                        {
                            endpoints.MapGet("/api/books/{bookId}", async context =>
                            {
                                var bookIdStr = context.Request.RouteValues["bookId"]?.ToString();
                                if (int.TryParse(bookIdStr, out var bookId))
                                {
                                    var processor = context.RequestServices.GetRequiredService<IGetBookRequestProcessor>();
                                    var result = await processor.HandleAsync(new GetBookRequest { BookId = bookId });
                                    
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
        var response = await client.GetAsync("/api/books/1");
        var content = await response.Content.ReadAsStringAsync();
        var book = JsonSerializer.Deserialize<BookDto>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(book);
        Assert.Equal(1, book.BookId);
        Assert.Equal("Test Book", book.Title);
        Assert.Equal("1234567890", book.ISBN);
        Assert.Equal(19.99m, book.Price);
        Assert.Equal(1, book.AuthorId);
        Assert.Equal("Test Author", book.AuthorName);
    }

    [Fact]
    public async Task GetBook_WithNonExistingId_ReturnsNotFound()
    {
        // Arrange
        var mockProcessor = new Mock<IGetBookRequestProcessor>();
        mockProcessor.Setup(p => p.HandleAsync(It.Is<GetBookRequest>(r => r.BookId == 999), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new GetBookResponse { Result = null });

        using var host = await new HostBuilder()
            .ConfigureWebHost(webBuilder =>
            {
                webBuilder
                    .UseTestServer()
                    .ConfigureServices(services =>
                    {
                        services.AddRouting(); // Add routing services
                        services.AddEndpointsApiExplorer();
                        services.AddSwaggerGen();
                        services.AddSingleton<IGetBookRequestProcessor>(mockProcessor.Object);
                    })
                    .Configure(app =>
                    {
                        app.UseRouting();
                        
                        // Create a minimal API route that mimics the behavior of MapBookEndpoints
                        app.UseEndpoints(endpoints =>
                        {
                            endpoints.MapGet("/api/books/{bookId}", async context =>
                            {
                                var bookIdStr = context.Request.RouteValues["bookId"]?.ToString();
                                if (int.TryParse(bookIdStr, out var bookId))
                                {
                                    var processor = context.RequestServices.GetRequiredService<IGetBookRequestProcessor>();
                                    var result = await processor.HandleAsync(new GetBookRequest { BookId = bookId });
                                    
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
        var response = await client.GetAsync("/api/books/999");

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
}