using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Data.Models;

public class Calendar
{
    [Required]
    [JsonProperty("listing_id")]
    public int ListingId { get; set; }

    [Required]
    public DateTime? Date { get; set; }

    [Required]
    public bool Available { get; set; } = true;

    public float Price { get; set; } = 0f;
}
