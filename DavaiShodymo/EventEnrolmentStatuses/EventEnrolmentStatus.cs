using DavaiShodymo.EventEnrolments;

namespace DavaiShodymo.EventEnrolmentStatuses;

public class EventEnrolmentStatus(string status)
{
    public int Id { get; set; }
    public string Status { get; set; } = status;
    public ICollection<EventEnrolment> EventEnrolments { get; set; } = new List<EventEnrolment>();
}