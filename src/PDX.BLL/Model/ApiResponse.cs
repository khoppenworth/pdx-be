namespace PDX.BLL.Model
{
    public class ApiResponse
    {
        public int StatusCode { get; set; }
        public object Result { get; set; }
        public string Type { get; set; }
        public string Message { get; set; }
        public bool IsSuccess { get; set; } 
    }
}