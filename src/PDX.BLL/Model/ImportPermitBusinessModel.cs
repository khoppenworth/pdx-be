using System.Collections.Generic;
using System.Reflection.Metadata;

namespace PDX.BLL.Model
{
    public class ImportPermitBusinessModel
    {
        public Domain.Procurement.ImportPermit ImportPermit { get; set; }
        public IList<Document> Documents { get; set; }
        public IList<Domain.Document.Document> UploadedDocuments { get; set; }
        public bool IsDraft { get; set; } = false;
        public bool TermsAndConditions  { get; set; }
        public string SubmoduleCode { get; set; }
        public System.Guid Identifier { get; set; }
        public ImportPermitStatuses CurrentStatusCode { get; set; }
        public string SubModuleTypeCode { get; set; }
    }
}