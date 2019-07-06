using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PDX.BLL.Model;
using PDX.BLL.Services.Interfaces;
using PDX.DAL.Helpers;
using PDX.DAL.Repositories;
using PDX.Domain.Catalog;
using PDX.Domain.Common;
using PDX.Domain.License;

namespace PDX.BLL.Services {
    public class ChecklistService : Service<Checklist>, IChecklistService {
        private readonly IService<SubmoduleChecklist> _submoduleChecklistService;
        private readonly IService<MAChecklist> _maChecklistService;
        private readonly IService<MAReview> _maReviewService;
        private readonly IService<Module> _moduleService;
        private readonly IService<Submodule> _submoduleService;
        private readonly IService<ResponderType> _respoderTypeService;
        private readonly IUserService _userService;
        private readonly IService<MA> _maService;
        public ChecklistService (IUnitOfWork unitOfWork, IService<SubmoduleChecklist> submoduleChecklistService,
            IService<Submodule> submoduleService, IService<MAChecklist> maChecklistService, IService<MAReview> maReviewService,
            IService<ResponderType> respoderTypeService, IService<Module> moduleService, IUserService userService, IService<MA> maService) : base (unitOfWork) {
            _submoduleChecklistService = submoduleChecklistService;
            _maChecklistService = maChecklistService;
            _maReviewService = maReviewService;
            _moduleService = moduleService;
            _submoduleService = submoduleService;
            _respoderTypeService = respoderTypeService;
            _userService = userService;
            _maService = maService;
        }

        public async override Task<IEnumerable<Checklist>> GetAllAsync (bool activeOnly = false, Func<IQueryable<Checklist>, IOrderedQueryable<Checklist>> orderBy = null) {
            var checklist = await base.GetAllAsync (activeOnly, x => x.OrderBy (c => c.Label));
            var hChecklist = BuildHierarchy (checklist, 0, null);
            return hChecklist;
        }

        public async Task<IList<Checklist>> GetChecklistByTypeAsync (int checklistTypeID) {
            var checklists = (await this.GetAllAsync ()).Where (ch => ch.ChecklistTypeID == checklistTypeID).ToList ();
            return checklists;
        }

        public async Task<IEnumerable<Checklist>> GetChecklistByMAAsync (IEnumerable<MAChecklist> maChecklist, string submoduleCode, string checklistTypeCode = null, bool? isSra = false) {
            var submodule = await _submoduleService.GetAsync (s => s.SubmoduleCode == submoduleCode);
            var checklistIDs = (await _submoduleChecklistService.FindByAsync (sc => sc.SubmoduleID == submodule.ID)).Select (s => s.ChecklistID);
            var checklists = await this.FindByAsync (sc => checklistIDs.Contains (sc.ID) && sc.IsSRA == isSra && (checklistTypeCode == null || (!string.IsNullOrEmpty (checklistTypeCode) &&
                sc.ChecklistType.ChecklistTypeCode == checklistTypeCode)));

            //Nullify MA and Checklist Navigation properties to avoid StackOverflowException
            foreach (var mac in maChecklist) {
                mac.MA = null;
                mac.Checklist = null;
            }

            //Assign MAChecklist for each checklist
            foreach (var checklist in checklists) {
                var filteredMAChecklist = maChecklist.Where (ch => ch.ChecklistID == checklist.ID).ToList ();
                checklist.MAChecklists = filteredMAChecklist;
            }

            var hChecklist = BuildHierarchy (checklists, 0, null);
            return hChecklist;
        }

        public async Task<IEnumerable<Checklist>> GetChecklistByMAAsync (int maID, string submoduleCode, int userID, bool? isSra = false) {
            var user = await _userService.GetAsync (userID);
            var role = user.Roles.FirstOrDefault ();
            string checkListTypeCode = null;

            if ((new List<string> { "CSD", "CSO", "CST", "IPA","ROLE_FOOD_REVIEWER" }).Contains (role.RoleCode)) {
                checkListTypeCode = "PSCR";
            } else if ((new List<string> { "ROLE_REVIEWER", "ROLE_HEAD", "ROLE_MODERATOR","ROLE_FOOD_REVIEWER" }).Contains (role.RoleCode)) {
                checkListTypeCode = "RVIW";
            }

            var maChecklists = await _maChecklistService.FindByAsync (mac => mac.MAID == maID);
            return await GetChecklistByMAAsync (maChecklists, submoduleCode, checkListTypeCode, isSra);
        }

        public async Task<IEnumerable<Checklist>> GetChecklistByMAAsync (int maID, string submoduleCode, string checklistTypeCode = null, bool? isSra = false) {
            var maChecklists = await _maChecklistService.FindByAsync (mac => mac.MAID == maID && mac.Checklist.IsSRA == isSra && (checklistTypeCode == null || (!string.IsNullOrEmpty (checklistTypeCode) &&
                mac.Checklist.ChecklistType.ChecklistTypeCode == checklistTypeCode)));
            return await GetChecklistByMAAsync (maChecklists, submoduleCode, checklistTypeCode, isSra);
        }

        public async Task<IList<Checklist>> GetChecklistByModuleAsync (string moduleCode, string checklistTypeCode, bool? isSra = false) {
            var module = await _moduleService.GetAsync (m => m.ModuleCode == moduleCode);
            var sChecklistsIDs = (await _submoduleChecklistService.FindByAsync (sc => sc.Submodule.ModuleID == module.ID)).Select (s => s.ChecklistID);
            var hChecklists = await GetChecklistBySubmoduleAsync (sChecklistsIDs, checklistTypeCode, isSra);
            return hChecklists;
        }

        public async Task<IList<Checklist>> GetChecklistBySubmoduleAsync (string submoduleCode, string checklistTypeCode, bool? isSra = false, bool? isBothVariationType = false) {
            var sChecklistsIDs= new List<int>();

            if ((bool) isBothVariationType && submoduleCode == "VMAJ") {
                var submodules = (await _submoduleService.FindByAsync (s => (new List<string> () { "VMAJ", "VMIN" }).Contains (s.SubmoduleCode))).Select (s => s.ID);
                 sChecklistsIDs = (await _submoduleChecklistService.FindByAsync (sc => submodules.Contains (sc.SubmoduleID))).Select (s => s.ChecklistID).Distinct().ToList();

            } else {
                var submodule = await _submoduleService.GetAsync (s => s.SubmoduleCode == submoduleCode);
                 sChecklistsIDs = (await _submoduleChecklistService.FindByAsync (sc => sc.SubmoduleID == submodule.ID))?.Select (s => s.ChecklistID)?.ToList();

            }
            var hChecklists = await GetChecklistBySubmoduleAsync (sChecklistsIDs, checklistTypeCode, isSra);
            return hChecklists;
        }

        public async Task<IEnumerable<MAReviewModel>> GetMAReviewWithChecklist (int maID, string submoduleCode) {
            //Filter Review type checklists
            var ma = await _maService.GetAsync (maID);
            var maChecklists = (await _maChecklistService.FindByAsync (mac => mac.MAID == maID && mac.Checklist.ChecklistType.ChecklistTypeCode == "RVIW"))
                .GroupBy (g => g.ResponderType.ResponderTypeCode,
                    (key, val) => new { ResponderTypeCode = key, MAChecklists = val.ToList () });

            var maReviews = (await _maReviewService.FindByAsync (mar => mar.MAID == maID && mar.IsActive)).OrderByDescending (r => r.CreatedDate).GroupBy (g => g.ResponderType.ResponderTypeCode,
                (key, val) => new { ResponderTypeCode = key, MAReview = val.FirstOrDefault () });

            var variationSummary = (await _maReviewService.FindByAsync (req => req.MAID == maID && req.IsActive && req.SuggestedStatus.MAStatusCode == "RQST" && req.ResponderType.ResponderTypeCode == "APL"))?.OrderByDescending (or => or.CreatedDate)?.FirstOrDefault ();


            //Get All responder types
            var responderTypes = await _respoderTypeService.GetAllAsync ();

            var maReviewModels = new List<MAReviewModel> ();
            foreach (var responderType in responderTypes) {
                var filteredMAChecklist = maChecklists.FirstOrDefault (m => m.ResponderTypeCode == responderType.ResponderTypeCode);
                var filteredMAReview = maReviews.FirstOrDefault (m => m.ResponderTypeCode == responderType.ResponderTypeCode);

                var maReviewModel = new MAReviewModel {
                    ResponderType = responderType,
                    ReviewChecklists = await GetChecklistByMAAsync (filteredMAChecklist?.MAChecklists ?? new List<MAChecklist> (), submoduleCode, "RVIW", ma.IsSRA),
                    MAReview = filteredMAReview != null ? filteredMAReview.MAReview : null
                };
                maReviewModels.Add (maReviewModel);
            }
            if (variationSummary != null) {
                maReviewModels.Add (new MAReviewModel {
                    ResponderType = variationSummary?.ResponderType,
                        ReviewChecklists = null,
                        MAReview = variationSummary
                });
            }

            return maReviewModels;
        }

        public async Task<IEnumerable<Checklist>> GetPrescreeningDeficiency (int maID) {
            IList<Checklist> deficiency = new List<Checklist> ();

            var maChecklists = (await _maChecklistService.FindByAsync (mac => mac.MAID == maID && mac.Checklist.ChecklistType.ChecklistTypeCode == "PSCR"));
            var appChecklists = maChecklists.Where (mac => mac.ResponderType.ResponderTypeCode == "APL");
            var preChecklists = maChecklists.Where (mac => mac.ResponderType.ResponderTypeCode == "PRSC");

            if (appChecklists.Count () == preChecklists.Count ()) {
                foreach (var appChecklist in appChecklists) {
                    var preChecklist = preChecklists.FirstOrDefault (f => f.ChecklistID == appChecklist.ChecklistID);
                    if (preChecklist?.Option.Name == "No" && appChecklist?.Option.Name != "No") {
                        deficiency.Add (appChecklist.Checklist);
                    }
                }
            }

            return deficiency;
        }

        public async override Task<bool> CreateOrUpdateAsync (Checklist checklist, bool commit = true, Expression<Func<Checklist, bool>> resolveBy = null) {
            var tChecklist = checklist.NullifyForeignKeys ();
            var result = await this.SaveNestedChecklist (tChecklist, commit, null, true, resolveBy);
            return result;
        }

        public async override Task<bool> CreateOrUpdateAsync (IEnumerable<Checklist> checklists, bool commit = true, params string[] resolvingProperties) {
            var result = true;
            foreach (var checklist in checklists) {
                result = result && (await this.CreateOrUpdateAsync (checklist, commit, c => c.RowGuid == checklist.RowGuid));
            }
            return result;
        }

        public async Task<IEnumerable<Checklist>> GetChecklistBySubmoduleType (string checklistTypeCode, string submoduleTypeCode) {
            var checklist = await base.FindByAsync (ch => ((checklistTypeCode != null && ch.ChecklistType.ChecklistTypeCode == checklistTypeCode) || checklistTypeCode == null) &&
                ((submoduleTypeCode != null && ch.SubmoduleType.SubmoduleTypeCode == submoduleTypeCode) || submoduleTypeCode == null),x => x.OrderBy(c => c.Label));
            var hChecklist = BuildHierarchy (checklist, 0, null);
            return hChecklist;
        }

        #region Private Helpers Methods

        private async Task<bool> SaveNestedChecklist (Checklist checklist, bool commit = true, int? parentChecklistID = null, bool accumulator = true, Expression<Func<Checklist, bool>> resolveBy = null) {
            checklist.ParentChecklistID = parentChecklistID;
            accumulator = accumulator && (await base.CreateOrUpdateAsync (checklist, commit, resolveBy));

            foreach (var child in checklist.Children) {
                var tchild = child.NullifyForeignKeys ();
                await SaveNestedChecklist (tchild, commit, checklist.ID, accumulator);
            }

            return accumulator;
        }

        /// <summary>
        /// Build Hierarchy of Checklist in recurrsive approach
        /// </summary>
        /// <param name="list"></param>
        /// <param name="depth"></param>
        /// <param name="parentChecklistID"></param>
        /// <returns></returns>
        private IEnumerable<Checklist> BuildHierarchy (IEnumerable<Checklist> list, int depth = 0, int? parentChecklistID = null) {
            var hList = from li in list
            where li.ParentChecklistID == parentChecklistID
            select new Checklist {
            ID = li.ID,
            IsActive = li.IsActive,
            CreatedDate = li.CreatedDate,
            ModifiedDate = li.ModifiedDate,
            RowGuid = li.RowGuid,

            Name = li.Name,
            ChecklistTypeID = li.ChecklistTypeID,
            IsSRA = li.IsSRA,
            AnswerTypeID = li.AnswerTypeID,
            Label = li.Label,
            Priority = li.Priority,
            ParentChecklistID = li.ParentChecklistID,
            OptionGroupID = li.OptionGroupID,

            ChecklistType = li.ChecklistType,
            AnswerType = li.AnswerType,
            OptionGroup = li.OptionGroup,
            SubmoduleType = li.SubmoduleType,
            SubmoduleTypeID = li.SubmoduleTypeID,
            Depth = depth,
            MAChecklists = li.MAChecklists,
            Children = BuildHierarchy (list, depth + 1, li.ID)
            };

            return hList.ToList ();

        }

        private async Task<IList<Checklist>> GetChecklistBySubmoduleAsync (IEnumerable<int> sChecklistsIDs, string checklistTypeCode, bool? isSra = false) {
            var checklists = (await this.FindByAsync (ch => ch.IsActive &&
                (checklistTypeCode == null || (checklistTypeCode != null && ch.ChecklistType.ChecklistTypeCode == checklistTypeCode)) &&
                sChecklistsIDs.Contains (ch.ID) &&
                (isSra == null || (isSra != null && ch.IsSRA == isSra)), x => x.OrderBy (c => c.Label))).Distinct ();
            var hChecklist = BuildHierarchy (checklists, 0, null);
            return hChecklist.OrderBy (ch => ch.Priority).ToList ();
        }

        #endregion
    }
}