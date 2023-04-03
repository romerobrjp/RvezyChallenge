using Infra.Data.Models;
using Infra.Data.Models.Context;
using Infra.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Data.Repositories;
public class ListingRepository : BaseRepository<Listing>, IListingRepository
{
  private new readonly IConfiguration configuration;

  public ListingRepository(MainDbContext dbContext, IConfiguration configuration) : base(dbContext, configuration)
  {
    this.configuration = configuration;
  }
}
