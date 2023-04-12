using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using SurfBoardsv2.Models;

namespace SurfBoardsv2.Core.ViewModels
{
    public class EditUserViewModel
    {
        public SurfBoardsv2User User { get; set; }

        public IList<SelectListItem> Roles { get; set; }
    }
}
