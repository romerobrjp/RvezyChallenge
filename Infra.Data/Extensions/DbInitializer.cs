using Infra.Data.Models.Context;
using Infra.Data.Seeds;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Data.Extensions;

internal class DbInitializer
{
  private readonly MainDbContext dbContext;
  private readonly IWebHostEnvironment env;

  public DbInitializer(MainDbContext dbContext, IWebHostEnvironment env)
  {
    this.dbContext = dbContext;
    this.env = env;
  }
  public void Seed()
  {
    dbContext.Database.EnsureCreated();

    if (!dbContext.Listings.Any())
    {
      var Seeder = new MainSeeder(dbContext, env);
      Seeder.SeedListings();
    }

    if (!dbContext.Reviews.Any())
    {
      var Seeder = new MainSeeder(dbContext, env);
      Seeder.SeedReviews();
    }

    if (!dbContext.Calendars.Any())
    {
      var Seeder = new MainSeeder(dbContext, env);
      Seeder.SeedCalendars();
    }
  }
}
