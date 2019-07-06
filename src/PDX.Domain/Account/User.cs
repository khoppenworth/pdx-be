using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Newtonsoft.Json;
using PDX.Domain.Common;
using PDX.Domain.Helpers;

namespace PDX.Domain.Account
{
    [Table("user", Schema = "account")]
    public class User : BaseEntity
    {
        public User()
        {
            this.UserRoles = new Collection<UserRole>();
            this.UserAgents = new Collection<UserAgent>();
        }

        [Required]
        [Column("first_name")]
        public string FirstName { get; set; }

        [Required]
        [Column("last_name")]
        public string LastName { get; set; }

        [Required]
        [Column("user_name")]
        public string Username { get; set; }

        [Required]
        [Column("password")]
        public string Password { get; set; }

        [Column("email")]
        public string Email { get; set; }

        [Column("phone")]
        [MaxLength(100)]
        public string Phone {get; set; }
        [Column("phone2")]
        [MaxLength(100)]
        public string Phone2 { get; set; }
        
        [Column("phone3")]
        [MaxLength(100)]
        public string Phone3 { get; set; }

        [Column("last_login")]
        public Nullable<System.DateTime> LastLogin { get; set; }

        [Column("user_type_id")]
        public int UserTypeID { get; set; }

        [NavigationProperty]
        [ForeignKey("UserTypeID")]
        public virtual UserType UserType { get; set; }

        [NotMapped]
        public virtual ICollection<Role> Roles { get; set; }

        [NavigationProperty]
        public virtual ICollection<UserRole> UserRoles { get; set; }
        //[JsonIgnore]
        [NavigationProperty]
        public virtual ICollection<UserAgent> UserAgents { get; set; }

        [NotMapped]
        public string AgentName { get { return this.UserAgents?.FirstOrDefault()?.Agent?.Name; } }
    }
}
