using Microsoft.Build.Framework;
using SurfBoardsv2.Controllers;
using System.ComponentModel.DataAnnotations;

namespace SurfBoardsv2.Models
{
    public class Rent
    {
        public Guid Id { get; set; }
        [ConcurrencyCheck]
        public DateTime RentPickDate { get; set; }
        [ConcurrencyCheck]
        public DateTime RentDropDate { get; set; }
        [ConcurrencyCheck]
        public string SurfBoardModelId { get; set; }
        [ConcurrencyCheck]
        public string SurfBoardModelName { get; set; }
        [ConcurrencyCheck]
        public string UserId { get; set; }
      
        [Timestamp]
        public byte[] RowVersion { get; set; }

        public void SetUserId(SurfBoardsv2User surfBoardsv2User)
        {
            this.UserId = surfBoardsv2User.Id.ToString();
        }

    }
}
