using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PDX.Domain.Common
{
    [Table("system_setting", Schema = "settings")]
    public class SystemSetting : BaseEntity
    {
        [Required]
        [Column("name")]
        public string Name { get; set; }
        [Required]
        [Column("value")]
        public string Value { get; set; }

        [Column("description")]
        [MaxLength(1000)]
        public string Description { get; set; }
        [Required]
        [Column("system_setting_code")]
        public string SystemSettingCode { get; set; }
        [Column("data_type")]
        [MaxLength(100)]
        public string DataType { get; set; }

        [NotMapped]
        public object ValueObj
        {
            get
            {
                object obj = Value;
                switch (DataType)
                {
                    case "Integer":
                    case "Float":
                    case "Double":                    
                    case "Decimal":
                    case "Number":
                    case "Numeric":
                        obj = decimal.Parse(Value);
                        break;
                    case "Boolean":
                        obj = bool.Parse(Value);
                        break;
                    case "Date":
                    case "DateTime":
                        obj = DateTime.Parse(Value);
                    break;
                }

                return obj;
            }
        }
    }
}