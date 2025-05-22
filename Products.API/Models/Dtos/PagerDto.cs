namespace Products.API.Models.Dtos
{
    public record PagerDto(int Page = 1, int RecordsPerPage = 10)
    {
        private const int MaxRecordPerPage = 50;

        public int Page { get; set; } = Math.Max(1, Page);

        public int RecordPerPage { get; set; } = Math.Clamp(RecordsPerPage, 1, MaxRecordPerPage);

    }
}
