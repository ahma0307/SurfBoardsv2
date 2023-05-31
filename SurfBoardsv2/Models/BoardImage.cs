namespace SurfBoardsv2.Models
{
    public class BoardImage
    {
        public Guid Id { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public Guid BoardId { get; set; }
        public bool IsMainImage { get; set; }
    }
}
