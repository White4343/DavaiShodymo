namespace DavaiShodymo.PasswordHasherHelper;

public interface IPasswordHasher
{
    public string Hash(string password);
    public bool Verify(string password, string passwordHash);
}