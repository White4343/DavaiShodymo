using DavaiShodymo.PasswordHasherHelper;

namespace DavaiShodymo.Users.Register;

public class RegisterHandler(IUserRepository userRepository, IPasswordHasher passwordHasher)
{
    public async Task<bool> HandleAsync(RegisterCommand command, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(command);

        // check if email is already taken
        if (await IsEmailTaken(command.Email, cancellationToken).ConfigureAwait(false))
        {
            throw new BadHttpRequestException("The email is already taken");
        }

        // check if username is already taken
        if (await IsUsernameTaken(command.UserName, cancellationToken).ConfigureAwait(false))
        {
            throw new BadHttpRequestException("The username is already taken");
        }

        User user = new User(command.UserName, command.Email, passwordHasher.Hash(command.Password),
            command.FirstName, command.LastName, command.PhotoLink, command.BirthDate, 1);

        await userRepository.AddAsync(user, cancellationToken).ConfigureAwait(false);

        return true;
    }

    private async Task<bool> IsEmailTaken(string email, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByEmailAsync(email, cancellationToken).ConfigureAwait(false);

        return user != null;
    }

    private async Task<bool> IsUsernameTaken(string username, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByUsernameAsync(username, cancellationToken).ConfigureAwait(false);

        return user != null;
    }
}