using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PDX.Domain.Account;

namespace PDX.Domain.Public
{
    [Table("notification", Schema = "public")]
    public class Notification:BaseEntity
    {
        private string _data;

        [Required]
        [Column("medium")]
        [MaxLength(100)]
        public string Medium {get; set; }

        [Column("user_id")]
        public int UserID { get; set; }   

        [Column("is_read")]
        public bool IsRead { get; set; }
         
        
        [ForeignKey("UserID")]
        public virtual User User { get; set; }


        [NotMapped]
        public JObject Data
        {
            get
            {
                return JsonConvert.DeserializeObject<JObject>(string.IsNullOrEmpty(_data) ? "{}" : _data);
            }
            set
            {
                _data = value.ToString();
            }
        }
    }
}