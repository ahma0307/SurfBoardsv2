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
using SurfBoardsv2.Core.ViewModels;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.IO;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Globalization;
using System.Net.NetworkInformation;
using System.Reflection.Metadata;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

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


        public async Task<IActionResult> Index(string currentFilter, string searchString, int? pageNumber)
        {
            //Making boards unavailable if they appear in a rent in the time period
            if (await _context.Rents.AnyAsync())
            {
                foreach (Rent rent in _context.Rents)
                {
                    if (rent.RentPickDate <= DateTime.Today.AddDays(1) && rent.RentDropDate >= DateTime.Today.Date)
                    {
                        var unAvailableBoard = await _context.Boards.FindAsync(rent.RentedBoardId);
                        unAvailableBoard.IsAvailable = false;
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        var unAvailableBoard = await _context.Boards.FindAsync(rent.RentedBoardId);
                        unAvailableBoard.IsAvailable = true;
                        await _context.SaveChangesAsync();
                    }
                }
            }
            else
            {
                foreach (Board availableBoard in _context.Boards)
                {
                    availableBoard.IsAvailable = true;
                }
            }

            await _context.SaveChangesAsync();

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;
            //The first line of the Index action method creates a LINQ query to select the boards:
            var board = from m in _context.Boards
                        select m;//The query is only defined at this point, it has not been run against the database

            if (!string.IsNullOrEmpty(searchString)) // If the searchString parameter contains a string, the movies query is modified to filter on the value of the search string:
            {
                board = board.Where(s => s.Name!.Contains(searchString));


                //The s => s.Title!.Contains(searchString) code above is a Lambda Expression.
            }

            const int pageSize = 8;
            pageNumber = pageNumber ?? 1;

            var filteredBoards = await board
                                .Skip(((int)pageNumber - 1) * pageSize)
                                .Take(pageSize)
                                .ToListAsync();

            // Pass total pages and current page to view
            ViewData["TotalPages"] = (int)Math.Ceiling((double)await board.CountAsync() / pageSize);
            ViewData["PageIndex"] = pageNumber;
            ViewData["HasPreviousPage"] = pageNumber > 1;
            ViewData["HasNextPage"] = pageNumber < (int)ViewData["TotalPages"];

            return View(filteredBoards);
        }


        // GET: Boards/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Boards == null)
            {
                return NotFound();
            }

            var board = await _context.Boards
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
        [Authorize(Policy = Constants.Policies.RequireManager)]
        public async Task<IActionResult> Create([Bind("Id,Name,Length,Width,Thickness,Volume,Type,Price,Equipment,IsAvailable")] Board board)
        {
            if (ModelState.IsValid)
            {
                //var i = 0;

                //foreach (IFormFile imageFile in board.ImageFiles)
                //{
                //    string wwwRootPath = _webHostEnvironment.WebRootPath;
                //    string fileName = Path.GetFileNameWithoutExtension(imageFile.FileName);
                //    string extension = Path.GetExtension(imageFile.FileName);
                //    fileName = fileName + DateTime.Now.ToString("yy-MM-dd-HH-mm-ss") + extension;
                //    string path = Path.Combine(wwwRootPath + "/Images/", fileName);

                //    using (var fileStream = new FileStream(path,FileMode.Create))
                //    {
                //        await imageFile.CopyToAsync(fileStream);
                //    }

                //    var boardImage = new BoardImage
                //    {
                //        FileName = fileName,
                //        Extension = extension,
                //        BoardId = board.Id,
                //        IsMainImage = false,
                //        ImageFile = imageFile
                //    };

                //    if (i == 0)
                //    {
                //        boardImage.IsMainImage = true;
                //    }

                //    _context.Add(boardImage);
                //    await _context.SaveChangesAsync();
                //    i++;
                //}
                
                //List<BoardImage> boardImages = await _context.BoardImages.Where(x => x.BoardId == board.Id).ToListAsync();

                //var mainImage = boardImages.FirstOrDefault(x => x.IsMainImage == true);

                //board.MainImageFileName = mainImage.FileName;
                //board.MainImageId = mainImage.Id;
                
                _context.Add(board);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(board);
        }


        // GET: Boards/Edit/5

        public IActionResult Edit(Guid? id)
        {
            if (id == null || _context.Boards == null)
            {
                return NotFound();
            }

            var board = _context.Boards
            //    .Include(b => b.Images) // Include the images associated with the board
                .FirstOrDefault(m => m.Id == id);

            if (board == null)
            {
                return NotFound();
            }

            var boardViewModel = new BoardViewModel
            {
                Id = board.Id,
                Name = board.Name,
                Length = board.Length.ToString(),
                Width = board.Width.ToString(),
                Thickness = board.Thickness.ToString(),
                Volume = board.Volume.ToString(),
                Type = board.Type,
                Price = board.Price.ToString(),
                Equipment = board.Equipment,
                IsAvailable = board.IsAvailable,
                //Images = board.Images.ToList()
            };

            return View(boardViewModel);
        }


        // POST: Boards/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = Constants.Policies.RequireManager)]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,Length,Width,Thickness,Volume,Type,Price,Equipment,IsAvailable")] BoardViewModel boardViewModel)
        {
            if (id != boardViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                // Retrieve the existing board from the database
                var existingBoard = await _context.Boards
                    //.Include(b => b.Images)
                    .FirstOrDefaultAsync(b => b.Id == id);

                if (existingBoard == null)
                {
                    return NotFound();
                }

                // Update the editable properties of the existing board
                existingBoard.Name = boardViewModel.Name;
                existingBoard.Length = float.Parse(boardViewModel.Length, CultureInfo.InvariantCulture);
                existingBoard.Width = float.Parse(boardViewModel.Width, CultureInfo.InvariantCulture);
                existingBoard.Thickness = float.Parse(boardViewModel.Thickness, CultureInfo.InvariantCulture);
                existingBoard.Volume = float.Parse(boardViewModel.Volume, CultureInfo.InvariantCulture);
                existingBoard.Type = boardViewModel.Type;
                existingBoard.Price = decimal.Parse(boardViewModel.Price, CultureInfo.InvariantCulture);
                existingBoard.Equipment = boardViewModel.Equipment;
                existingBoard.IsAvailable = boardViewModel.IsAvailable;

                // Check if new image files were uploaded
                //if (boardViewModel.ImageFiles != null && boardViewModel.ImageFiles.Count > 0)
                //{
                //    foreach (var file in boardViewModel.ImageFiles)
                //    {
                //        var fileId = Guid.NewGuid();
                //        // Process and save the uploaded image files
                //        var fileName = boardViewModel.Name + "-" + boardViewModel.ImageFiles.Count();// Generate a unique file name

                //        // Get the file extension from the original file name
                //        string fileExtension = Path.GetExtension(fileName);

                //        // Generate a unique filename for the image using the imageId and file extension
                //        string uniqueFileName = $"{fileId}{fileExtension}";

                //        // Set the directory where the images will be stored (adjust this path as per your application's requirements)
                //        string imageDirectory = "wwwroot/Images/";

                //        // Combine the directory and unique filename to create the full filepath
                //        string filePath = Path.Combine(imageDirectory, uniqueFileName);

                //        // Create a new BoardImage entity for each uploaded image file
                //        var image = new BoardImage
                //        {
                //            FileName = fileName,
                //            Extension = filePath
                //        };

                //        existingBoard.Images.Add(image);
                //    }
                //}
                //if (existingBoard.MainImageId == null)
                //{
                //    existingBoard.MainImageId = existingBoard.Images.FirstOrDefault().Id;
                //}

                // Save the changes to the database
                _context.Update(existingBoard);
                await _context.SaveChangesAsync();

                return RedirectToAction("Details", existingBoard.Id);
            }

            return View(boardViewModel);
        }


        // GET: Boards/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == null || _context.Boards == null)
            {
                return NotFound();
            }

            var board = await _context.Boards
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
        [Authorize(Policy = Constants.Policies.RequireManager)]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Boards == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Board'  is null.");
            }
            var board = await _context.Boards.FindAsync(id);
            if (board != null)
            {
                _context.Boards.Remove(board);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BoardExists(Guid id)
        {
            return _context.Boards.Any(e => e.Id == id);
        }

    }
}
