namespace PDX.BLL.Model {
    public class SearchRequest {
        public string Query { get; set; }
        public int PageNumber { get; set; }
        public int? PageSize { get; set; }
        public string SubmoduleTypeCode { get; set; } = "MDCN";
        public string SubmoduleCode { get; set; }
    }
}