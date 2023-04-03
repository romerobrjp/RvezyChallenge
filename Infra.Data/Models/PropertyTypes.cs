using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Data.Models;

public class PropertyTypes
{
  private PropertyTypes(string value) { Value = value; }

  public string Value { get; private set; }

  public static PropertyTypes Apartment { get { return new PropertyTypes("apartment"); } }
  public static PropertyTypes House { get { return new PropertyTypes("house"); } }
  public static PropertyTypes Get(string value)
  {
    return new PropertyTypes(value);
  }

  public override string ToString()
  {
    return Value;
  }
}
