using System;
using System.Collections.Generic;

namespace PDX.BLL.Model
{
    public class ChangeLogModel
    {
        public ChangeLogModel()
        {
            ChangeLogs = new List<ChangeLog>();
        }
        public string Version { get; set; }
        public int? VersionNumber { get; set; }
        public ReleaseType ReleaseType { get; set; }
        public string ReleaseTypeCode { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public string Summary { get; set; }
        public IList<ChangeLog> ChangeLogs { get; set; }
    }

    public class ChangeLog{

        public ChangeLog()
        {
            Logs = new List<Log>();
        }

        public int? Priority { get; set; }
        public Label Label { get; set; }
        public IList<Log> Logs { get; set; }
    }

    public class Log{
        public int? ID { get; set; }
        public int? Priority { get; set; }
        public string Name { get; set; }
    }

    public class Label{
        public int? Priority { get; set; }
        public string Name { get; set; }
    }

    public class ReleaseType{
         public string Name { get; set; }
         public string Code { get; set; }
    }
}