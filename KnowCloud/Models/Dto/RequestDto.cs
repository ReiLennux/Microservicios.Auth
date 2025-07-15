using static KnowCloud.Utility.Utilities;

namespace KnowCloud.Models.Dto
{
    public class RequestDto
    {
        public ContentType ContentType { get; set; }
        public string Url { get; set; }

        public object Data { get; set; }

        public APIType ApiType { get; set; } = APIType.GET;
    }
}
