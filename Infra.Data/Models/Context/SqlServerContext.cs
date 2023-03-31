using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Data.Models.Context;
public class SqlServerContext : DbContext
{
    public DbSet<Listing> Listings { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<Calendar> Calendars { get; set; }

    public SqlServerContext(DbContextOptions opts) : base(opts)
    {
    }    

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Listing>().HasMany<Review>(_ => _.Reviews);
            //.WithOne(_ => _.Listing)
            //.HasForeignKey<Listing>(_ => _.ListingId);
        modelBuilder.Entity<Listing>().HasMany<Calendar>(_ => _.Calendars);
    }
}
