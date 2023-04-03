using CsvHelper.Configuration.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Data.Models;
public class Review : BaseEntity
{
  public Review()
  {
  }

  public Review(long Id, long ListingId, long ReviewerId, string ReviewerName, string Comments, string Date)
  {
    this.Id = Id;
    this.ListingId = ListingId;
    this.ReviewerId = ReviewerId;
    this.ReviewerName = ReviewerName;
    this.Comments = Comments;
    this.Date = DateTime.Parse(Date);
  }

  [JsonProperty("listing_id")]
  public long ListingId { get; set; }
  public Listing Listing { get; set; }

  [JsonProperty("reviewer_id")]
  public long ReviewerId { get; set; }

  [JsonProperty("reviewer_name")]
  public string ReviewerName { get; set; }

  [JsonProperty("comments")]
  public string Comments { get; set; }

  [JsonProperty("date")]
  public DateTime Date { get; set; }
}
