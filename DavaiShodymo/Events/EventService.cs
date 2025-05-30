using DavaiShodymo.Categories;
using DavaiShodymo.EventImagePlacements;
using DavaiShodymo.EventImages;
using DavaiShodymo.EventReviews;
using DavaiShodymo.Events.GetEventById;
using DavaiShodymo.Tags;
using DavaiShodymo.Users;

namespace DavaiShodymo.Events;

public class EventService(IEventRepository eventRepository, IUserRepository userRepository,
    IEventImagePlacementRepository eventImagePlacementRepository, IEventImageRepository eventImageRepository,
    IEventReviewRepository eventReviewRepository) : IEventService
{
    public async Task<GetEventByIdResponse> GetEventByIdAsync(int eventId, CancellationToken cancellationToken)
    {
        var eventEntity = await eventRepository.GetByIdAsync(eventId, cancellationToken)
            .ConfigureAwait(false);

        if (eventEntity == null)
        {
            throw new ArgumentNullException($"Event with ID {eventEntity} not found");
        }

        var user = await userRepository.GetByIdAsync(eventEntity.UserId, cancellationToken)
            .ConfigureAwait(false);

        var userFullName = user != null
            ? $"{user.FirstName} {user.LastName}"
            : "Unknown User";

        int? userId = user?.Id ?? null;

        var eventImagePlacements = await eventImagePlacementRepository.GetAllAsync(cancellationToken)
            .ConfigureAwait(false);

        var eventImages = await eventImageRepository.GetByEventIdAsync(eventEntity.Id, cancellationToken)
            .ConfigureAwait(false);

        List<EventImageCommand> eventImageCommands = new List<EventImageCommand>();

        foreach (var eventImagePlacement in eventImagePlacements)
        {
            EventImage eventImage = eventImages
                .FirstOrDefault(ei => ei.Id == eventImagePlacement.Id);

            if (eventImage != null)
            {
                var eventImageCommand = new EventImageCommand(eventImage.PhotoLink, eventImagePlacement.Placement);

                eventImageCommands.Add(eventImageCommand);
            }
        }

        List<Tag> tags = new List<Tag>();
        List<Category> categories = new List<Category>();

        var totalRating = await eventReviewRepository.GetAverageRatingAsync(eventEntity.Id, cancellationToken);

        var result = new GetEventByIdResponse(eventEntity.Id, eventEntity.DateStart, eventEntity.DateEnd,
            eventEntity.DateStamp, eventEntity.Name, eventEntity.Description, eventEntity.Location,
            eventImageCommands, totalRating, userId, userFullName, tags, categories);

        return result;
    }
}