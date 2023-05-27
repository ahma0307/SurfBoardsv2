using SurfBoardsv2.Models;

namespace SurfBoardsv2.Core.ViewModels
{
    public class RentBoardViewModel
    {
        public Rent Rent { get; set; }
        public Board Board { get; set; }

        public SurfBoardsv2User User { get; set; }

    }
}
