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
        public string Name { get; set; }
        public float? Length { get; set; }
        public float Width { get; set; }
        public float Thickness { get; set; }
        public float Volume { get; set; }
        public string? Type { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }
        public string? Equipment { get; set; }
        [NotMapped]
        [DisplayName("Upload file")]
        [DataType(DataType.Upload)]
        public IFormFile? ImageFile { get; set; }
        public string? ImageFileName { get; set; }
        [ConcurrencyCheck]
        public bool IsAvailable { get; set; }

        public bool PublicBoard { get; set; }
        public ICollection<Rent> Rents { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }


    }       

        
}