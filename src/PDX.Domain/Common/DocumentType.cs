using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace PDX.Domain.Common
{
    [Table("document_type", Schema = "common")]
    public class DocumentType: LookUpBaseEntity
    {
        [Column("is_system_generated")]
        public bool IsSystemGenerated{get;set;}

        [Column("is_uploaded_later")]
        public bool IsUploadedLater{get;set;}

        [Column("document_type_code")]
        public string DocumentTypeCode { get; set; }

        [Column("is_dossier")]
        public bool IsDossier {get;set;}

        [Column("parent_document_type_id")]
        public Nullable<int> ParentDocumentTypeID { get; set; }
    }
}