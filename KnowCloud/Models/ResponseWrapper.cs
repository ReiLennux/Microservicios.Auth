using System.Net;

namespace KnowCloud.Models
{
    public class ResponseWrapper<T>
    {
        public HttpStatusCode StatusCode { get; set; }

        public string StatusMessage { get; set; }



    }
}
