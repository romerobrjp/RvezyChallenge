using Infra.Data.Models.Context;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Data.Extensions;

public static class SeederExtension
{
  public static IHost SeedData(this IHost host)
  {
    using (var scope = host.Services.CreateScope())
    {
      var services = scope.ServiceProvider;

      try
      {
        var context = services.GetRequiredService<MainDbContext>();
        var env = services.GetRequiredService<IWebHostEnvironment>();
        var seeder = new DbInitializer(context, env);
        Console.WriteLine(">> SeederExtension: Preparing to seed...");
        seeder.Seed();
      }
      catch (Exception)
      {
        Console.WriteLine(">> SeederExtension: An error occurred while trying to initialize the database.");
        throw;
      }
    }
    return host;
  }
}
