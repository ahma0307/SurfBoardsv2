using Microsoft.Build.Framework;
using SurfBoardsv2.Controllers;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SurfBoardsv2.Models
{
    public class Rent
    {
        public Guid Id { get; set; }
        [ConcurrencyCheck]
        public DateTime RentPickDate { get; set; }
        [ConcurrencyCheck]
        public DateTime RentDropDate { get; set; }

        // Foreign keys
        public Guid RentedBoardId { get; set; }
        public string BoardRenterId { get; set; }

        [ConcurrencyCheck]
        public Board RentedBoard { get; set; }
        [ConcurrencyCheck]
        public SurfBoardsv2User BoardRenter { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
