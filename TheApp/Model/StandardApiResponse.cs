using System.Net;

namespace TheApp.Model
{
    public class StandardApiResponse
    {
        public bool IsSuccess { get; set; }
        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.NotImplemented;
        public dynamic Data { get; set; } = null;
        public string Message { get; set; }
    }
}
