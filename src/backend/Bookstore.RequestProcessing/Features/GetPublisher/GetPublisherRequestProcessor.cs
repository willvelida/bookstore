using Bookstore.Data;
using Bookstore.Dtos;

namespace Bookstore.RequestProcessing.Features.GetPublisher;

public class GetPublisherRequest
{
    public int PublisherId { get; set; }
}

public class GetPublisherResponse
{
    public PublisherDto? Result { get; set; }
}

public class GetPublisherRequestProcessor
{
    private readonly BookstoreRepository _repository;

    public GetPublisherRequestProcessor(BookstoreRepository repository)
    {
        _repository = repository;
    }

    public async Task<GetPublisherResponse> HandleAsync(GetPublisherRequest request, CancellationToken cancellationToken = default)
    {
        // For now, return a hardcoded response
        var publisherDto = new PublisherDto
        {
            PublisherId = request.PublisherId,
            Name = $"Publisher {request.PublisherId}",
            ContactEmail = $"contact@publisher{request.PublisherId}.com",
            Address = $"123 Publisher Street, City {request.PublisherId}"
        };

        return new GetPublisherResponse { Result = publisherDto };
    }
}