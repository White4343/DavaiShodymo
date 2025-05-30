using DavaiShodymo.Users;

namespace DavaiShodymo.Logs;

public class Log(int logTypeId, int userId)
{
    public int Id { get; set; }
    public DateTime DateStamp { get; set; } = DateTime.UtcNow;
    public int LogTypeId { get; set; } = logTypeId;
    public LogType LogType { get; set; } = null!;
    public int UserId { get; set; } = userId;
    public User User { get; set; } = null!;
    public ICollection<LogStripe> LogStripes { get; set; } = new List<LogStripe>();
}