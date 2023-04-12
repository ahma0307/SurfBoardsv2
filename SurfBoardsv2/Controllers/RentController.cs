using Microsoft.AspNetCore.Mvc;
using SurfBoardsv2.Models;
using SurfBoardsv2.Controllers;
using SurfBoardsv2.Repositories;
using SurfBoardsv2.Data;

namespace SurfBoardsv2.Controllers
{
    public class RentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RentController (ApplicationDbContext context)
        {
           _context = context;
        }

        public IActionResult Index()
        {
            return View();
            
        }
        public IActionResult SurfBoardModels()
        {

            var query = from p in _context.Board
                    select p.Name;

        // store the data in a list
            List<string> board = query.ToList();
            ViewBag.Board = board;
            return View();

        }
        



        public ActionResult GetBoard()
        {
            var board = _context.Board.ToList();
           
            return Json(board);
        }

       
        
       
       
    }


        

}
