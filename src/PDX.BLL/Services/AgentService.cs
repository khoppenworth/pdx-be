using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;
using DataTables.AspNet.Core;

using PDX.BLL.Services.Interfaces;
using PDX.DAL.Repositories;
using PDX.Domain.Customer;
using PDX.Domain.Account;
using PDX.Domain.Views;
using PDX.BLL.Model;
using PDX.BLL.Helpers;
using PDX.DAL.Query;
using PDX.Domain.Common;

namespace PDX.BLL.Services
{
    public class AgentService : Service<Agent>, IAgentService
    {
        private readonly IService<Address> _addressService;
        private readonly IService<UserAgent> _userAgentService;
        private readonly IUserService _userService;
        private readonly IService<UserRole> _userRoleService;
        private readonly IService<AgentSupplier> _agentSupplierService;
        private readonly IService<vwAgent> _vwAgentService;
        private readonly IStatusLogService _statusLogService;
        private readonly IDocumentService _documentService;

        private List<string> columns = new List<string> {
                "Email",
                "Name",
                "CountryName",
                "AgentTypeName"
        };

        public AgentService(IUnitOfWork unitOfWork,IService<Address> addressService, IService<UserAgent> userAgentService, IUserService userService, IService<UserRole> userRoleService,
        IService<AgentSupplier> agentSupplierService, IService<vwAgent> vwAgentService, IStatusLogService statusLogService, IDocumentService documentService) : base(unitOfWork)
        {
            _addressService = addressService;
            _userAgentService = userAgentService;
            _userService = userService;
            _userRoleService = userRoleService;
            _agentSupplierService = agentSupplierService;
            _vwAgentService = vwAgentService;
            _statusLogService = statusLogService;
            _documentService = documentService;
        }

        /// <summary>
        /// override update service to include status log
        /// </summary>
        /// <param name="agent"></param>
        /// <param name="commit"></param>
        /// <param name="modifiedBy"></param>
        /// <returns></returns>
        public override async Task<bool> UpdateAsync(Agent agent, bool commit = true, int? modifiedBy = null)
        {
            var oldAgent = await this.GetAsync(agent.ID);
            //status log
            await _statusLogService.LogStatusAsync(oldAgent, agent, nameof(agent.IsActive), agent.Remark, (int)modifiedBy);
            //Update Address Information
            var addressSaved = await _addressService.UpdateAsync(agent.Address);
            var result = addressSaved ? await base.UpdateAsync(agent, commit): false;
            return result;
        }

        public async Task<List<Agent>> GetAgentByUser(int userID)
        {
            var user = (await _userService.FindByAsync(u => u.ID == userID)).FirstOrDefault();
            var userRoles = (await _userRoleService.FindByAsync(ur => ur.User.ID == userID)).Select(ur => ur.Role);
            var agents = new List<Agent>();

            if (user != null)
            {
                if (userRoles.Any(ur => ur.RoleCode == "CSO"))
                {
                    agents = (await base.FindByAsync(f => f.IsActive && f.IsApproved)).ToList();
                }
                else if (userRoles.Any(ur => (new List<string> { "IPA", "PIPA" }).Contains(ur.RoleCode)))
                {
                    var userAgent = await _userAgentService.FindByAsync(ua => ua.UserID == userID && ua.Agent.IsActive && ua.Agent.IsApproved);
                    agents = userAgent.Select(ua => ua.Agent).ToList();
                }

            }

            return agents;
        }
        public async Task<bool> CreateUserAgent(UserAgent userAgent)
        {
            var result = true;
            var existing = await _userAgentService.GetAsync(userAgent.ID);
            if (existing != null)
            {
                result = await _userAgentService.UpdateAsync(userAgent);
            }

            else
            {
                result = await _userAgentService.CreateAsync(userAgent);
            }
            return result;
        }
        public async Task<bool> CreateOrUpdateAgentSupplier(AgentSupplier agentSupplier, int? modifiedBy = null)
        {
            var exists = await _agentSupplierService.ExistsAsync(agentSupplier);
            if (exists)
            {
                var oldAgentSupplier = await _agentSupplierService.GetAsync(agentSupplier.ID);
                await _statusLogService.LogStatusAsync(oldAgentSupplier, agentSupplier, nameof(agentSupplier.IsActive), agentSupplier.Remark, (int)modifiedBy);
                return await _agentSupplierService.UpdateAsync(agentSupplier);
            }
            var result = await _agentSupplierService.CreateAsync(agentSupplier);
            return result;
        }
        public async Task<List<Agent>> GetAgentBySupplier(int supplierID)
        {
            var agentSuppliers = await _agentSupplierService.FindByAsync(s => s.SupplierID == supplierID && s.IsActive);
            var agents = agentSuppliers.Select(s => s.Agent).ToList();
            return agents;
        }

        public async Task<List<Agent>> GetAgentsForIpermit()
        {
            var agentSuppliers = await _userAgentService.FindByAsync(s => s.IsActive && s.Agent.IsApproved);
            var agents = agentSuppliers.Select(s => s.Agent).Distinct().ToList();
            return agents;
        }

        /// <summary>
        ///  Agent list by agent type code
        /// </summary>
        /// <param name="agentTypeCode"></param>
        /// <returns></returns>
        public async Task<List<Agent>> GetAgentsByTypeCode(string agentTypeCode)
        {
            var agents = (await base.FindByAsync(agent => agent.AgentType.AgentTypeCode == agentTypeCode)).ToList();
            return agents;
        }

        /// <summary>
        /// AgentSuppiers by Supplier
        /// </summary>
        /// <param name="supplierID"></param>
        /// <returns></returns>
        public async Task<List<AgentSupplier>> GetAgentSuppliersBySupplier(int supplierID)
        {
            var agentSuppliers = await _agentSupplierService.FindByAsync(s => s.SupplierID == supplierID);
            foreach(var asp in agentSuppliers){
                asp.AgencyAgreementDoc = (await _documentService.GetDocumentAsync("AGEN", asp.ID)).FirstOrDefault();
            }
            return agentSuppliers.ToList();
        }

        /// <summary>
        /// AgentSuppiers by Agent
        /// </summary>
        /// <param name="agentID"></param>
        /// <returns></returns>
        public async Task<List<AgentSupplier>> GetAgentSuppliersByAgent(int agentID)
        {
            var agentSuppliers = await _agentSupplierService.FindByAsync(s => s.AgentID == agentID);
            foreach(var asp in agentSuppliers){
                asp.AgencyAgreementDoc = (await _documentService.GetDocumentAsync("AGEN", asp.ID)).FirstOrDefault();
            }
            return agentSuppliers.ToList();
        }

        /// <summary>
        /// AgentSuppiers by Agent and Supplier
        /// </summary>
        /// <param name="supplierID"></param>
        /// <param name="agentID"></param>
        /// <returns></returns>
        public async Task<AgentSupplier> GetAgentSuppliersBySupplierAndAgent(int supplierID, int agentID)
        {
            var agentSupplier = await _agentSupplierService.GetAsync(s => s.AgentID == agentID && s.SupplierID == supplierID && s.IsActive);
            if (agentSupplier != null)
            {
                agentSupplier.AgencyAgreementDoc = (await _documentService.GetDocumentAsync("AGEN", agentSupplier.ID)).FirstOrDefault();
            }
            return agentSupplier;
        }

        /// <summary>
        /// Agents page
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<DataTablesResult<vwAgent>> GetAgentPage(IDataTablesRequest request)
        {
            Expression<Func<vwAgent, bool>> predicate = DataTableHelper.ConstructFilter<vwAgent>(request.Search.Value, columns);
            OrderBy<vwAgent> orderBy = new OrderBy<vwAgent>(qry => qry.OrderBy(e => e.Name));
            var response = await _vwAgentService.GetPageAsync(request, predicate, orderBy.Expression);
            return response;
        }

        /// <summary>
        /// Get AgentLevel by UserID
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="supplierID"></param>
        /// <returns></returns>
        public async Task<AgentLevel> GetAgentLevelByUserAndSupplier(int userID, int supplierID)
        {
            var userAgent = await _userAgentService.GetAsync(ua => ua.UserID == userID);
            var agentSupplier = await GetAgentSuppliersBySupplierAndAgent(supplierID, userAgent.AgentID);
            return agentSupplier?.AgentLevel;
        }
    }
}