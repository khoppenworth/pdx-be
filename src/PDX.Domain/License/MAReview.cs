using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using PDX.Domain.Account;
using PDX.Domain.Common;
using PDX.Domain.Helpers;
using PDX.Domain.License;

namespace PDX.Domain.License {
    [Table ("ma_review", Schema = "license")]
    public class MAReview : BaseEntity {
        [Column ("ma_id")]
        public int MAID { get; set; }

        [Column ("responder_id")]
        public int ResponderID { get; set; }

        [Column ("responder_type_id")]
        public int ResponderTypeID { get; set; }

        [Column ("suggested_status_id")]
        public int? SuggestedStatusID { get; set; }

        [Required]
        [Column ("comment")]
        public string Comment { get; set; }

        [Column ("is_draft")]
        public bool IsDraft { get; set; }

        [Column ("fir_due_date")]
        public Nullable<DateTime> FIRDueDate { get; set; }

        [JsonIgnore]
        [ForeignKey ("MAID")]
        public virtual MA MA { get; set; }

        [ForeignKey ("SuggestedStatusID")]
        [NavigationProperty]
        public virtual MAStatus SuggestedStatus { get; set; }

        [ForeignKey ("ResponderID")]
        [NavigationProperty]
        public virtual User Responder { get; set; }

        [ForeignKey ("ResponderTypeID")]
        [NavigationProperty]
        public virtual ResponderType ResponderType { get; set; }

        [NotMapped]
        public string SuggestedStatusCode { get; set; }
    }
}