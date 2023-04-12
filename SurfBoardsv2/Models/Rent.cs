using Microsoft.Build.Framework;
using SurfBoardsv2.Controllers;

namespace SurfBoardsv2.Models
{
    public class Rent
    {
        public int Id { get; set; }
        [Required]
       
        public DateTime RentPickDate { get; set; }
        [Required]
        public DateTime RentDropDate { get; set; }
        [Required]
        public string SurfBoardModels { get; set; }
        [Required]
        public int UserId { get; set; }



      
        
       
       
        
    }
}
