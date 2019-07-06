using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PDX.Domain.Common;
using PDX.Domain.Helpers;

namespace PDX.Domain.Catalog
{
    [Table("submodule_checklist", Schema = "catalog")]
    public class SubmoduleChecklist : BaseEntity
    {
        [Column("submodule_id")]
        public int SubmoduleID { get; set; }

        [Column("checklist_id")]
        public int ChecklistID { get; set; }

        [NavigationProperty]
        [ForeignKey("SubmoduleID")]
        public virtual Submodule Submodule { get; set; }

        [NavigationProperty]
        [ForeignKey("ChecklistID")]
        public virtual Checklist Checklist { get; set; }

    }
}