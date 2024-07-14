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
            if (appUser == null)
            {
                // تسجيل دخول
                Console.WriteLine("User not found");
                return false;
            }
            return await userManager.IsInRoleAsync(appUser, role);
        }
        // تسجيل دخول
        Console.WriteLine("User is not authenticated");
        return false;
    }
}
