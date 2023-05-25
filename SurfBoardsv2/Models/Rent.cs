using Microsoft.Build.Framework;
using SurfBoardsv2.Controllers;
using System.ComponentModel.DataAnnotations;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;

namespace SurfBoardsv2.Models
{
    public class Rent
    {
        public Guid Id { get; set; }
        [Required]
       
        public DateTime RentPickDate { get; set; }
        [Required]
        public DateTime RentDropDate { get; set; }
        [Required]
        public string SurfBoardModels { get; set; }
        [Required]
        public int UserId { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }


        

    }
}
