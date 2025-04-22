using Bookstore.Data;
using Bookstore.Dtos;

namespace Bookstore.RequestProcessing.Features.GetAllPublishers;

public class GetAllPublishersRequest
{
    // No parameters needed for getting all publishers
}

public class GetAllPublishersResponse
{
    public List<PublisherDto> Publishers { get; set; } = new List<PublisherDto>();
}

public class GetAllPublishersRequestProcessor
{
    private readonly BookstoreRepository _repository;

    public GetAllPublishersRequestProcessor(BookstoreRepository repository)
    {
        _repository = repository;
    }

    public async Task<GetAllPublishersResponse> HandleAsync(GetAllPublishersRequest request, CancellationToken cancellationToken = default)
    {
        // For now, return hardcoded data
        var publishers = new List<PublisherDto>
        {
            new PublisherDto { PublisherId = 1, Name = "Penguin Books", ContactEmail = "contact@penguin.com", Address = "123 Publishing Ave, New York" },
            new PublisherDto { PublisherId = 2, Name = "HarperCollins", ContactEmail = "info@harpercollins.com", Address = "456 Book Street, London" },
            new PublisherDto { PublisherId = 3, Name = "Random House", ContactEmail = "support@randomhouse.com", Address = "789 Novel Road, Toronto" },
            new PublisherDto { PublisherId = 4, Name = "Macmillan Publishers", ContactEmail = "inquiry@macmillan.com", Address = "101 Literature Blvd, Sydney" }
        };

        return new GetAllPublishersResponse { Publishers = publishers };
    }
}