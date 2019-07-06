using System.Collections.Generic;
using System.Threading.Tasks;
using DataTables.AspNet.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PDX.API.Helpers;
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
    public class MAAssignmentController : BaseController<MAAssignment>
    {
        private readonly IMAAssignmentService _maAssignmentService;
        private readonly IMAService _maService;
        public MAAssignmentController(IMAAssignmentService maAssignmentService, IMAService maService) : base(maAssignmentService)
        {
            _maAssignmentService = maAssignmentService;
            _maService = maService;
        }


        /// <summary>
        /// Save multiple ma assignments
        /// </summary>
        /// <param name="maAssignments"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> AssignMA([FromBody]IList<MAAssignment> maAssignments)
        {
            var result = true;
            foreach (var maAssignment in maAssignments)
            {
                result = result && await _maAssignmentService.CreateAsync(maAssignment);
            }
            var currentStatus = await _maService.GetMAStatus(maAssignments.FirstOrDefault().MAID);
            if (result && currentStatus.MAStatusCode == "VER")
            {
                var maLogStatus = new MAStatusLogModel
                {
                    MAID = maAssignments.FirstOrDefault().MAID,
                    ToStatusCode = "ASD",
                    Comment = "Registration Assigned",
                    ChangedByUserID = maAssignments.FirstOrDefault().AssignedByUserID
                };
                await _maService.ChangeMAStatusAsync(maLogStatus);
            }
            return result;
        }

        /// <summary>
        /// Get ResponderType from MAAssignments
        /// </summary>
        /// <param name="maID"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("ResponderType/{maID}/{userID}")]
        public async Task<ResponderType> GetResponderType(int maID, int userID)
        {
            var result = await _maAssignmentService.GetResponderType(maID, userID);
            return result;
        }

        [HttpGet]
        [Route("Grouped/{maID}")]
        public async Task<object> GetGroupedMAAssignments(int maID)
        {
            var result = await _maAssignmentService.GetGroupedMAAssignments(maID);
            return result;
        }

        [HttpGet]
        [Route("ByMA/{maID}")]
        public async Task<object> GetMAAssignments(int maID)
        {
            var result = await _maAssignmentService.GetMAAssignments(maID);
            return result;
        }

    }
}