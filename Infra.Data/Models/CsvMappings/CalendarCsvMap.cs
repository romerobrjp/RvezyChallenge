using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;
using Infra.Data.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Data.Models.CsvMappings;

public class CalendarCsvMap : ClassMap<Calendar>
{
  public CalendarCsvMap()
  {
    Map(m => m.ListingId).Name("listing_id");
    Map(m => m.Date).Name("date");
    Map(m => m.Available).Name("available");
    Map(m => m.Price).Name("price").Convert(_ =>
    {
      //_.Value.Price.ToString().Replace("$", "");
      NumberFormatInfo numberFormatInfo = new()
      {
        NegativeSign = "-",
        CurrencyDecimalSeparator = ".",
        CurrencyGroupSeparator = ",",
        CurrencySymbol = "$",
      };

      return decimal.Parse(_.Row.GetField("price") ?? "0.0", NumberStyles.Currency, numberFormatInfo);
    });
  }
}
