using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PDX.Domain
{
    public class BaseEntity
    {
        public BaseEntity()
        {
            IsActive = true;
            CreatedDate = ModifiedDate = DateTime.UtcNow;
        }
        
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int ID { get; set; }

        [Column("is_active")]
        public bool IsActive { get; set; }

        [Column("created_date")]
        public DateTime CreatedDate { get; set; }

        [Column("modified_date")]
        public DateTime ModifiedDate { get; set; }
        
         [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("rowguid")]
        public Guid RowGuid { get; set; }
    }
}
