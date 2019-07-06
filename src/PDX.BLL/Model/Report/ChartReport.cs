using System.Collections.Generic;
using PDX.DAL.Reporting.Models;

namespace PDX.BLL.Model.Report
{
    public class ChartReport
    {        
        public int ReportID { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ReportType { get; set; }
        public int? Priority { get; set; }
        public int? Width { get; set; }
        public string[] Series { get; set; }        
        public IList<Filter> Filters { get; set; }
        public IEnumerable<dynamic> Data { get; set; }
    }
}