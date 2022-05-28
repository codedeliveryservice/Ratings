using Microsoft.AspNetCore.Identity;
using Web;
using Web.Data;
using Web.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext(builder.Configuration);
builder.Services.AddIdentity();
builder.Services.AddWebServices();

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddResponseCompression(options => options.EnableForHttps = true);

builder.Services.Configure<RouteOptions>(config =>
{
    config.LowercaseUrls = true;
    config.LowercaseQueryStrings = true;
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseResponseCompression();
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapDefaultControllerRoute();
    endpoints.MapRazorPages();
});

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    await ApplicationDbContextSeed.SeedSampleDataAsync(context, userManager, roleManager);
}

app.Run();