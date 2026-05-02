using Microsoft.AspNetCore.Identity;
using LifeHub.Domain.Models;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace LifeHub.Infrastructure.Seed
{
    public static class DataSeeder
    {
        public static async Task SeedRolesAndAdminAsync(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                // ...existing code...
            }
        }
    }
}
