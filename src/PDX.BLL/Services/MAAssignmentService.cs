using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PDX.BLL.Model;
using PDX.BLL.Services.Interfaces;
using PDX.BLL.Services.Notification;
using PDX.DAL.Repositories;
using PDX.Domain.Account;
using PDX.Domain.Common;
using PDX.Domain.License;

namespace PDX.BLL.Services {
    public class MAAssignmentService : Service<MAAssignment>, IMAAssignmentService {
        private readonly IUserService _userService;
        private readonly IService<ResponderType> _responderTypeService;
        private readonly IMAService _maService;
        private readonly NotificationFactory _notificationFactory;
        public MAAssignmentService (IUnitOfWork unitOfWork, IUserService userService, IService<ResponderType> responderTypeService, IMAService maService,
            NotificationFactory notificationFactory) : base (unitOfWork) {
            _userService = userService;
            _responderTypeService = responderTypeService;
            _maService = maService;
            _notificationFactory = notificationFactory;
        }

        public override async Task<bool> CreateAsync (MAAssignment maAssignment, bool commit = true, int? createdBy = null) {
            var previousMaAssignments = await base.FindByAsync (maa => maa.MAID == maAssignment.MAID && maa.ResponderTypeID == maAssignment.ResponderTypeID);

            if (previousMaAssignments.Count (maa => maa.AssignedToUserID == maAssignment.AssignedToUserID && maa.IsActive) > 0 && maAssignment.IsActive) {
                return true;
            }

            foreach (var previousMaAssignment in previousMaAssignments) {
                previousMaAssignment.IsActive = false;
                await base.UpdateAsync (previousMaAssignment, commit);
            }
            var result = await base.CreateAsync (maAssignment, commit);
            if (result) {
                await Notify (maAssignment);
            }
            return result;
        }

        public async Task<ResponderType> GetResponderType (int maID, int userID) {
            var maAssignment = (await base.GetAsync (maa => maa.MAID == maID && maa.AssignedToUserID == userID && maa.IsActive));
            if (maAssignment == null) {
                var user = await _userService.GetAsync (us => us.ID == userID);
                var responderTypeCode = string.Empty;

                if (user.Roles.Any (r => r.RoleCode == "ROLE_MODERATOR")) //Registration Team Leader
                {
                    responderTypeCode = "TLD";
                }

                if (user.Roles.Any (r => r.RoleCode == "IPA")) //Applicant
                {
                    responderTypeCode = "APL";
                }

                //Get Team leader or Applicant ResponderType 
                var responderType = await _responderTypeService.GetAsync (rt => rt.ResponderTypeCode == responderTypeCode);
                return responderType;
            }
            return maAssignment.ResponderType;
        }

        public async Task<IList<MAAssignment>> GetMAAssignments (int maID) {
            var maAssignments = await base.FindByAsync (maa => maa.MAID == maID);
            return maAssignments.ToList ();
        }

        public async Task<object> GetGroupedMAAssignments (int maID) {
            var maAssignments = (await base.FindByAsync (maa => maa.MAID == maID && maa.IsActive && maa.MA.IsActive));
            var groupedMAAssignments = new {
                CSO = new List<MAAssignment> (),
                Reviewers = new List<MAAssignment> ()
            };

            foreach (var mas in maAssignments) {
                // var user = await _userService.GetAsync (us => us.ID == mas.AssignedToUserID);
                if (mas.ResponderType.ResponderTypeCode == "PRSC") {
                    groupedMAAssignments.CSO.Add (mas);
                } else if ((new List<string> { "PRAS", "SCAS" }).Contains (mas.ResponderType.ResponderTypeCode)) {
                    groupedMAAssignments.Reviewers.Add (mas);
                }
            }

            return groupedMAAssignments;
        }

        private async Task Notify (MAAssignment maAssignment) {
            var ma = await _maService.GetAsync (maAssignment.MAID);
            var assignedUser = await _userService.GetAsync (maAssignment.AssignedToUserID);
            var users = new List<User> () { assignedUser };
            var pushNotifier = _notificationFactory.GetNotification (NotificationType.PUSHNOTIFICATION);
            await pushNotifier.Notify (users, new { body = string.Format ("Market Authorization with number of {0} has been assigned to you.", ma.MANumber), subject = "Market Authorization Assigned" }, "IPSC");
        }
    }
}