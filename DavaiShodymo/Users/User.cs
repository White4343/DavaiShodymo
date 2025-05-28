using DavaiShodymo.Bans;
using DavaiShodymo.EventEnrolments;
using DavaiShodymo.EventReviews;
using DavaiShodymo.Events;
using DavaiShodymo.Logs;
using DavaiShodymo.Roles;
using DavaiShodymo.Tickets;
using DavaiShodymo.UserSubscriptions;

namespace DavaiShodymo.Users;

public class User(string userName, string email, string passwordHash, string? firstName, string? lastName, string? photoLink,
    DateTime? birthDate, int roleId)
{
    public int Id { get; set; }
    public string UserName { get; set; } = userName;
    public string NormalizedUserName { get; set; } = userName.ToUpperInvariant();
    public string Email { get; set; } = email;
    public string NormalizedEmail { get; set; } = email.ToUpperInvariant();
    public string PasswordHash { get; set; } = passwordHash;
    public string? FirstName { get; set; } = firstName;
    public string? LastName { get; set; } = lastName;
    public string? PhotoLink { get; set; } = photoLink;
    public DateTime? BirthDate { get; set; } = birthDate;
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedDate { get; set; } = DateTime.UtcNow;
    public int RoleId { get; set; } = roleId;
    public Role Role { get; set; } = null!;
    public ICollection<Ban> Bans { get; set; } = new List<Ban>();
    public ICollection<Ban> BannedBy { get; set; } = new List<Ban>();
    public ICollection<UserSubscription> UserSubscriptions { get; set; } = new List<UserSubscription>();
    public ICollection<Log> Logs { get; set; } = new List<Log>();
    public ICollection<Event> Events { get; set; } = new List<Event>();
    public ICollection<EventReview> EventReviews { get; set; } = new List<EventReview>();
    public ICollection<EventEnrolment> EventEnrolments { get; set; } = new List<EventEnrolment>();
    public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();

    public void Update(string userName, string email, string firstName, string lastName, string photoLink, DateTime birthDate)
    {
        UserName = userName;
        NormalizedUserName = userName.ToUpperInvariant();
        Email = email;
        NormalizedEmail = email.ToUpperInvariant();
        FirstName = firstName;
        LastName = lastName;
        PhotoLink = photoLink;
        BirthDate = birthDate;
        UpdatedDate = DateTime.UtcNow;
    }

    public void UpdateProfile(string? firstName, string? lastName, string? photoLink, DateTime? birthDate)
    {
        int updatedFieldsCount = 0;

        if (!String.IsNullOrWhiteSpace(firstName))
        {
            FirstName = firstName;
            updatedFieldsCount++;
        }
        if (!String.IsNullOrWhiteSpace(lastName))
        {
            LastName = lastName;
            updatedFieldsCount++;
        }
        if (!String.IsNullOrWhiteSpace(photoLink))
        {
            PhotoLink = photoLink;
            updatedFieldsCount++;
        }
        if (birthDate.HasValue)
        {
            BirthDate = birthDate.Value;
            updatedFieldsCount++;
        }
        if (updatedFieldsCount > 0)
        {
            UpdatedDate = DateTime.UtcNow;
        }
    }

    public void UpdateRole(int roleId)
    {
        RoleId = roleId;
        UpdatedDate = DateTime.UtcNow;
    }

    public void UpdatePassword(string passwordHash)
    {
        PasswordHash = passwordHash;
        UpdatedDate = DateTime.UtcNow;
    }
}