using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.NodeServices;
using PDX.API.Model.Ipermit;
using PDX.BLL.Model;
using PDX.DAL.Reporting.Models;
using PDX.Domain.Procurement;

namespace PDX.API.Services.Interfaces
{
    public interface IGenerateDocuments
    {
        Task GeneratePDFDocument(string filePath, ImportStatusLog ipermit, INodeServices nodeServices,string documenttypeCode,string submoduleCode,ImportPermit iipermit);
        Task<Domain.Document.Document> GenerateRegistrationPDFDocument(INodeServices nodeServices, int maId, string statusCode,int userId,string comment=null);
        Task<string> GenerateReportDocument(INodeServices nodeServices, int reportID,IList<Filter> filters=null);
    }
}