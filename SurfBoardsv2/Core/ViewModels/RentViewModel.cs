namespace SurfBoardsv2.Core.ViewModels
{
    public class RentViewModel
    {
        public Guid Id { get; set; }
        public DateTime RentPickDate { get; set; }
        public DateTime RentDropDate { get; set; }
        public string BoardName { get; set; }
        public string UserFullName { get; set; }
        
    }
}

