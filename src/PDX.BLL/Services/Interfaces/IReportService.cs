using System.Collections.Generic;
using System.Threading.Tasks;
using PDX.Domain.Report;
using PDX.DAL.Reporting.Models;
using PDX.BLL.Model.Report;
using PDX.Domain.Account;
using DataTables.AspNet.Core;
using PDX.BLL.Model;
using System.Linq.Expressions;
using System;

namespace PDX.BLL.Services.Interfaces
{
    public interface IReportService : IService<Report>
    {
        Task<bool> SaveReportRolesAsync(IList<ReportRole> reportRoles);
        Task<IEnumerable<Report>> GetReportsByUser(int userID, Expression<Func<Report, bool>> predicate = null);
        Task<IEnumerable<TabularReport>> GetTabularReportsByUser(int userID);
        Task<IEnumerable<ChartReport>> GetChartReportsByUser(int userID, bool includeData = false);
        Task<ChartReport> GetChartReport(int reportID, int userID);
        Task<ChartReport> GetChartReport(Report report, int userID, bool includeData = false);
        Task<TabularReport> GetTabularReport(int reportID, bool includeData = false, IList<Filter> filters = null);
        Task<DataTablesResult<dynamic>> GetTabularReportData(int reportID, IDataTablesRequest request, IList<Filter> filters = null);
    }
}