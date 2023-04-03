using CsvHelper.Configuration.Attributes;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Data.Models;

public abstract class BaseEntity
{
  [Key]
  [DatabaseGenerated(DatabaseGeneratedOption.None)]
  public virtual long? Id { get; set; }
}
