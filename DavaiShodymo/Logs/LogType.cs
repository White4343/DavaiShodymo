namespace DavaiShodymo.Logs;

public class LogType(string name)
{
    public int Id { get; set; }
    public string Name { get; set; } = name;
    public ICollection<Log> Logs { get; set; } = new List<Log>();

    public void Update(string name)
    {
        Name = name;
    }
}