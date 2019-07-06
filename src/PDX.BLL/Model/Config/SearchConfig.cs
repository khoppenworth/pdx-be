using System.Collections.Generic;

namespace PDX.BLL.Model.Config
{
    public class SearchConfig
    {
        public IList<string> Strings { get; set; }
        public IList<int> Integers { get; set; }
        public IList<double> Doubles { get; set; }
        public IList<decimal> Decimals { get; set; }
    }
}