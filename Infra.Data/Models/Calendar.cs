using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Data.Models;

public class Calendar : BaseEntity
{
  public Calendar()
  {
  }

  [Key]
  [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
  public override long? Id { get; set; }

  public long ListingId { get; set; }

  public Listing Listing { get; set; }

  public DateTime Date { get; set; }

  public bool Available { get; set; } = true;

  [Column(TypeName = "decimal(18,4)")]
  public decimal Price { get; set; } = 0m;
}
