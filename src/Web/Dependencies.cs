using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Web.Data;
using Web.Interfaces;
using Web.Models;
using Web.Services;

namespace Web;

public static class Dependencies
{
    public static void AddWebServices(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(MappingProfile));

        services.AddTransient<IPlayersService, PlayersService>();
        services.AddTransient<IImageStorageService, ImageStorageService>();
    }

    public static void AddDbContext(this IServiceCollection services)
    {
        services.AddDbContext<ApplicationDbContext>(options => options.UseInMemoryDatabase("PlayersDb"));
    }

    public static void AddIdentity(this IServiceCollection services)
    {
        services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultUI();
    }
}