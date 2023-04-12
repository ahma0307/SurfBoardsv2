using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SurfBoardsv2.Core.Repositories;
using SurfBoardsv2.Core.ViewModels;
using SurfBoardsv2.Models;

namespace SurfBoardsv2.Controllers
{
    public class UserController : Controller
    {
        private readonly SignInManager<SurfBoardsv2User> _signInManager;
        private readonly IUnitOfWork _unitOfWork;
        

        public UserController(IUnitOfWork unitOfWork, SignInManager<SurfBoardsv2User> signInManager)
        {
            _signInManager = signInManager;
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            var users = _unitOfWork.User.GetUsers();

            return View(users);
        }

        public async Task<IActionResult> Edit(string id)

        {
            var user = _unitOfWork.User.GetUser(id);
            var roles = _unitOfWork.Role.GetRoles();

            var userRoles = await _signInManager.UserManager.GetRolesAsync(user);

         

       
            var roleItems = roles.Select(role =>
            new SelectListItem(
                role.Name,
                role.Id,
                userRoles.Any(ur => ur.Contains(role.Name)))).ToList();
            var vm = new EditUserViewModel
            {
                Roles = roleItems,
                User = user
            };

            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> OnPostAsync(EditUserViewModel data)
        {
            var user = _unitOfWork.User.GetUser(data.User.Id);
            if(user == null)
            {
                return NotFound();
            }
            var userRolesInDb = await _signInManager.UserManager.GetRolesAsync(user);

            var rolesToAdd = new List<string>();
            var rolesToDelete = new List<string>(); 
             foreach (var role in data.Roles)
            {
                var assignedInDb = userRolesInDb.FirstOrDefault(ur => ur == role.Text);
                if (role.Selected)
                {
                    if (assignedInDb == null)
                    {
                        rolesToAdd.Add(role.Text);
                    }
                    if (assignedInDb != null)
                    {
                        rolesToDelete.Add(role.Text);
                    }
                }
            }

             if (rolesToAdd.Any())
            {
                await _signInManager.UserManager.AddToRolesAsync(user,rolesToAdd);
            }
             if (rolesToDelete.Any())
            {
                await _signInManager.UserManager.RemoveFromRolesAsync(user, rolesToDelete);
            }
            user.UserName = data.User.UserName;
            user.DOB = data.User.DOB;
            user.Email = data.User.Email;

            _unitOfWork.User.UpdateUser(user);


            return RedirectToAction("Edit", new {id = user.Id});
        }
    }
}
