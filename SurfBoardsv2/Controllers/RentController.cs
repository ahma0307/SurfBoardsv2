using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SurfBoardsv2.Core.ViewModels;
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
        public IActionResult Index()
        {
            //Fetching from Database
            var rents = _context.Rents.ToList(); 
            var boards = _context.Boards.ToList();
            var users = _context.Users.ToList();

            //Supplies RentViewModel with the data needed for the view
            var rentViewModels = rents.Select(rent => new RentViewModel
            {
                Id = rent.Id,
                RentPickDate = rent.RentPickDate,
                RentDropDate = rent.RentDropDate,
                BoardName = boards.Find(x => x.Id == rent.RentedBoardId).Name,
                UserFullName = users.Find(x => x.Id == rent.BoardRenterId.ToString()).GetFullName()
            }).ToList();

            return View(rentViewModels);
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
        [Authorize]
        public async Task<IActionResult> Create(Guid boardId, string userId)
        {
            
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
                RentedBoardId = board.Id,
                BoardRenterId = Guid.Parse(user.Id),
            };

            var model = new Tuple<Rent, Board, SurfBoardsv2User>(rent, board, user);

            return View(model);
        }


        // POST: Rents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,RentPickDate,RentDropDate,RentedBoardId,BoardRenterId")] Rent rent)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                foreach (var error in errors)
                {
                    System.Diagnostics.Debug.WriteLine(error.ErrorMessage);
                }
                return View(rent);
            }

            bool saved = false;
            while (!saved)
            {
                try
                {
                    var board = await _context.Boards.FindAsync(rent.RentedBoardId);
                    var otherRents = await _context.Rents.Where(x => x.RentedBoardId == board.Id).ToListAsync();

                    foreach (var otherRent in otherRents)
                    {
                        if (otherRent.RentPickDate <= rent.RentDropDate && otherRent.RentDropDate >= rent.RentPickDate)
                        {
                            return View(rent);
                        }
                    }
                    
                    rent.TimeOfOrder = DateTime.Now;
                    await _context.Rents.AddAsync(rent);
                    await _context.SaveChangesAsync();
                    saved = true;
                }
                catch (DbUpdateConcurrencyException)
                {
                    foreach (var entry in _context.ChangeTracker.Entries())
                    {
                        if (entry.State == EntityState.Modified)
                        {
                            entry.Reload();
                        }
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                    ModelState.AddModelError("", "Unable to save changes. An error occurred while updating the entries. ");
                    return View(rent);
                }
            }

            return RedirectToAction("Confirmation", rent);
        }

        // GET: Rents/Create
        
        public async Task<IActionResult> CreateNotLoggedIn(Guid boardId)
        {

            var board = await _context.Boards.FindAsync(boardId);

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
                RentedBoardId = board.Id,
                BoardRenterId = Guid.NewGuid(),
            };

            var model = new Tuple<Rent, Board>(rent, board);

            return View(model);
        }


        // POST: Rents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateNotLoggedIn([Bind("Id,RentPickDate,RentDropDate,RentedBoardId,BoardRenterId,BoardRenterFirstName,BoardRenterLastName,BoardRenterEmail")] Rent rent)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                foreach (var error in errors)
                {
                    System.Diagnostics.Debug.WriteLine(error.ErrorMessage);
                }
                return View(rent);
            }

            bool saved = false;
            while (!saved)
            {
                try
                {
                    var board = await _context.Boards.FindAsync(rent.RentedBoardId);
                    var otherRents = await _context.Rents.Where(x => x.RentedBoardId == board.Id).ToListAsync();

                    foreach (var otherRent in otherRents)
                    {
                        if (otherRent.RentPickDate <= rent.RentDropDate && otherRent.RentDropDate >= rent.RentPickDate)
                        {
                            return View(rent);
                        }
                    }

                    rent.TimeOfOrder = DateTime.Now;
                    await _context.Rents.AddAsync(rent);
                    await _context.SaveChangesAsync();
                    saved = true;
                }
                catch (DbUpdateConcurrencyException)
                {
                    foreach (var entry in _context.ChangeTracker.Entries())
                    {
                        if (entry.State == EntityState.Modified)
                        {
                            entry.Reload();
                        }
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                    ModelState.AddModelError("", "Unable to save changes. An error occurred while updating the entries. ");
                    return View(rent);
                }
            }

            return RedirectToAction("ConfirmationNotLoggedIn", rent);
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

            var board = await _context.Boards.FindAsync(rent.RentedBoardId);
            var user = await _context.Users.FindAsync(rent.BoardRenterId);

            var model = new Tuple<Rent, Board, SurfBoardsv2User>(rent, board, user);

            return View(model);
        }

        // POST: Rents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,RentPickDate,RentDropDate,RentedBoardId,BoardRenterId")] Rent rent)
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

        // GET : Rents/Confirmation
        public async Task<IActionResult> Confirmation(Rent rent)
        {
            var board = await _context.Boards.FindAsync(rent.RentedBoardId);
            var user = await _context.Users.FindAsync(rent.BoardRenterId.ToString());

            var model = new Tuple<Rent, Board, SurfBoardsv2User>(rent, board, user);

            return View(model);
        }

        public async Task<IActionResult> ConfirmationNotLoggedIn(Rent rent)
        {
            var board = await _context.Boards.FindAsync(rent.RentedBoardId);
            
            var model = new Tuple<Rent, Board>(rent, board);

            return View(model);
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
