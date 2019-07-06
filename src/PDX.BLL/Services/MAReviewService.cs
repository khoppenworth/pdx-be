using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PDX.BLL.Model;
using PDX.BLL.Services.Interfaces;
using PDX.DAL.Repositories;
using PDX.Domain.Common;
using PDX.Domain.License;

namespace PDX.BLL.Services
{
    public class MAReviewService : Service<MAReview>, IMAReviewService
    {
        private readonly IUserService _userService;
        private readonly IService<MAStatus> _maStatusService;
        private readonly IMAService _maService;
        private readonly IService<MAAssignment> _maAssignmentService;
        public MAReviewService(IUnitOfWork unitOfWork, IUserService userService, IService<MAStatus> maStatusService, IMAService maService,
        IService<MAAssignment> maAssignmentService) : base(unitOfWork)
        {
            _userService = userService;
            _maStatusService = maStatusService;
            _maService = maService;
            _maAssignmentService = maAssignmentService;
        }

        public async Task<IEnumerable<MAReview>> GetMAReviewByMA(int maID, int userID, int? statusID)
        {
            var user = await _userService.GetAsync(userID);
            var fRole = user.Roles.FirstOrDefault();
            IEnumerable<MAReview> maReviews = null;

            if (fRole.RoleCode == "IPA")
            {
                maReviews = await base.FindByAsync(mar => mar.MAID == maID && !mar.IsDraft
                && (new List<string> { "PRSC", "TLD", "APL" }).Contains(mar.ResponderType.ResponderTypeCode)
                && (new List<string> { "RTA", "FIR",  "FIRR", "RQST" }).Contains(mar.SuggestedStatus.MAStatusCode));
            }
            else if ((new List<string> { "CSO", "CST", "CSD","ROLE_FOOD_REVIEWER" }).Contains(fRole.RoleCode))
            {
                maReviews = await base.FindByAsync(mar => mar.MAID == maID && !mar.IsDraft
                 && mar.ResponderType.ResponderTypeCode == "PRSC");
            }
            else if (fRole.RoleCode == "ROLE_MODERATOR" || fRole.RoleCode == "ROLE_REVIEWER" || fRole.RoleCode=="ROLE_FOOD_REVIEWER")
            {
                maReviews = await base.FindByAsync(mar => mar.MAID == maID && !mar.IsDraft
                && (new List<string> { "APL", "PRAS", "SCAS", "TLD" }).Contains(mar.ResponderType.ResponderTypeCode));
            }
            else
            {
                maReviews = await base.FindByAsync(mar => mar.MAID == maID && !mar.IsDraft);
            }

            maReviews = maReviews.Where(mar => (statusID == null || (statusID != null && mar.SuggestedStatusID == (int)statusID)));

            return maReviews;
        }


        public async Task<bool> MAReviewCreateOrUpdateAsync(MAReview maReview, bool? changeStatus = false)
        {

            if (!string.IsNullOrEmpty(maReview.SuggestedStatusCode))
            {
                var maStatus = await _maStatusService.GetAsync(mas => mas.MAStatusCode == maReview.SuggestedStatusCode);
                maReview.SuggestedStatusID = maStatus.ID;
                maReview.SuggestedStatusCode = maStatus.MAStatusCode;
            }

            if (maReview.SuggestedStatusCode == "RTAS" || (maReview.SuggestedStatusCode == "FIR" && (bool)changeStatus))
            {
                //make previous assessments inactive
                var responderTypeCodes = new List<string> { "PRAS", "SCAS", "TLD" };
                var assessorsMAReview = await this.FindByAsync(mar => mar.IsActive && !mar.IsDraft && responderTypeCodes.Contains(mar.ResponderType.ResponderTypeCode) && mar.MAID == maReview.MAID);
                
                foreach (var mar in assessorsMAReview)
                {
                    mar.IsActive = false;
                    await base.CreateOrUpdateAsync(mar, true);
                };
                maReview.IsActive = false;
            }

            var result = await base.CreateOrUpdateAsync(maReview, true);

            if (result && !maReview.IsDraft)
            {
                if ((bool)changeStatus)
                {
                    var maLogStatus = new MAStatusLogModel
                    {
                        MAID = maReview.MAID,
                        ToStatusCode = maReview.SuggestedStatusCode,
                        Comment = maReview.Comment,
                        ChangedByUserID = maReview.ResponderID
                    };
                    await _maService.ChangeMAStatusAsync(maLogStatus);
                }
                else
                {
                    //Check if all assessors submitted their review; if yes, update ma to 'Submitted to Team Leader' status
                    var responderTypeCodes = new List<string> { "PRAS", "SCAS" };
                    var assessorsMAAssignments = await _maAssignmentService.FindByAsync(mas => responderTypeCodes.Contains(mas.ResponderType.ResponderTypeCode) && mas.MAID == maReview.MAID && mas.IsActive);
                    var assessorsMAReview = await this.FindByAsync(mar => !mar.IsDraft && responderTypeCodes.Contains(mar.ResponderType.ResponderTypeCode) && mar.MAID == maReview.MAID && mar.IsActive);

                    var grAssessorsMAReview = assessorsMAReview.GroupBy(global => global.ResponderType.ResponderTypeCode);

                    if (assessorsMAAssignments.Count() > 0 && assessorsMAAssignments.Count() == grAssessorsMAReview.Count())
                    {
                        var maLogStatus = new MAStatusLogModel
                        {
                            MAID = maReview.MAID,
                            ToStatusCode = "STL",
                            Comment = "Application is submitted to Team Leader",
                            ChangedByUserID = maReview.ResponderID
                        };
                        await _maService.ChangeMAStatusAsync(maLogStatus);

                    }
                }
            }


            return result;
        }

    }
}