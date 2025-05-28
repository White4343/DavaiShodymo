using DavaiShodymo.Users;

namespace DavaiShodymo.Roles;

public class Role(string roleName)
{
    public int Id { get; set; }
    public string RoleName { get; set; } = roleName;
    public ICollection<User> Users { get; set; } = new List<User>();
}