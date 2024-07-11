using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Threading.Tasks;

public static class UserHelper
{
    public static async Task<bool> IsInRoleAsync(this ClaimsPrincipal user, string role, UserManager<IdentityUser> userManager)
    {
        if (user.Identity.IsAuthenticated)
        {
            var appUser = await userManager.GetUserAsync(user);
            return await userManager.IsInRoleAsync(appUser, role);
        }
        return false;
    }
}
