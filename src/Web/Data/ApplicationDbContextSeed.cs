using Microsoft.AspNetCore.Identity;
using Web.Models;

namespace Web.Data;

public class ApplicationDbContextSeed
{
    public static async Task SeedSampleDataAsync(ApplicationDbContext context,
        UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        if (context.Players.Any() || context.Users.Any())
        {
            return;
        }

        await roleManager.CreateAsync(new IdentityRole(Constants.Administrator));

        // NOTE: The hard-coded Guid Id is used in case of using an in-memory database
        var adminUser = new ApplicationUser
        {
            Id = "833225B1-8C1C-4DF3-8D9C-3398492F198B",
            UserName = "admin@gmail.com",
            Email = "admin@gmail.com"
        };

        await userManager.CreateAsync(adminUser, password: "Admin123.");
        await userManager.AddToRoleAsync(adminUser, Constants.Administrator);

        context.Players.AddRange(GetPlayers());
        await context.SaveChangesAsync();
    }

    private static IEnumerable<Player> GetPlayers()
    {
        return new List<Player>
        {
            new Player("Magnus Carlsen", "Norway", 2864, 2847, 2832,
                "World Champion 2013 till the present, after defending his title against Anand in 2014, Karjakin in 2016, Caruana in 2018 and Nepomniachtchi in 2021.",
                "/images/f671bb39-9228-4540-aa44-ad3e9d5d86ae.jpg"),

            new Player("Ding Liren", "China", 2806, 2836, 2788,
                "Champion of China 2009, 2011 & 2012. In the world's top 100 ranked players since May 2011.",
                "/images/355bb56c-ee8d-4455-83c6-2d181c40a346.jpg"),

            new Player("Alireza Firouzja", "France", 2793, 2670, 2791,
                "At the age of 18 years and 5 month, Alireza Firouzja is the youngest player ever to reach a rating of 2800."),

            new Player("Fabiano Caruana", "USA", 2782, 2765, 2846,
                "Highest ever live rating was 2851.3 after winning the Sinquefield Cup 2014 with 8.5/10."),

            new Player("Levon Aronian", "USA", 2774, 2727, 2849,
                "Aronian has long been Armenia's strongest player, but now represents the US. He is now the US #1 in the ratings.")
        };
    }
}