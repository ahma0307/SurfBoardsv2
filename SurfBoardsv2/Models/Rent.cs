using Microsoft.Build.Framework;
using SurfBoardsv2.Controllers;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SurfBoardsv2.Models
{
    public class Rent
    {
        public Guid Id { get; set; }
        public DateTime TimeOfOrder { get; set; }
        
        public DateTime RentPickDate { get; set; }
        
        public DateTime RentDropDate { get; set; }

        // Foreign keys
        [ConcurrencyCheck]
        public Guid RentedBoardId { get; set; }
        
        public Guid BoardRenterId { get; set; }

        [Timestamp]
        public byte[]? RowVersion { get; set; }
    }
}
