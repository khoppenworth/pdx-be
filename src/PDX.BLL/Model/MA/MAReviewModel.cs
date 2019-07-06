using System.Collections.Generic;
using PDX.Domain.Catalog;
using PDX.Domain.Common;
using PDX.Domain.License;

namespace PDX.BLL.Model
{
    public class MAReviewModel
    {
        public ResponderType ResponderType { get; set; }
        public MAReview MAReview { get; set; }
        public IEnumerable<Checklist> ReviewChecklists { get; set; }
    }
}