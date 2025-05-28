using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace DavaiShodymo.JwtProviderHelper;

public static class GetUserInformation
{
    public static int GetUserIdFromClaims(ClaimsPrincipal user)
    {
        var userId = user.FindFirstValue(ClaimTypes.NameIdentifier);

        return Convert.ToInt32(userId);
    }

    public static int GetRoleIdFromClaims(ClaimsPrincipal user)
    {
        var roleId = user.FindFirstValue("RoleId");

        return Convert.ToInt32(roleId);
    }

    public static void CheckUserId(Guid authorUserId, string userId)
    {
        if (authorUserId.ToString() != userId)
        {
            throw new UnauthorizedAccessException("You are not authorized to view this task.");
        }
    }
}