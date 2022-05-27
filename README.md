# Ratings

Programming technology course project of building a website based on ASP.NET Core with basic CRUD operations using Entity Framework Core.

The idea is to create a simple system for managing the rating list of players.

## Key Features

* Display a list of chess players with their ratings and additional info on their details page
* Sort the list by columns with server side implementation
* Basic authentication and authorization
* `Create` `Update` `Delete` operations on players for users with the administrator role

## Technologies

* [ASP.NET Core 6 MVC / Razor Pages](https://docs.microsoft.com/en-us/aspnet/core/?view=aspnetcore-6.0)
* [ASP.NET Core Identity](https://docs.microsoft.com/en-us/aspnet/core/security/authentication/identity?view=aspnetcore-6.0&tabs=visual-studio)
* [Entity Framework Core 6](https://docs.microsoft.com/en-us/ef/core)
* [AutoMapper](https://github.com/AutoMapper/AutoMapper)
* [C# 10](https://docs.microsoft.com/en-us/dotnet/csharp)

## Design

Since this is purely a practice project, to simplify, the project uses a simple monolithic All-in-One architecture without much separation of concerns.

Some separation is still achieved through the use of folders, and includes folders such as Controllers, Views and Models as responsibilities of MVC pattern.

In addition, Data and Services folders have been added as an approximation of Application and Infrastructure layers.

For more information about architecture in ASP.NET Core, I highly recommend Steve Smith's book on [Microsoft Docs](https://docs.microsoft.com/en-us/dotnet/architecture/modern-web-apps-azure/common-web-application-architectures).

### Controller

```csharp
[HttpGet]
[AllowAnonymous]
public async Task<ActionResult> Details(int id)
{
    return View(await _playersService.GetPlayer(id));
}
```

### Service

```csharp
public async Task<PlayerViewModel> GetPlayer(int id)
{
    var player = await _context.Players.FirstOrDefaultAsync(x => x.Id == id);
    if (player is null)
    {
        throw new NotFoundException(nameof(Player), id);
    }

    return _mapper.Map<PlayerViewModel>(player);
}
```

## Screenshots

### Index Page

![Index Page](https://user-images.githubusercontent.com/93079612/170638145-95e20f56-668a-42c9-af6c-4ec3b6a5142e.png)

### Details Page

![Details Page](https://user-images.githubusercontent.com/93079612/170556509-fd13b2b0-14ce-4441-953f-56b5c9f101e9.png)

### Create Page

![Create Page](https://user-images.githubusercontent.com/93079612/170556512-1a5805b7-e309-449c-b227-1f2498709c0e.png)
