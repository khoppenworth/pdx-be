using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace PDX.BLL.Services.Interfaces
{
    public interface IDocumentService:IService<Domain.Document.Document>
    {
         Task<Domain.Document.Document> CreateDocumentAsync(PDX.BLL.Model.Document document);
         Task<List<Domain.Document.Document>> GetDocumentAsync(string submoduleCode, int referenceID);
         Task<List<Domain.Document.Document>> GetDocumentAsync(int referenceID);
         Task<bool> DetachDocumentReferenceAsync(int referenceID, bool commit=true);
         Task<string> ReadDocumentAsBase64Async(int documentID);
         Task<PhysicalFileResult> ReadPhysicalFile(int documentID);
         Task<PhysicalFileResult> ReadPhysicalFile(string relativeFilePath, string fileType);
         Task<PhysicalFileResult> ReadPhysicalFileByPermission(int documentID,int userID,string permission);
         Task<PhysicalFileResult> PrintPhysicalFile(int documentID, int userID);
         Task<byte[]> ReadFile(int documentID);
    }
}