﻿using Web.Areas.Identity;

[assembly: HostingStartup(typeof(IdentityHostingStartup))]

namespace Web.Areas.Identity;

public class IdentityHostingStartup : IHostingStartup
{
    public void Configure(IWebHostBuilder builder)
    {
        builder.ConfigureServices((context, services) => { });
    }
}