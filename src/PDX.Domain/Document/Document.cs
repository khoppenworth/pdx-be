using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PDX.Domain.Account;
using PDX.Domain.Helpers;

namespace PDX.Domain.Document
{
    [Table("document", Schema = "document")]
    public class Document:BaseEntity
    {
        [Column("module_document_id")]
        public int ModuleDocumentID{get;set;}

        [Required]
        [Column("file_path")]
        public string FilePath{get;set;}

        [Required]
        [Column("original_file_name")]
        public string OriginalFileName{get;set;}
        [Required]
        [Column("file_type")]
        public string FileType{get;set;}

        [Column("reference_id")]
        public int ReferenceID{get;set;}

        [Column("created_by")]
        public int CreatedBy{get;set;}

        [Column("updated_by")]
        public int UpdatedBy{get;set;}

        [ForeignKey("ModuleDocumentID")]
        [NavigationProperty]
        public virtual ModuleDocument ModuleDocument{get;set;}

        [ForeignKey("CreatedBy")]
        public virtual User CreatedByUser{get;set;}

        [ForeignKey("UpdatedBy")]
        public virtual User UpdatedByUser{get;set;}

    }
}