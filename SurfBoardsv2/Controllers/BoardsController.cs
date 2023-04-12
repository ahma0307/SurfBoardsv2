using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SurfBoardsv2.Models;
using SurfBoardsv2.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authorization;
using SurfBoardsv2.Core;
using Microsoft.AspNetCore.Identity;
using SurfBoardsv2.Core.Repositories;
using SurfBoardsv2.Repositories;

namespace SurfBoardsv2.Controllers
{
    public class BoardsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly SignInManager<SurfBoardsv2User> _signinmanager;
        

        public BoardsController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment, SignInManager<SurfBoardsv2User> signInManager)
        {
            _signinmanager = signInManager;
            
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Boards

        
        public async Task<IActionResult> Index(string searchString)
        {
            
            //The first line of the Index action method creates a LINQ query to select the boards:
            var board = from m in _context.Board
                         select m;//The query is only defined at this point, it has not been run against the database

            if (!string.IsNullOrEmpty(searchString)) // If the searchString parameter contains a string, the movies query is modified to filter on the value of the search string:
            {
                board = board.Where(s => s.Name!.Contains(searchString));
                           
                
                //The s => s.Title!.Contains(searchString) code above is a Lambda Expression.
            }
           

 
            return View(await board.ToListAsync());
            
        }

        // GET: Boards/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Board == null)
            {
                return NotFound();
            }

            var board = await _context.Board
                .FirstOrDefaultAsync(m => m.Id == id);
            if (board == null)
            {
                return NotFound();
            }

            return View(board);
        }

        // GET: Boards/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Boards/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = Constants.Policies.RequireAdmin)]
        public async Task<IActionResult> Create([Bind("Id,Name,Length,Width,Thickness,volume,type,Price,Equipment,ImageFile, ImageFileName")] Board board)
        {

            if (ModelState.IsValid)
            {
                
                
                    if (board.ImageFile != null && board.ImageFile.Length > 0)
                    {
                        // Get the file name and extension

                        var fileName = Path.GetFileName(board.ImageFile.FileName);
                        var fileExtension = Path.GetExtension(fileName);

                        // Generate a unique file name to prevent conflicts
                        var uniqueFileName = Guid.NewGuid().ToString() + fileExtension;

                        // Combine the path with the unique file name
                        var filePath = Path.Combine("wwwroot/Images", uniqueFileName);
                        Console.WriteLine(filePath);
                        // Save the file to the server
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await board.ImageFile.CopyToAsync(stream);
                        }

                        // Save the file name to the model
                        board.ImageFileName = uniqueFileName;
                    }
                    
                    board.Id = Guid.NewGuid();
                    _context.Add(board);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                
                
              }
            return View(board);
        }

        // GET: Boards/Edit/5
        
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Board == null)
            {
                return NotFound();
            }

            var board = await _context.Board.FindAsync(id);
            if (board == null)
            {
                return NotFound();
            }
            return View(board);
        }

        // POST: Boards/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = Constants.Policies.RequireAdmin)]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,Length,Width,Thickness,volume,type,Price,Equipment, ImageFile, ImageFileName, IsAvailable")] Board board)
        {
            if (id != board.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Check if user is an administrator
                    var currentUser = await _signinmanager.UserManager.GetUserAsync(User);
                    var isAdmin = await _signinmanager.UserManager.IsInRoleAsync(currentUser, "Administrator");

                    // Update the board's availability if user is an administrator
                    if (isAdmin)
                    {
                        _context.Update(board);
                    }
                    else
                    {
                        // Retrieve the current availability of the board
                        var currentBoard = await _context.Board.AsNoTracking().FirstOrDefaultAsync(b => b.Id == id);
                        board.IsAvailable = currentBoard.IsAvailable; // Restore original value
                    }

                    if (board.ImageFile != null && board.ImageFile.Length > 0)
                    {
                        // Get the file name and extension
                        var fileName = Path.GetFileName(board.ImageFile.FileName);
                        var fileExtension = Path.GetExtension(fileName);

                        // Generate a unique file name to prevent conflicts
                        var uniqueFileName = Guid.NewGuid().ToString() + fileExtension;

                        // Combine the path with the unique file name
                        var filePath = Path.Combine("wwwroot/Images", uniqueFileName);

                        // Save the file to the server
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await board.ImageFile.CopyToAsync(stream);
                        }

                        // Save the file name to the model
                        board.ImageFileName = uniqueFileName;
                    }

                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BoardExists(board.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            return View(board);
        }

        // GET: Boards/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Board == null)
            {
                return NotFound();
            }

            var board = await _context.Board
                .FirstOrDefaultAsync(m => m.Id == id);
            if (board == null)
            {
                return NotFound();
            }

            return View(board);
        }

        // POST: Boards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = Constants.Policies.RequireAdmin)]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Board == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Board'  is null.");
            }
            var board = await _context.Board.FindAsync(id);
            if (board != null)
            {
                _context.Board.Remove(board);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BoardExists(Guid id)
        {
          return _context.Board.Any(e => e.Id == id);
        }
    }
}
