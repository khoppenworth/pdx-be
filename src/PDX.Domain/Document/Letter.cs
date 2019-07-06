using System.ComponentModel.DataAnnotations.Schema;
using PDX.Domain.Helpers;

namespace PDX.Domain.Document
{
    [Table("letter", Schema = "document")]
    public class Letter:BaseEntity
    {
        [Column("module_document_id")]
        public int ModuleDocumentID{get;set;}

        public string Title { get; set; }

        public string Body { get; set; }

        public string Footer { get; set; }
        public string OtherText { get; set; }

        [ForeignKey("ModuleDocumentID")]
        [NavigationProperty]
        public virtual ModuleDocument ModuleDocument{get;set;}
    }
}