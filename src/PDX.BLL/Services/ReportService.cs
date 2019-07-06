using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DataTables.AspNet.Core;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using PDX.BLL.Helpers;
using PDX.BLL.Model;
using PDX.BLL.Model.Config;
using PDX.BLL.Model.Report;
using PDX.BLL.Services.Interfaces;
using PDX.DAL.Helpers;
using PDX.DAL.Reporting.Engine;
using PDX.DAL.Reporting.Models;
using PDX.DAL.Repositories;
using PDX.Domain.Account;
using PDX.Domain.Report;

namespace PDX.BLL.Services {
    public class ReportService : Service<Report>, IReportService {
        //Key => Filter Field Name, Value => Filter Value
        private IDictionary<string, string> _innerFilterDictionaryMap = new Dictionary<string, string> ();
        private readonly IService<ReportRole> _reportRoleService;
        private readonly IUserService _userService;
        private readonly IDataProvider _dataProvider;
        private readonly IService<UserAgent> _userAgentService;

        public ReportService (IUnitOfWork unitofWork, IService<ReportRole> reportRoleService,
            IUserService userService, IOptions<ConnectionConfig> connectionConfig, IService<UserAgent> userAgentService) : base (unitofWork) {
            var connection = connectionConfig.Value;

            _reportRoleService = reportRoleService;
            _userService = userService;
            _userAgentService = userAgentService;
            _dataProvider = new DataProvider (connection.DefaultConnection);

        }

        public async Task<bool> SaveReportRolesAsync (IList<ReportRole> reportRoles) {
            var result = true;
            foreach (var rr in reportRoles) {
                var existingReportRole = await _reportRoleService.GetAsync (rr.ID);
                result = result && (existingReportRole != null ? await _reportRoleService.UpdateAsync (rr) : await _reportRoleService.CreateAsync (rr));
            }
            return result;
        }

        public async Task<IEnumerable<Report>> GetReportsByUser (int userID, Expression<Func<Report, bool>> predicate = null) {
            var user = await _userService.GetAsync (userID);
            var roleIDs = user.Roles.Select (r => r.ID);
            var reportRoles = await _reportRoleService.FindByAsync (rr => rr.IsActive && rr.Report.IsActive && roleIDs.Contains (rr.RoleID));
            var reports = reportRoles.Select (rr => rr.Report).Distinct ();
            reports = reports.Where (predicate.Compile ());
            return reports;
        }
        public async Task<IEnumerable<TabularReport>> GetTabularReportsByUser (int userID) {
            IList<TabularReport> tabularReports = new List<TabularReport> ();
            var reports = await GetReportsByUser (userID, r => r.ReportType.ReportTypeCode == "Tabular");

            foreach (var report in reports) {
                tabularReports.Add (ConvertToTabularReport (report));
            }

            return tabularReports;
        }

        public async Task<ChartReport> GetChartReport (int reportID, int userID) {
            var report = await this.GetAsync (reportID);
            return await GetChartReport (report, userID, true);
        }

        public async Task<ChartReport> GetChartReport (Report report, int userID, bool includeData = false) {

            var rp = new ChartReport {
            ReportID = report.ID,
            ReportType = report.ReportType.ReportTypeCode,
            Title = report.Title,
            Name = report.Name,
            Series = string.IsNullOrWhiteSpace (report.SeriesColumns) ? null : report.SeriesColumns.Split (','),
            Description = report.Description,
            Priority = report.Priority,
            Width = report.Width

            };

            if (includeData) {
                if (!_innerFilterDictionaryMap.Keys.Any ()) {
                    var user = await _userService.GetAsync (userID);
                    var userAgent = (await _userAgentService.FindByAsync (ur => ur.UserID == userID && ur.IsActive)).FirstOrDefault ();
                    PopulateInnerFiltersDictionaryMap (user, userAgent?.AgentID);
                }

                var filters = report.FilterColumns.Select (s => s.ToObject<Filter> ()).ToList ();

                //Populate default inner filter values
                foreach (var innerFilter in filters.Where (f => f.IsInnerFilter)) {
                    var defaultFilterValue = _innerFilterDictionaryMap[innerFilter.FieldName];
                    innerFilter.Value = defaultFilterValue;
                }

                var reportData = _dataProvider.GetData (report.Query, filters);
                rp.Data = reportData;
            }

            return rp;
        }

        public async Task<IEnumerable<ChartReport>> GetChartReportsByUser (int userID, bool includeData = false) {
            var reports = new List<ChartReport> ();
            var chartReports = await GetReportsByUser (userID, (r => r.ReportType.ReportTypeCode != "Tabular"));

            foreach (var report in chartReports) {
                var rp = await GetChartReport (report, userID, includeData);
                reports.Add (rp);
            }

            return reports.OrderBy (o => o.Priority);
        }
        public async Task<TabularReport> GetTabularReport (int reportID, bool includeData = false, IList<Filter> filters = null) {
            var report = await base.GetAsync (r => r.ID == reportID && r.ReportType.ReportTypeCode == "Tabular");
            if (report == null) return null;

            var tabularReport = ConvertToTabularReport (report, includeData, filters);
            return tabularReport;
        }

        public async Task<DataTablesResult<dynamic>> GetTabularReportData (int reportID, IDataTablesRequest request, IList<Filter> filters = null) {
            var report = await base.GetAsync (reportID);
            var reportData = _dataProvider.GetData (report.Query, filters, request?.Columns?.Where (c => c.Sort != null).ToList ());
            var totalRecords = reportData.Count ();

            var pageData = reportData.Skip (request.Start).Take (request.Length);
            var response = new DataTablesResult<dynamic> (request.Draw, totalRecords, totalRecords, pageData);

            return response;
        }

        private TabularReport ConvertToTabularReport (Report report, bool includeData = false, IList<Filter> filters = null) {
            TabularReport tabularReport = new TabularReport ();
            tabularReport.Report = report;
            tabularReport.Filters = report.FilterColumns.Select (s => s.ToObject<Filter> ()).ToList ();

            foreach (var filter in tabularReport.Filters) {
                if (!string.IsNullOrWhiteSpace (filter.Query)) {
                    var filterData = _dataProvider.GetData (filter.Query);
                    filter.Data = filterData;
                }
            }

            if (includeData) {
                var reportData = _dataProvider.GetData (report.Query, filters != null && filters.Count () > 0 ? filters : tabularReport.Filters);
                tabularReport.Data = reportData;
            }

            return tabularReport;
        }

        private void PopulateInnerFiltersDictionaryMap (User user, int? agentID = null) {
            foreach (var role in user.Roles) {
                var keyValuePair = GetInnerFilterByRole (role.RoleCode, user.ID, agentID);
                if (!string.IsNullOrEmpty (keyValuePair.Value)) {
                    //Insert KeyValuePair
                    _innerFilterDictionaryMap.Add (keyValuePair);
                }
            }
        }

        private KeyValuePair<string, string> GetInnerFilterByRole (string roleCode, int userID, int? agentID = null) {
            KeyValuePair<string, string> keyValuePair = new KeyValuePair<string, string> ();

            switch (roleCode) {
                //Customer Service Officer, Medicine Dossier Assessor, Port inspector
                case "CSO":
                case "ROLE_REVIEWER":
                case "ROLE_FOOD_REVIEWER":
                case "PINS":
                    keyValuePair = KeyValuePair.Create ("user_id", userID.ToString ());
                    break;
                case "IPA":
                case "PIPA":
                    keyValuePair = KeyValuePair.Create ("agent_id", agentID.ToString ());
                    break;

            }

            return keyValuePair;
        }
    }
}