using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SurfBoardsv2.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace SurfBoardsv2.Core.ViewModels
{
    public class BoardViewModel
    {
        public Guid? Id { get; set; }
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Length is required.")]
        public string Length { get; set; }
        [Required(ErrorMessage = "Width is required.")]
        public string Width { get; set; }
        [Required(ErrorMessage = "Thickness is required.")]
        public string Thickness { get; set; }
        [Required(ErrorMessage = "Volume is required.")]
        public string Volume { get; set; }
        public string? Type { get; set; }
        [Required(ErrorMessage = "Price is required.")]
        public string Price { get; set; }
        public string? Equipment { get; set; }

        [NotMapped]
        [DisplayName("Upload file")]
        [DataType(DataType.Upload)]
        public List<IFormFile> ImageFiles { get; set; }
        public ICollection<BoardImage> Images { get; set; }
        

        public Guid? MainImageId { get; set; }
        public string MainImageFilePath { get; set; }
        public bool IsAvailable { get; set; }


    }
}
