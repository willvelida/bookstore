using Bookstore.Dtos;
using Bookstore.RequestProcessing.Features.GetAuthor;
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

public class AuthorEndpointsTests
{
    [Fact]
    public async Task GetAuthor_WithExistingId_ReturnsOkWithAuthorDto()
    {
        // Arrange
        var mockProcessor = new Mock<IGetAuthorRequestProcessor>();
        mockProcessor.Setup(p => p.HandleAsync(It.Is<GetAuthorRequest>(r => r.AuthorId == 1), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new GetAuthorResponse 
            { 
                Result = new AuthorDto 
                { 
                    AuthorId = 1, 
                    Name = "Test Author", 
                    Biography = "Test Author Biography",
                    Books = new List<BookDto>
                    {
                        new BookDto
                        {
                            BookId = 1,
                            Title = "Test Book 1",
                            ISBN = "1234567890",
                            Price = 19.99m,
                            AuthorId = 1,
                            AuthorName = "Test Author"
                        },
                        new BookDto
                        {
                            BookId = 2,
                            Title = "Test Book 2",
                            ISBN = "0987654321",
                            Price = 29.99m,
                            AuthorId = 1,
                            AuthorName = "Test Author"
                        }
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
                        services.AddSingleton<IGetAuthorRequestProcessor>(mockProcessor.Object);
                    })
                    .Configure(app =>
                    {
                        app.UseRouting();
                        
                        app.UseEndpoints(endpoints =>
                        {
                            endpoints.MapGet("/api/authors/{authorId}", async context =>
                            {
                                var authorIdStr = context.Request.RouteValues["authorId"]?.ToString();
                                if (int.TryParse(authorIdStr, out var authorId))
                                {
                                    var processor = context.RequestServices.GetRequiredService<IGetAuthorRequestProcessor>();
                                    var result = await processor.HandleAsync(new GetAuthorRequest { AuthorId = authorId });
                                    
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
        var response = await client.GetAsync("/api/authors/1");
        var content = await response.Content.ReadAsStringAsync();
        var author = JsonSerializer.Deserialize<AuthorDto>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(author);
        Assert.Equal(1, author.AuthorId);
        Assert.Equal("Test Author", author.Name);
        Assert.Equal("Test Author Biography", author.Biography);
        Assert.NotNull(author.Books);
        Assert.Equal(2, author.Books.Count);
        Assert.Equal("Test Book 1", author.Books[0].Title);
        Assert.Equal("Test Book 2", author.Books[1].Title);
    }

    [Fact]
    public async Task GetAuthor_WithNonExistingId_ReturnsNotFound()
    {
        // Arrange
        var mockProcessor = new Mock<IGetAuthorRequestProcessor>();
        mockProcessor.Setup(p => p.HandleAsync(It.Is<GetAuthorRequest>(r => r.AuthorId == 999), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new GetAuthorResponse { Result = null });

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
                        services.AddSingleton<IGetAuthorRequestProcessor>(mockProcessor.Object);
                    })
                    .Configure(app =>
                    {
                        app.UseRouting();
                        
                        app.UseEndpoints(endpoints =>
                        {
                            endpoints.MapGet("/api/authors/{authorId}", async context =>
                            {
                                var authorIdStr = context.Request.RouteValues["authorId"]?.ToString();
                                if (int.TryParse(authorIdStr, out var authorId))
                                {
                                    var processor = context.RequestServices.GetRequiredService<IGetAuthorRequestProcessor>();
                                    var result = await processor.HandleAsync(new GetAuthorRequest { AuthorId = authorId });
                                    
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
        var response = await client.GetAsync("/api/authors/999");

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
}