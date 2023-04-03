using CsvHelper;
using CsvHelper.Configuration;
using Infra.Data.Models;
using Infra.Data.Models.Context;
using Infra.Data.Models.CsvMappings;
using Microsoft.AspNetCore.Hosting;
using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infra.Data.Seeds;

public class MainSeeder
{
  private readonly MainDbContext dbContext;
  private readonly IWebHostEnvironment webHostEnvironment;
  private CsvConfiguration csvReaderConfig;

  public MainSeeder(MainDbContext dbContext, IWebHostEnvironment webHostEnvironment)
  {
    this.dbContext = dbContext;
    this.webHostEnvironment = webHostEnvironment;
    this.csvReaderConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
    { 
      //HeaderValidated = null 
    };
  }

  public void SeedListings()
  {
    //Console.WriteLine(decimal.Parse("$5.50", NumberStyles.AllowCurrencySymbol | NumberStyles.Number, "$"));
    string projectPath = Directory.GetParent(".").FullName;
    using (var streamReader = new StreamReader(Path.GetFullPath(Path.Combine(projectPath, "Infra.Data", "Seeds", "listings.csv"))))
    using (var csvReader = new CsvReader(streamReader, this.csvReaderConfig))
    {
      csvReader.Context.RegisterClassMap<ListingCsvMap>();
      var items = csvReader.GetRecords<Listing>();

      foreach (var item in items)
      {
        Listing listing = new()
        {
          Id = item.Id,
          ListingUrl = item.ListingUrl,
          Name = item.Name,
          Description = item.Description,
          PropertyType = item.PropertyType,
        };

        this.dbContext.Listings.Add(listing);
      }

      this.dbContext.SaveChanges();
    }
  }

  public void SeedReviews()
  {
    string projectPath = Directory.GetParent(".").FullName;
    using (var streamReader = new StreamReader(Path.GetFullPath(Path.Combine(projectPath, "Infra.Data", "Seeds", "reviews.csv"))))
    using (var csvReader = new CsvReader(streamReader, this.csvReaderConfig))
    {
      csvReader.Context.RegisterClassMap<ReviewCsvMap>();
      var items = csvReader.GetRecords<Review>();

      foreach (var item in items)
      {
        Review review = new()
        {
          Id = item.Id,
          ListingId = item.ListingId,
          ReviewerId = item.ReviewerId,
          ReviewerName = item.ReviewerName,
          Date = item.Date,
          Comments = item.Comments,
        };

        this.dbContext.Reviews.Add(review);
      }

      this.dbContext.SaveChanges();
    }
  }

  public void SeedCalendars()
  {
    string projectPath = Directory.GetParent(".").FullName;
    using (var streamReader = new StreamReader(Path.GetFullPath(Path.Combine(projectPath, "Infra.Data", "Seeds", "calendar.csv"))))
    using (var csvReader = new CsvReader(streamReader, this.csvReaderConfig))
    {
      csvReader.Context.RegisterClassMap<CalendarCsvMap>();
      var items = csvReader.GetRecords<Models.Calendar>();

      foreach (var item in items)
      {
        Models.Calendar entity = new()
        {
          Date = item.Date,
          Available = item.Available,
          Price = item.Price,
          ListingId = item.ListingId
        };

        this.dbContext.Calendars.Add(entity);
      }

      this.dbContext.SaveChanges();
    }
  }
}
