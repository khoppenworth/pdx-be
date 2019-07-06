using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PDX.Domain.Common;
using PDX.Domain.Helpers;

namespace PDX.Domain.Report
{
    [Table("report", Schema = "report")]
    public class Report : BaseEntity
    {
        private string _columnDefinitions;
        private string _filterColumns;

        [Required]
        [Column("name")]
        [MaxLength(500)]
        public string Name { get; set; }

        [Column("description")]
        [MaxLength(1000)]
        public string Description { get; set; }

        [Required]
        [Column("title")]
        [MaxLength(500)]
        public string Title { get; set; }

        [Column("report_type_id")]
        public int ReportTypeID { get; set; }

        [Required]
        [Column("query")]
        public string Query { get; set; }

        [Column("series_columns")]
        public string SeriesColumns { get; set; }

        [Column("priority")]
        public int? Priority { get; set; }

        [Column("width")]
        public int? Width { get; set; }

        [Column("max_rows")]
        public int? MaxRows { get; set; }
        [Column("is_mobile")]
        public bool IsMobile { get; set; }

        [NavigationProperty]
        [ForeignKey("ReportTypeID")]
        public virtual ReportType ReportType { get; set; }

        [NotMapped]
        public IList<JObject> ColumnDefinitions
        {
            get
            {
                return JsonConvert.DeserializeObject<IList<JObject>>(string.IsNullOrEmpty(_columnDefinitions) ? "[]" : _columnDefinitions);
            }
            set
            {
                _columnDefinitions = value.ToString();
            }
        }

        [NotMapped]
        public IList<JObject> FilterColumns
        {
            get
            {
                return JsonConvert.DeserializeObject<IList<JObject>>(string.IsNullOrEmpty(_filterColumns) ? "[]" : _filterColumns);
            }
            set
            {
                _filterColumns = value.ToString();
            }
        }

        [NotMapped]
        public string ColumnDefinitionsStr { get {return _columnDefinitions ;} set{_columnDefinitions = value;} }

        [NotMapped]
        public string FilterColumnsStr { get {return _filterColumns ;} set{_filterColumns = value;} }
        
    }
}