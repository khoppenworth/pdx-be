using PDX.Domain.License;
using PDX.Domain.Commodity;
using PDX.Domain.Document;
using System.Collections.Generic;
using PDX.Domain.Catalog;

namespace PDX.BLL.Model
{
    public class MABusinessModel
    {
        public MA MA { get; set; }        
        public Product Product { get; set; }   
        public IEnumerable<MDModelSize> Products { get; set; }  
        public IEnumerable<Document> Documents { get; set; }
        public IEnumerable<Domain.Document.Document> UploadedDocuments { get; set; }
        public IEnumerable<Document> Dossiers { get; set; }
        public IEnumerable<Checklist> Checklists { get; set; }
        public string SubmoduleCode { get; set; }
        public string SubmoduleTypeCode { get; set; }
        public System.Guid Identifier { get; set; }
        public string Comment { get; set; }
        public string BrandName { get; set; }
        public bool IsLabResultUploaded {get;set;} = false;
        public int FIRGeneratedCount {get;set;} = 0;
    }
}