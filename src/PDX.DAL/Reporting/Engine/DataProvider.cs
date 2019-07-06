using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using DataTables.AspNet.Core;
using PDX.DAL.Helpers;
using PDX.DAL.Reporting.Models;

namespace PDX.DAL.Reporting.Engine {
    public class DataProvider : IDataProvider {
        private readonly DataTableQueryExecuter _queryExecuter;
        public DataProvider (string connectionString) {
            _queryExecuter = new DataTableQueryExecuter (connectionString);
        }
        public IEnumerable<dynamic> GetData (string query) {
            var result = _queryExecuter.Execute (query);
            return result.ToExpando ();
        }
        public IEnumerable<dynamic> GetData (string query, IList<Filter> filters = null, IList<IColumn> columns = null) {
            if (filters == null) return GetData (query);
            //process query by injecting inner filter in the original query            
            var processedQuery = ProcessQuery (query, filters.Where (f => f.IsInnerFilter));
            var result = _queryExecuter.Execute (processedQuery);
            //if (columns == null) return result.ToExpando ();
            //Apply outer filters
            var processedResult = ProcessDataTable (result, filters.Where (f => !f.IsInnerFilter && f.Value != null), columns);
            return processedResult.ToExpando ();
        }

        public Tuple<IEnumerable<dynamic>, IEnumerable<dynamic>> GetMultipleData (string query) {
            var result = _queryExecuter.ExecuteMultiple (query);
            return new Tuple<IEnumerable<dynamic>, IEnumerable<dynamic>> (result.Item1.ToExpando (), result.Item2.ToExpando ());
        }
        public Tuple<IEnumerable<dynamic>, IEnumerable<dynamic>> GetMultipleData (string query, IList<Filter> filters = null, IList<IColumn> columns = null) {
            if (filters == null && columns == null) return GetMultipleData (query);

            //process query by injecting inner filter in the original query            
            var processedQuery = ProcessQuery (query, filters.Where (f => f.IsInnerFilter));
            var result = _queryExecuter.ExecuteMultiple (processedQuery);

            if (result != null) {
                //filter only data section (Item1)
                var processedResult = ProcessDataTable (result.Item1, filters.Where (f => !f.IsInnerFilter), columns);
                return new Tuple<IEnumerable<dynamic>, IEnumerable<dynamic>> (processedResult.ToExpando (), result.Item2.ToExpando ());
            }
            return null;
        }

        private DataTable ProcessDataTable (DataTable dataTable, IEnumerable<Filter> outerFilters = null, IEnumerable<IColumn> columns = null) {
            if (dataTable == null || dataTable.Rows.Count == 0) return dataTable;

            DataTable dt = dataTable;

            //Apply sorting
            if (columns != null && columns.Any ()) {
                dt = dt.OrderBy (columns);
            }

            //Apply sent filter here
            if (outerFilters != null && outerFilters.Any ()) {
                string predicate = ReportFilterHelper.BuildOuterPredicate (outerFilters);
                if (string.IsNullOrEmpty (predicate)) return dt;

                var filteredResult = dt.Select (predicate);
                var dtc = dataTable.Clone ();
                dtc.CopyToDataTable (filteredResult);

                //copy filtered dt to result
                dt = dtc;
            }

            return dt;
        }

        private string ProcessQuery (string query, IEnumerable<Filter> innerFilters = null) {
            if (innerFilters == null) return query;

            var q = query;

            //Apply inner filters from filters
            foreach (var innerFilter in innerFilters) {
                var filterQuery = innerFilter.Value != null? ReportFilterHelper.BuildPredicate (innerFilter): "";
                q = q.Replace ("@" + innerFilter.ParameterName, filterQuery);
            }

            return q;
        }
    }
}