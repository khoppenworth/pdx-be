using System.Collections.Generic;
using DataTables.AspNet.AspNetCore;

namespace PDX.BLL.Model
{
    /// <summary>
    /// DataTableResult
    /// </summary>
    public class DataTablesResult<T> : DataTablesResponse where T : class
    {
       public DataTablesResult(int draw, int totalRecords, int totalRecordsFiltered, IEnumerable<T> data): base(draw, totalRecords, totalRecordsFiltered, data)
       {
           
       }
       public int RecordsTotal { get{return this.TotalRecords;} }
       public int RecordsFiltered { get{return this.TotalRecordsFiltered;} }
    }
}