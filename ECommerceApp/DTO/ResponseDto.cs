using System.Net;

namespace ECommerceApp.DTO
{
    public class ResponseDto<T>
    {
        public HttpStatusCode StatusCode { get; set; }
        public string Message { get; set; }
        public T? Data { get; set; }
    }

}
