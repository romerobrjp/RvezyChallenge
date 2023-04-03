using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Data.Models.Request;

public class ListingCreateRequest
{
  [JsonProperty("id")]
  public long Id { get; set; }
  
  [JsonProperty("listing_url")]
  public string? ListingUrl { get; set; }

  [JsonProperty("name")]
  public string Name { get; set; }

  [JsonProperty("description")]
  public string Description { get; set; }

  [JsonProperty("property_type")]
  public string PropertyType { get; set; }
}
