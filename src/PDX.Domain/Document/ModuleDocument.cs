using System.ComponentModel.DataAnnotations.Schema;
using PDX.Domain.Common;
using PDX.Domain.Helpers;

namespace PDX.Domain.Document
{
    [Table("module_document", Schema = "document")]
    public class ModuleDocument:BaseEntity
    {
         [Column("submodule_id")]
         public int SubmoduleID{get;set;}

         [Column("document_type_id")]
         public int DocumentTypeID{get;set;}

         [Column("is_required")]
         public bool IsRequired{get;set;}
         [Column("is_sra")]
         public bool? IsSRA{get;set;}
         
         [NavigationProperty]
         [ForeignKey("DocumentTypeID")]
         public virtual DocumentType DocumentType{get;set;}

         [NavigationProperty]
         [ForeignKey("SubmoduleID")]
         public virtual Submodule Submodule{get;set;}

    }
}