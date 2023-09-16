using TaskSync.Application.Context;
using TaskSync.Domain.Entities;
using TaskSync.Domain.Enums;

namespace TaskSync.API.Seed;

public static class SeedData
{
    public static async Task SeedAll(this IApplicationBuilder builder)
    {
        ApplicationDbContext db = builder.ApplicationServices.CreateScope().ServiceProvider
            .GetRequiredService<ApplicationDbContext>();

        if (db.Roles.Any())
            return;
        await db.Roles.AddRangeAsync(SeedRoles());
        await db.SaveChangesAsync();
    }
    public static IEnumerable<ApplicationRole> SeedRoles()
    {
        return new List<ApplicationRole>()
        {
            new ApplicationRole()
            {
                Name = UserType.Admin.ToStringValue(),
                NormalizedName = UserType.Admin.ToStringValue()!.ToUpper().Normalize()
            },

            new ApplicationRole()
            {
                Name = UserType.Admin.ToStringValue(),
                NormalizedName = UserType.User.ToStringValue()!.ToUpper().Normalize()
            },

        };
    }
}
