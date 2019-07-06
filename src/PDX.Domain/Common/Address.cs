using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PDX.Domain.Helpers;

namespace PDX.Domain.Common
{
    [Table("address", Schema = "common")]
    public class Address:BaseEntity
    {
        [Column("country_id")]
        public int CountryID { get; set; }

        [Column("line1")]
        [MaxLength(500)]
        public string Line1 { get; set; }

        [Column("line2")]
        [MaxLength(500)]
        public string Line2 { get; set; }

        [Column("region")]
        [MaxLength(100)]
        public string Region { get; set; }

        [Column("city")]
        [MaxLength(100)]
        public string City { get; set; }

        [Column("sub_city")]
        [MaxLength(100)]
        public string SubCity { get; set; }

        [Column("woreda")]
        [MaxLength(100)]
        public string Woreda { get; set; }

        [Column("kebele")]
        [MaxLength(100)]
        public string Kebele { get; set; }

        [Column("house_number")]
        [MaxLength(100)]
        public string HouseNumber { get; set; }

        [Column("street_name")]
        [MaxLength(100)]
        public string StreetName { get; set; }

        [Column("place_name")]
        [MaxLength(100)]
        public string PlaceName { get; set; }

        [Column("zip_code")]
        [MaxLength(100)]
        public string ZipCode { get; set; }

        [NavigationProperty]
        [ForeignKey("CountryID")]
        public virtual Country Country { get; set; }
    }
}