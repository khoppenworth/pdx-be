using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataTables.AspNet.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.NodeServices;
using PDX.API.Helpers;
using PDX.API.Model;
using PDX.API.Services.Interfaces;
using PDX.BLL.Helpers;
using PDX.BLL.Model;
using PDX.BLL.Model.Report;
using PDX.BLL.Services.Interfaces;
using PDX.DAL.Reporting.Models;
using PDX.DAL.Repositories;
using PDX.Domain.Account;
using PDX.Domain.Customer;
using PDX.Domain.Report;
using PDX.Domain.Views;

namespace PDX.API.Controllers {
    [Authorize]
    [Route ("api/[controller]")]
    public class ReportController : CrudBaseController<Report> {
        private readonly IReportService _reportService;
        private readonly INodeServices _nodeServices;
        private readonly IDocumentService _documentService;
        private readonly IGenerateDocuments _generateDoc;
        public ReportController (IReportService reportService, INodeServices nodeServices, IDocumentService documentService, IGenerateDocuments generateDoc) : base (reportService) {
            _reportService = reportService;
            _documentService = documentService;
            _nodeServices = nodeServices;
            _generateDoc = generateDoc;
        }

        /// <summary>
        /// Get all reports
        /// </summary>
        [HttpGet]
        public override async Task<IEnumerable<Report>> GetAllAsync () {
            var entities = await _reportService.GetAllAsync (true);
            return entities;
        }
        /// <summary>
        /// get reports by User
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route ("ByUser/{userID}")]
        public async Task<IEnumerable<Report>> GetReportsByUser (int userID) {
            var result = await _reportService.GetReportsByUser (userID);
            return result;
        }

        /// <summary>
        /// get chart reports by user
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route ("Chart/ByUser/{userID}/{includeData?}")]
        public async Task<IEnumerable<ChartReport>> GetChartReportsByUser (int userID, [FromRoute] bool includeData = false) {
            var result = await _reportService.GetChartReportsByUser (userID, includeData);
            return result;
        }

        /// <summary>
        /// get single chart reports
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route ("Chart/{reportID}")]
        public async Task<ChartReport> GetChartReport (int reportID) {
            var chartReport = await _reportService.GetChartReport (reportID, this.HttpContext.GetUserID ());
            return chartReport;
        }

        #region Tabular Reports

        /// <summary>
        /// get tabular reports by user
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route ("Tabular/ByUser/{userID}")]
        public async Task<IEnumerable<TabularReport>> GetTabularReportsByUser (int userID) {
            var result = await _reportService.GetTabularReportsByUser (userID);
            return result;
        }

        /// <summary>
        /// get single tabular reports by id
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route ("Tabular/{reportID}")]
        public async Task<TabularReport> GetTabularReport (int reportID) {
            var result = await _reportService.GetTabularReport (reportID);
            return result;
        }

        /// <summary>
        /// Get tabular report
        /// </summary>
        /// <param name="reportID">filters</param>
        /// <param name="filters">filters</param>
        /// <returns>bool</returns>
        [HttpPost]
        [Route ("Tabular/{reportID}")]
        public virtual async Task<DataTablesResult<dynamic>> GetTabularReport (int reportID, IList<Filter> filters, [FromBody] IDataTablesRequest request) {
            var result = await _reportService.GetTabularReportData (reportID, request, filters);
            return result;
        }

        /// <summary>
        /// Export tabular report to pdf
        /// </summary>
        /// <param name="reportID"></param>
        /// <returns></returns>
        [HttpPost]
        [Route ("Tabular/Export/PDF/{reportID}")]
        public async Task<IActionResult> ExportToPDF (int reportID, [FromBody] IList<Filter> filters = null) {
            var filePath = await _generateDoc.GenerateReportDocument (_nodeServices, reportID, filters);
            var result = await _documentService.ReadPhysicalFile (filePath, "application/pdf");
            return result;
        }

        /// <summary>
        /// Export tabular report to pdf
        /// </summary>
        /// <param name="reportID"></param>
        /// <returns></returns>
        [HttpPost]
        [Route ("Tabular/Export/Excel/{reportID}")]
        public async Task<IActionResult> ExportToExcel (int reportID,[FromBody] IList<Filter> filters) {
            var tabularReport = await _reportService.GetTabularReport (reportID, true, filters);
            var result = new ExcelFileResult (tabularReport.Data, tabularReport.Report.ColumnDefinitions);
            return result;
        }

        #endregion

        /// <summary>
        /// Save Report Roles
        /// </summary>
        /// <param name="reportRoles">The role</param>
        /// <returns>bool</returns>
        [HttpPost]
        [Route ("ReportRoles")]
        public async Task<bool> SaveMenuRoles ([FromBody] IList<ReportRole> reportRoles) {
            var result = await _reportService.SaveReportRolesAsync (reportRoles);
            return result;
        }

    }
}