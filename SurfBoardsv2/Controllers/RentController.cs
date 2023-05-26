using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SurfBoardsv2.Data;
using SurfBoardsv2.Models;

namespace SurfBoardsv2.Controllers
{
    public class RentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RentController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Rents
        public async Task<IActionResult> Index()
        {
              return _context.Rents != null ? 
                          View(await _context.Rents.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Rent'  is null.");
        }

        // GET: Rents/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Rents == null)
            {
                return NotFound();
            }

            var rent = await _context.Rents
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rent == null)
            {
                return NotFound();
            }

            return View(rent);
        }

        // GET: Rents/Create
        public IActionResult Create(Guid? boardId)
        {
            if (boardId != null)
            {
                var selectedBoard = _context.Boards.FirstOrDefault(b => b.Id == boardId);
                if (selectedBoard != null)
                {
                    // Save the selected board id in TempData to use it in the POST action
                    TempData["SelectedBoardId"] = selectedBoard.Id.ToString();

                    var rent = new Rent
                    {
                        SurfBoardModelId = selectedBoard.Id.ToString()
                        // Other properties...
                    };
                    return View(rent);
                }
            }
            return View();
        }


        public IActionResult Confirmation()
        {
            return View();
        }

        // POST: Rents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,RentPickDate,RentDropDate")] Rent rent)
        {
            if (ModelState.IsValid)
            {
                if (TempData["SelectedBoardId"] != null)
                {
                    var boardId = Guid.Parse(TempData["SelectedBoardId"].ToString());
                    rent.SurfBoardModelId = boardId.ToString();
                }

                // Get the current user's id
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (userId == null)
                {
                    // If user id is not found, return unauthorized
                    return Unauthorized();
                }

                // Fetch SurfBoardsv2User instance from the database
                var surfBoardsv2User = await _context.Users.FindAsync(userId);

                if (surfBoardsv2User == null)
                {
                    // If the user is not found, return not found
                    return NotFound();
                }

                rent.Id = Guid.NewGuid();
                rent.SetUserId(surfBoardsv2User);
                _context.Add(rent);
                await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
                return RedirectToAction("Confirmation", new { id = rent.Id });
            }

            return View(rent);
        }


        // GET: Rents/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Rents == null)
            {
                return NotFound();
            }

            var rent = await _context.Rents.FindAsync(id);
            if (rent == null)
            {
                return NotFound();
            }
            return View(rent);
        }

        // POST: Rents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,RentPickDate,RentDropDate,SurfBoardModels,UserId")] Rent rent)
        {
            if (id != rent.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RentExists(rent.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(rent);
        }

        // GET: Rents/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Rents == null)
            {
                return NotFound();
            }

            var rent = await _context.Rents
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rent == null)
            {
                return NotFound();
            }

            return View(rent);
        }

        // POST: Rents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Rents == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Rent'  is null.");
            }
            var rent = await _context.Rents.FindAsync(id);
            if (rent != null)
            {
                _context.Rents.Remove(rent);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Confirmation(Guid id)
        {
            var rent = await _context.Rents.FindAsync(id);

            if (rent == null)
            {
                return NotFound();
            }

            return View(rent);
        }


        private bool RentExists(Guid id)
        {
          return (_context.Rents?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
