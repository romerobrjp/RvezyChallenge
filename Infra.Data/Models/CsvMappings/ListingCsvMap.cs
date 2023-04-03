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

public class ListingCsvMap : ClassMap<Listing>
{
  public ListingCsvMap()
  {
    Map(m => m.Id).Name("id");
    Map(m => m.ListingUrl).Name("listing_url");
    Map(m => m.Name).Name("name");
    Map(m => m.Description).Name("description");
    Map(m => m.PropertyType).Name("property_type");
  }
}
