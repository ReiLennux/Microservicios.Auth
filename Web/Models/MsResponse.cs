namespace Web.Models
{
    public class MsResponse
    {
        public object? Result { get; set; }
        public bool IsSuccess { get; set; }
        public string? Message { get; set; }
    }
}
