using System.Collections.Generic;
using System.Threading.Tasks;
using DataTables.AspNet.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.NodeServices;
using PDX.API.Helpers;
using PDX.API.Services.Interfaces;
using PDX.BLL.Model;
using PDX.BLL.Services.Interfaces;
using PDX.Domain.Common;
using PDX.Domain.License;
using PDX.Domain.Views;
using System.Linq;

namespace PDX.API.Controllers.License
{
    [Authorize]
    [Route("api/[controller]")]
    public class MAReviewController : CrudBaseController<MAReview>
    {
        private readonly IMAReviewService _maReviewService;
        private readonly IService<MAStatus> _maStatusService;
        private readonly IChecklistService _checklistService;
        private readonly IMAService _maService;
        private readonly INodeServices _nodeServices;
        private readonly IGenerateDocuments _generateDocument;
        private readonly IRoleService _roleService;


        public MAReviewController(IMAReviewService maReviewService, IService<MAStatus> maStatusService, IChecklistService checklistService,
        IMAService maLogStatusService, INodeServices nodeServices, IGenerateDocuments generateDocument, IRoleService roleService) : base(maReviewService)
        {
            _maReviewService = maReviewService;
            _maStatusService = maStatusService;
            _checklistService = checklistService;
            _maService = maLogStatusService;
            _nodeServices = nodeServices;
            _generateDocument = generateDocument;
            _roleService = roleService;
        }



        /// <summary>
        /// Insert or Update maReview entity
        /// </summary>
        /// <param name="maReview">The maReview</param>
        /// <param name="changeStatus">Change status</param>
        /// <returns>bool</returns>
        [HttpPost]
        [Route("InsertOrUpdate/{changeStatus}")]
        public async Task<bool> CreateOrUpdateAsync([FromBody]MAReview maReview, bool? changeStatus = false)
        {
            var result = await _maReviewService.MAReviewCreateOrUpdateAsync(maReview, changeStatus);
            if ((bool)changeStatus)
            {
                await _generateDocument.GenerateRegistrationPDFDocument(_nodeServices, maReview.MAID, maReview.SuggestedStatusCode, maReview.ResponderID);
                var roleTeamLeader = (await _roleService.GetRolesByUserAsync(maReview.ResponderID)).FirstOrDefault(r => r.RoleCode == "ROLE_MODERATOR");
                var ma = await _maService.GetAsync(maReview.MAID);
                if (maReview.SuggestedStatusCode == "SFA" && roleTeamLeader != null && !ma.IsPremarketLabRequest)
                    await _generateDocument.GenerateRegistrationPDFDocument(_nodeServices, maReview.MAID, "CONL", maReview.ResponderID);
            }
            return result;
        }


        /// <summary>
        /// Get MAReview with Checklist by MA
        /// </summary>
        /// <param name="maID"></param>
        /// <param name="submoduleCode"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("ReviewWithChecklist/{maID}/{submoduleCode}")]
        public async Task<IEnumerable<MAReviewModel>> GetMAReviewWithChecklist(int maID, string submoduleCode)
        {
            var result = await _checklistService.GetMAReviewWithChecklist(maID, submoduleCode);
            return result;
        }

        /// <summary>
        /// Get MAReview by MA
        /// </summary>
        /// <param name="maID"></param>
        /// <param name="statusID"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("ByMA/{maID}/{statusID}")]
        public async Task<IEnumerable<MAReview>> GetMAReviewByMA(int maID, int? statusID)
        {
            var result = await _maReviewService.GetMAReviewByMA(maID, this.HttpContext.GetUserID(), statusID);
            return result;
        }
    }
}