namespace KnowCloud.Models
{
    public class FileAttach
    {
        public int Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public string URL { get; set; }

        public string Title { get; set; }

        public int Order { get; set; }
    }
}
