using System.Collections.Generic;

namespace PDX.DAL.Reporting.Models
{
    public class Filter
    {
        public string Type { get; set; }
        public string FieldName { get; set; }
        public string Title { get; set; }
        public string Value { get; set; }
        public string Query { get; set; }
         public bool IsInnerFilter { get; set; }
        public string Alias { get; set; }
        public string OverridingFieldName { get; set; }
        public string ParameterName { get; set; }
        public IEnumerable<dynamic> Data { get; set; }
    }
}