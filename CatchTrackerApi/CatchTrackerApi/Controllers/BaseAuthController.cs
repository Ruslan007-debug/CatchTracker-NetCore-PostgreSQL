using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CatchTrackerApi.Controllers
{
    [ApiController]
    public class BaseAuthController : ControllerBase
    {
        protected int CurrentUserId => int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        protected void ValidateOwnership(int resourceUserId)
        {
            if (resourceUserId!=CurrentUserId && !User.IsInRole("Admin"))
            {
                Forbid("Access denied");
            }
        }
    }
}
