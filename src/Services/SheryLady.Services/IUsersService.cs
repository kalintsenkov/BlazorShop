namespace SheryLady.Services
{
    public interface IUsersService
    {
        string GenerateJwtToken(string userId, string userName, string secret);
    }
}
