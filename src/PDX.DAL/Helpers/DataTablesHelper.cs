using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using DataTables.AspNet.Core;

namespace PDX.DAL.Helpers {
    public static class DataTablesHelper {
        private const string AndString = "AND";

        public static IQueryable<T> OrderBy<T> (this IQueryable<T> source, IEnumerable<IColumn> columns) {
            var expression = source.Expression;
            int count = 0;
            foreach (var column in columns) {
                var parameter = Expression.Parameter (typeof (T), "x");
                var selector = Expression.PropertyOrField (parameter, column.Field);
                var method = column.Sort.Direction == DataTables.AspNet.Core.SortDirection.Descending ?
                    (count == 0 ? "OrderByDescending" : "ThenByDescending") :
                    (count == 0 ? "OrderBy" : "ThenBy");
                expression = Expression.Call (typeof (Queryable), method, new Type[] { source.ElementType, selector.Type },
                    expression, Expression.Quote (Expression.Lambda (selector, parameter)));
                count++;
            }
            return count > 0 ? source.Provider.CreateQuery<T> (expression) : source;
        }

        public static DataTable OrderBy (this DataTable source, IEnumerable<IColumn> columns) {
            string sortingString = ReportFilterHelper.BuildSortString (columns);
            source.DefaultView.Sort = sortingString;
            return source.DefaultView.ToTable ();

        }

        public static void CopyToDataTable (this DataTable dataTable, DataRow[] rowArray) {
            foreach (DataRow row in rowArray) {
                dataTable.ImportRow (row);
            }
        }

    }

    public static class ObjectToDictionaryHelper {
        public static IEnumerable<IDictionary<string, object>> ToDictionary (this IEnumerable<dynamic> data) {
            var dictionaries = new List<Dictionary<string, object>> ();
            foreach (var d in data) {
                dictionaries.Add (ToDictionary (d));
            }
            return dictionaries;
        }
        public static IDictionary<string, object> ToDictionary (this object source) {
            return source.ToDictionary<object> ();
        }

        public static IDictionary<string, T> ToDictionary<T> (this object source) {
            if (source == null)
                ThrowExceptionWhenSourceArgumentIsNull ();

            var dictionary = new Dictionary<string, T> ();
            foreach (PropertyDescriptor property in TypeDescriptor.GetProperties (source))
                AddPropertyToDictionary<T> (property, source, dictionary);

            return dictionary;
        }

        /// <summary>
        /// Extension method that turns a dictionary of string and object to an ExpandoObject
        /// </summary>
        public static dynamic ToExpando (this IDictionary<string, object> dictionary) {
            var expando = new ExpandoObject ();
            var expandoDic = (IDictionary<string, object>) expando;

            // go through the items in the dictionary and copy over the key value pairs)
            foreach (var kvp in dictionary) {
                // if the value can also be turned into an ExpandoObject, then do it!
                if (kvp.Value is IDictionary<string, object>) {
                    var expandoValue = ((IDictionary<string, object>) kvp.Value).ToExpando ();
                    expandoDic.Add (kvp.Key, expandoValue);
                } else if (kvp.Value is ICollection) {
                    // iterate through the collection and convert any string-object dictionaries
                    // along the way into expando objects
                    var itemList = new List<object> ();
                    foreach (var item in (ICollection) kvp.Value) {
                        if (item is IDictionary<string, object>) {
                            var expandoItem = ((IDictionary<string, object>) item).ToExpando ();
                            itemList.Add (expandoItem);
                        } else {
                            itemList.Add (item);
                        }
                    }

                    expandoDic.Add (kvp.Key, itemList);
                } else {
                    expandoDic.Add (kvp);
                }
            }

            return expando;
        }

        public static IEnumerable<dynamic> ToExpando (this IEnumerable<IDictionary<string, object>> dictionaries) {
            return dictionaries.Select (dict => dict.ToExpando ());
        }

        public static IEnumerable<dynamic> ToExpando (this DataTable datatable) {
            if (datatable != null) {
                var index = 1;
                foreach (DataRow row in datatable.Rows) {
                    var expandoObject = new ExpandoObject ();

                    ((IDictionary<String, Object>) expandoObject).Add ("RowNumber", index++);
                    foreach (DataColumn column in datatable.Columns) {
                        if (column.ColumnName != "RowNumber") {
                            ((IDictionary<String, Object>) expandoObject).Add (column.ColumnName, row[column.ColumnName] != DBNull.Value?row[column.ColumnName] : null);

                        }
                    }

                    yield return expandoObject;
                }
            }

        }

        private static void AddPropertyToDictionary<T> (PropertyDescriptor property, object source, Dictionary<string, T> dictionary) {
            object value = property.GetValue (source);
            if (IsOfType<T> (value))
                dictionary.Add (property.Name, (T) value);
        }

        private static bool IsOfType<T> (object value) {
            return value is T;
        }

        private static void ThrowExceptionWhenSourceArgumentIsNull () {
            throw new ArgumentNullException ("source", "Unable to convert object to a dictionary. The source object is null.");
        }
    }
}