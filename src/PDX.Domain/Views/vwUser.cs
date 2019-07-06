using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace PDX.Domain.Views {
    [Table ("vwuser", Schema = "account")]
    public class vwUser : BaseEntity {

        [Column ("first_name")]
        public string FirstName { get; set; }

        [Column ("last_name")]
        public string LastName { get; set; }

        [Column ("user_name")]
        public string Username { get; set; }

        [Column ("password")]
        public string Password { get; set; }

        [Column ("email")]
        public string Email { get; set; }

        [Column ("phone")]
        public string Phone { get; set; }

        [Column ("last_login")]
        public Nullable<System.DateTime> LastLogin { get; set; }

        [Column ("user_type_id")]
        public int UserTypeID { get; set; }

        [Column ("user_type_name")]
        public string UserTypeName { get; set; }

        [Column ("user_type_code")]
        public string UserTypeCode { get; set; }

        [Column ("agent_name")]
        public string AgentName { get; set; }

        [NotMapped]
        public string Status { get { return this.IsActive? "Active": "InActive"; } }
    }
}