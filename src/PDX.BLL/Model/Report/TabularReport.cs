using System.Collections.Generic;
using PDX.DAL.Reporting.Models;
using PDX.Domain.Report;

namespace PDX.BLL.Model.Report
{
    public class TabularReport
    {
        public PDX.Domain.Report.Report Report { get; set; }
        public IList<Filter> Filters { get; set; }
        public IEnumerable<dynamic> Data { get; set; }

    }
}