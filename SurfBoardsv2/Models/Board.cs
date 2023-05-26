using Microsoft.EntityFrameworkCore.Metadata.Internal;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace SurfBoardsv2.Models
{
    public class Board
    {
        public Guid Id { get; set; }
        [ConcurrencyCheck]
        public string Name { get; set; }
        [ConcurrencyCheck]
        public float? Length { get; set; }
        [ConcurrencyCheck]
        public float Width { get; set; }
        [ConcurrencyCheck]
        public float Thickness { get; set; }
        [ConcurrencyCheck]
        public float Volume { get; set; }
        [ConcurrencyCheck]
        public string? Type { get; set; }
        [ConcurrencyCheck]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }
        [ConcurrencyCheck]
        public string? Equipment { get; set; }
        [ConcurrencyCheck]
        [NotMapped]
        [DisplayName("Upload file")]
        [DataType(DataType.Upload)]
        public IFormFile? ImageFile { get; set; }
        [ConcurrencyCheck]
        public string? ImageFileName { get; set; }
        [ConcurrencyCheck]
        public bool IsAvailable { get; set; }


        [Timestamp]
        public byte[] RowVersion { get; set; }


    }       

        
}