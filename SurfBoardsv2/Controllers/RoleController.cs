using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using SurfBoardsv2.Core;
using SurfBoardsv2.Models;
using Constants = SurfBoardsv2.Core.Constants;

namespace SurfBoardsv2.Controllers
{
    public class RoleController : Controller
    {
        private readonly UserManager<SurfBoardsv2User> _userManager;

        public RoleController(UserManager<SurfBoardsv2User> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Policy = Constants.Policies.RequireAdmin)]
        public IActionResult Manager()
        {
            return View();
        }
        [Authorize(Roles = $"{Constants.Roles.Administrator},{Constants.Roles.Manager}")]
        public IActionResult Admin()
        {
            return View();
        }

        [Authorize(Policy = Constants.Policies.RequireManager)]
        public async Task<IActionResult> PromoteToManager(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            
            if (user == null)
            {
                // User not found
                return NotFound();
            }
            // Check if the user is already in the "Manager" role
            if (!await _userManager.IsInRoleAsync(user, Constants.Roles.Manager))
            {
                // Add the user to the "Manager" role
                await _userManager.AddToRoleAsync(user, Constants.Roles.Manager);
            }

            // Redirect to a success page or perform other actions
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Policy = Constants.Policies.RequireManager)]
        public async Task<IActionResult> PromoteToAdmin(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                // User not found
                return NotFound();
            }

            // Check if the user is already in the "Manager" role
            if (!await _userManager.IsInRoleAsync(user, Constants.Roles.Manager) || !await _userManager.IsInRoleAsync(user, Constants.Roles.Administrator))
            {
                // Add the user to the "Manager" role
                await _userManager.AddToRoleAsync(user, Constants.Roles.Manager);
            }

            // Redirect to a success page or perform other actions
            return RedirectToAction(nameof(Index));
        }
    }
}
