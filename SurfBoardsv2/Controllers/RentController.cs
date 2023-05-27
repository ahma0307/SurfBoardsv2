using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
        private readonly ILogger<RentController> _logger;

        public RentController(ApplicationDbContext context, ILogger<RentController> logger)
        {
            _context = context;
            _logger = logger;
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
        public async Task<IActionResult> Create(Guid boardId, string userId)
        {
            _logger.LogInformation("Create (GET) called with boardId {boardId} and userId {userId}.", boardId, userId);

            var board = await _context.Boards.FindAsync(boardId);
            var user = await _context.Users.FindAsync(userId);

            var currentDate = DateTime.Now.Date;

            if (currentDate.DayOfWeek == DayOfWeek.Saturday)
            {
                currentDate = currentDate.AddDays(2);
            }
            else if (currentDate.DayOfWeek == DayOfWeek.Sunday)
            {
                currentDate = currentDate.AddDays(1);
            }

            var initialRentPickDate = currentDate;

            var nextWeekday = currentDate.AddDays(1);

            while (nextWeekday.DayOfWeek == DayOfWeek.Saturday || nextWeekday.DayOfWeek == DayOfWeek.Sunday)
            {
                nextWeekday = nextWeekday.AddDays(1);
            }

            var initialRentDropDate = nextWeekday;

            var rent = new Rent
            {
                RentPickDate = initialRentPickDate,
                RentDropDate = initialRentDropDate,
                RentedBoard = board,
                BoardRenter = user,
                RentedBoardId = board.Id,
                BoardRenterId = user.Id
            };

            //if (user != null)
            //{
            //    rent.BoardRenter = user;
            //}
            return View(rent);
        }


        public async Task<IActionResult> Confirmation(Guid rentId)
        {
            var rent = await _context.Rents.FindAsync(rentId);
            return View(rent);
        }


        // POST: Rents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,RentPickDate,RentDropDate,RentedBoardId,BoardRenterId")] Rent rent)
        {
            _logger.LogInformation("Create (POST) called.");

            if (ModelState.IsValid)
            {
                var board = await _context.Boards.FindAsync(rent.RentedBoardId);
                var user = await _context.Users.FindAsync(rent.BoardRenterId);

                if (board == null || user == null)
                {
                    _logger.LogWarning("Create (POST): Board or User not found.");
                    return NotFound();
                }

                rent.RentedBoard = board;
                rent.BoardRenter = user;

                _context.Rents.Add(rent);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Rent successfully created.");
                return RedirectToAction("Confirmation");
            }

            _logger.LogWarning("Create (POST): Model State is not valid.");
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

        private bool RentExists(Guid id)
        {
          return (_context.Rents?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
