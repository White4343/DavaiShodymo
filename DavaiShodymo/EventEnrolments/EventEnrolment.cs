using DavaiShodymo.EventEnrolmentStatuses;
using DavaiShodymo.Events;
using DavaiShodymo.Users;

namespace DavaiShodymo.EventEnrolments
{
    public class EventEnrolment(bool isFavorite, int eventId, int eventEnrolmentStatusId, int userId)
    {
        public int Id { get; set; }
        public DateTime DateStamp { get; set; } = DateTime.UtcNow;
        public bool IsFavorite { get; set; } = isFavorite;
        public int EventId { get; set; } = eventId;
        public Event Event { get; set; } = null!;
        public int EventEnrolmentStatusId { get; set; } = eventEnrolmentStatusId;
        public EventEnrolmentStatus EventEnrolmentStatus { get; set; } = null!;
        public int UserId { get; set; } = userId;
        public User User { get; set; } = null!;

        public void Update(bool? isFavorite, int? eventEnrolmentStatusId)
        {
            if (isFavorite.HasValue)
            {
                IsFavorite = isFavorite.Value;
            }
            if (eventEnrolmentStatusId.HasValue)
            {
                EventEnrolmentStatusId = eventEnrolmentStatusId.Value;
            }

            DateStamp = DateTime.UtcNow;
        }
    }
}