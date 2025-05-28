namespace DavaiShodymo.Users.GetProfile;

public class GetProfileHandler(IUserRepository userRepository)
{
    public async Task<GetProfileResponse> HandleAsync(int userId, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(userRepository);

        var user = await userRepository.GetByIdAsync(userId, cancellationToken).ConfigureAwait(false);

        if (user is null)
        {
            throw new BadHttpRequestException("User not found");
        }

        var response = new GetProfileResponse(user.UserName, user.FirstName, user.LastName, user.PhotoLink,
            user.BirthDate, user.CreatedDate);

        return response;
    }
}