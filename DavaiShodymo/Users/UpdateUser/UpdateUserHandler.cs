using System.Security.Claims;
using DavaiShodymo.JwtProviderHelper;

namespace DavaiShodymo.Users.UpdateUser;

public class UpdateUserHandler(IUserRepository userRepository)
{
    public async Task HandleAsync(UpdateUserCommand command, ClaimsPrincipal user, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(command);

        var existingUser = await userRepository.GetByIdAsync(command.Id, cancellationToken).ConfigureAwait(false);

        if (existingUser is null)
        {
            throw new BadHttpRequestException("User not found");
        }

        var authUserId = GetUserInformation.GetUserIdFromClaims(user);

        if (existingUser.Id != authUserId)
        {
            throw new UnauthorizedAccessException("You can only update your own profile");
        }

        existingUser.UpdateProfile(command.FirstName, command.LastName, command.PhotoLink, command.BirthDate);

        await userRepository.UpdateAsync(existingUser, cancellationToken).ConfigureAwait(false);
    }
}