using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Data.Models;
public class Review
{
    [Required]
    public long Id { get; set; }

    [Required]
    [JsonProperty("listing_id")]
    public int ListingId { get; set; }

    [Required]
    public DateTime? Date { get; set; }

    [Required]
    [JsonProperty("reviewer_id")]
    public long ReviwerId { get; set; }

    [JsonProperty("reviewer_name")]
    public string? ReviewerName { get; set; }

    public string? Comments { get; set; }
}
