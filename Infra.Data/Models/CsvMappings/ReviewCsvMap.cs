using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;
using Infra.Data.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Data.Models.CsvMappings;

public class ReviewCsvMap : ClassMap<Review>
{
  public ReviewCsvMap()
  {
    Map(m => m.Id).Name("id");
    Map(m => m.ListingId).Name("listing_id");
    Map(m => m.ReviewerId).Name("reviewer_id");
    Map(m => m.ReviewerName).Name("reviewer_name");
    Map(m => m.Comments).Name("comments");
    Map(m => m.Date).Name("date");
  }
}
