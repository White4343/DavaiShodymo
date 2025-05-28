using DavaiShodymo.Users;

namespace DavaiShodymo.JwtProviderHelper;

public interface IJwtProvider
{
    string Generate(User user);
}