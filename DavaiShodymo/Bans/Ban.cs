using DavaiShodymo.Users;

namespace DavaiShodymo.Bans;

public class Ban(string reason, DateTime dateEnd, int userId, int adminUserId)
{
    public int Id { get; set; }
    public string Reason { get; set; } = reason;
    public DateTime DateEnd { get; set; } = dateEnd;
    public DateTime DateStamp { get; set; } = DateTime.UtcNow;
    public bool IsActive { get; set; } = DateTime.UtcNow < dateEnd;
    public int UserId { get; set; } = userId;
    public User User { get; set; } = null!;
    public int AdminUserId { get; set; } = adminUserId;
    public User AdminUser { get; set; } = null!;

    public void Update(string reason, DateTime dateEnd)
    {
        Reason = reason;
        DateEnd = dateEnd;
        IsActive = dateEnd > DateTime.UtcNow;
        DateStamp = DateTime.UtcNow;
    }

    public void Unban()
    {
        IsActive = false;
        DateEnd = DateTime.UtcNow;
        DateStamp = DateTime.UtcNow;
    }
}