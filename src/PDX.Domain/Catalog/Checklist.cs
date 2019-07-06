using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PDX.Domain.Common;
using PDX.Domain.Helpers;
using PDX.Domain.License;

namespace PDX.Domain.Catalog {
    [Table ("checklist", Schema = "catalog")]
    public class Checklist : LookUpEntity {
        [Column ("checklist_type_id")]
        public int ChecklistTypeID { get; set; }

        [Column ("is_sra")]
        public bool IsSRA { get; set; }

        [Column ("parent_checklist_id")]
        public int? ParentChecklistID { get; set; }

        [Column ("answer_type_id")]
        public int? AnswerTypeID { get; set; }

        [Column ("label")]
        public string Label { get; set; }

        [Column ("priority")]
        public int? Priority { get; set; }

        [Column ("option_group_id")]
        public int? OptionGroupID { get; set; }

        [Column ("is_fast_tracking")]
        public bool? IsFastTracking { get; set; }

        [Column ("submodule_type_id")]
        public int? SubmoduleTypeID { get; set; }

        [NavigationProperty]
        [ForeignKey ("SubmoduleTypeID")]
        public virtual SubmoduleType SubmoduleType { get; set; }

        [NavigationProperty]
        [ForeignKey ("ChecklistTypeID")]
        public virtual ChecklistType ChecklistType { get; set; }

        [NavigationProperty]
        [ForeignKey ("AnswerTypeID")]
        public virtual AnswerType AnswerType { get; set; }

        [NavigationProperty]
        [ForeignKey ("OptionGroupID")]
        public virtual OptionGroup OptionGroup { get; set; }

        [NotMapped]
        public virtual IEnumerable<Checklist> Children { get; set; }

        [NotMapped]
        public int Depth { get; set; }

        [NotMapped]
        public virtual ICollection<MAChecklist> MAChecklists { get; set; }
    }
}