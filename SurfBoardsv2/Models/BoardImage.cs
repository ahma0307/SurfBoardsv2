using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SurfBoardsv2.Models
{
    public class BoardImage
    {
        [Key]
        public Guid Id { get; set; }
        public string FileName { get; set; }
        public string Extension { get; set; }
        [ForeignKey(nameof(Board.Id))]
        public Guid BoardId { get; set; }
        public bool IsMainImage { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
    }
}
