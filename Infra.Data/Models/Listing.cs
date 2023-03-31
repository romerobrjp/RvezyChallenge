using Infra.Data.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Data.Models;

public class Listing
{
    [Required]
    public long Id { get; set; }

    [Required]
    [JsonProperty("listing_url")]
    public string ListingUrl { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public string Description { get; set; }

    [Required]
    [JsonProperty("property_type")]
    public PropertyTypeEnum PropertyType { get; set; }
}
