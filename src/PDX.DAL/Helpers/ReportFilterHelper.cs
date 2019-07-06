using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using DataTables.AspNet.Core;
using PDX.DAL.Reporting.Models;

namespace PDX.DAL.Helpers {
    public static class ReportFilterHelper {
        private const string AND_OPERATOR = "&&",
            AND_STRING = "AND",
            ASCENDING_STRING = "ASC",
            DESCENDING_STRING = "DESC",
            COMMA_STRING = ",",
            DOT_STRING = ".";

        private static IDictionary<string, string> _dataTypeMapper = new Dictionary<string, string> () {
            {
            "String",
            "String"
            }, {
            "Integer",
            "Int32"
            }, {
            "Date",
            "DateTime"
            }, {
            "DateRange",
            "DateTime"
            }
        };
        public static string BuildOuterPredicate (Filter filter) {
            string predicate = string.Empty, whereClause = string.Empty;

            // Discover the type at runtime (and convert accordingly)
            if (!string.IsNullOrEmpty (filter.Value)) {
                string stype = _dataTypeMapper[filter.Type];

                switch (filter.Type) {
                    case "String":
                    case "Integer":
                        whereClause = "[{0}] = '{1}'";
                        predicate = String.Format (whereClause, filter.FieldName, filter.Value);

                        break;
                    case "Date":
                        whereClause = "([{0}] >=  #{1}#) AND ([{0}] < #{2}#)";
                        filter.Value = (DateTime.Parse(filter.Value)).ToString("MM/dd/yyyy");
                        var date = (DateTime.Parse(filter.Value).AddDays(1)).ToString("MM/dd/yyyy");
                        predicate = String.Format (whereClause, filter.FieldName,filter.Value,date);
                        break;

                    case "DateRange":
                        whereClause = "([{0}] >=  #{1}# ) AND ([{0}] <= #{2}#)";
                        var dateRange = filter.Value.Split (',');
                        predicate = String.Format (whereClause, filter.FieldName, DateTime.Parse (dateRange[0]), DateTime.Parse (dateRange[1]));

                        break;

                }
            }
            return predicate;
        }

        public static string BuildOuterPredicate (IEnumerable<Filter> filters) {
            var predicate = new StringBuilder ();
            foreach (var filter in filters) {
                var pr = BuildOuterPredicate (filter);
                if (string.IsNullOrEmpty (pr)) continue; //skip if filter predicate is null or empty
                predicate.Append ($" ({pr}) {AND_STRING}");
            }

            return predicate.ToString ().TrimEnd (AND_STRING.ToCharArray ());
        }

        public static string BuildPredicate (Filter filter) {
            string predicate = "1 = 1", whereClause = string.Empty;

            if (!string.IsNullOrEmpty (filter.Value)) {
                switch (filter.Type) {
                    case "String":
                    case "Integer":
                        whereClause = "{2} {0} = '{1}'";
                        predicate = String.Format (whereClause, GetFieldName (filter.FieldName, filter.OverridingFieldName, filter.Alias), filter.Value, AND_STRING);
                        break;
                    case "Date":
                        whereClause = "{2} cast({0} as date) = '{1}'";
                        predicate = String.Format (whereClause, GetFieldName (filter.FieldName, filter.OverridingFieldName, filter.Alias), filter.Value, AND_STRING);
                        break;
                    case "DateRange":
                        whereClause = "{3} (({0} >= '{1}') {3} ({0} <= '{2}'))";
                        if (!string.IsNullOrEmpty (filter.Value)) {
                            var dateRange = filter.Value.Split (',');
                            predicate = String.Format (whereClause, GetFieldName (filter.FieldName, filter.OverridingFieldName, filter.Alias), DateTime.Parse (dateRange[0]), DateTime.Parse (dateRange[1]), AND_STRING);
                        }

                        break;

                }
            }
            return predicate;
        }

        public static string BuildPredicate (IEnumerable<Filter> filters) {
            var predicate = new StringBuilder ();
            foreach (var filter in filters) {
                var pr = BuildPredicate (filter);
                if (string.IsNullOrEmpty (pr)) continue; //skip if filter predicate is null or empty
                predicate.Append ($" ({pr}) {AND_STRING}");
            }

            return predicate.ToString ().TrimEnd (AND_STRING.ToCharArray ());
        }

        public static string BuildSortString (IColumn column) {
            if (column == null) return string.Empty;
            string format = "{0} {1}";

            return string.Format (format, column.Field, column.Sort.Direction == DataTables.AspNet.Core.SortDirection.Descending ? DESCENDING_STRING : ASCENDING_STRING);
        }

        public static string BuildSortString (IEnumerable<IColumn> columns) {
            var sortString = new StringBuilder ();
            foreach (var column in columns) {
                var st = BuildSortString (column);
                if (string.IsNullOrEmpty (st)) continue; //skip if sorting string is null or empty
                sortString.Append ($" {st} {COMMA_STRING}");
            }

            return sortString.ToString ().TrimEnd (COMMA_STRING.ToCharArray ());
        }

        static string GetFieldName (string fieldName, string overridingFieldName = null, string alias = null) {
            return string.Format ("{0}{1}", (string.IsNullOrEmpty (alias) ? string.Empty : string.Format ("{0}{1}", alias, DOT_STRING)), (!string.IsNullOrEmpty (overridingFieldName) ? overridingFieldName : fieldName));
        }
    }
}