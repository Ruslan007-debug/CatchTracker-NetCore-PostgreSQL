using CatchTrackerApi.Models;

namespace CatchTrackerApi.Interfaces.ServiceInterfaces
{
    public interface IJWTService
    {
        string GenerateAccessToken(User user);
        string GenerateRefreshToken();
    }
}
