using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace PDX.Domain.Account
{
    [Table("user_login", Schema = "account")]
    public class UserLogin : BaseEntity
    {
        [Column("user_id")]
        public int UserID { get; set; }
        [Column("device_type")]
        public string DeviceType { get; set; }
        [Column("os")]
        public string OS { get; set; }
        [Column("os_version")]
        public string OSVersion { get; set; }
        [Column("browser")]
        public string Browser { get; set; }
        [Column("browser_version")]
        public string BrowserVersion { get; set; }
        [Column("login_time")]
        public DateTime LoginTime { get; set; } = System.DateTime.UtcNow;
        [Column("logout_time")]
        public DateTime? LogoutTime { get; set; }
        [Column("logout_reason")]
        public string LogoutReason { get; set; }
    }
}