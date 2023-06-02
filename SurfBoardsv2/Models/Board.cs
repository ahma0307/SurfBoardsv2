using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Diagnostics.CodeAnalysis;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace SurfBoardsv2.Models
{
    public class Board
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Length is required.")]
        public float Length { get; set; }
        [Required(ErrorMessage = "Width is required.")]
        public float Width { get; set; }
        [Required(ErrorMessage = "Thickness is required.")]
        public float Thickness { get; set; }
        [Required(ErrorMessage = "Volume is required.")]
        public float Volume { get; set; }
        public string? Type { get; set; }
        [Required(ErrorMessage = "Price is required.")]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }
        public string? Equipment { get; set; }

        [NotMapped]
        [DisplayName("Upload file")]
        [DataType(DataType.Upload)]
        [Required(AllowEmptyStrings = true)]
        public List<IFormFile>? ImageFiles { get; set; }
        public ICollection<BoardImage>? Images { get; set; }

        public Guid? MainImageId { get; set; }
        public string MainImageFileName { get; set; }

        [ConcurrencyCheck]
        public bool IsAvailable { get; set; }

        public bool PublicBoard { get; set; }
        public ICollection<Rent> Rents { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }

    }


}