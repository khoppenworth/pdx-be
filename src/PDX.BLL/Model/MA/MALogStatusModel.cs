using System.Collections.Generic;
using PDX.Domain.License;

namespace PDX.BLL.Model
{
    public class MAStatusLogModel
    {
        public int MAID { get; set; }
        public string ToStatusCode { get; set; }
        public string Comment { get; set; }
        public int ChangedByUserID { get; set; }
        public IList<MAFieldSubmoduleType> MAFields { get; set; }
    }
}
