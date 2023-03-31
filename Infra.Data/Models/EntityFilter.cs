using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Data.Models;

public class EntityFilter
{
    public string TableName { get; set; }
    public int Offset { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; } // sql LIMIT
    public IDictionary<string, object> FieldsDictionary { get; set; }
    public string SortField { get; set; }
    public string SortOrder { get; set; }
    public int TotalRows { get; set; }
    public IList Rows { get; set; }

    public EntityFilter() { }

    public EntityFilter(
      string tableName,
      int offset,
      int pageNumber,
      int pageSize,
      IDictionary<string, object> fieldsDictionary,
      string sortField,
      string sortOrder,
      int totalRows
    )
    {
        TableName = tableName;
        Offset = offset;
        PageNumber = pageNumber;
        PageSize = pageSize;
        FieldsDictionary = fieldsDictionary;
        SortField = sortField;
        SortOrder = sortOrder;
        TotalRows = totalRows;
    }
}
