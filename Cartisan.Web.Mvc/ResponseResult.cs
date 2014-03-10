namespace Cartisan.Web.Mvc {
    public class ResponseResult {
        public bool Success { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }
}