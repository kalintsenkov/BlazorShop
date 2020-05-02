namespace SheryLady.Services.Users
{
    public interface IUsersService
    {
        string GenerateJwtToken(string userId, string userName, string secret);
    }
}
