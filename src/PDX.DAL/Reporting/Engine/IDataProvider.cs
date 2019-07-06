using System;
using System.Linq.Expressions;
using System.Collections.Generic;
using PDX.DAL.Reporting.Models;
using DataTables.AspNet.Core;

namespace PDX.DAL.Reporting.Engine
{
    public interface IDataProvider
    {
         IEnumerable<dynamic> GetData(string query);
         IEnumerable<dynamic> GetData(string query, IList<Filter> filters = null, IList<IColumn> columns = null);
         Tuple<IEnumerable<dynamic>, IEnumerable<dynamic>> GetMultipleData(string query);
         Tuple<IEnumerable<dynamic>, IEnumerable<dynamic>> GetMultipleData(string query, IList<Filter> filters = null, IList<IColumn> columns = null);
    }
}