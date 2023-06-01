using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SurfBoardsv2.Data;
using SurfBoardsv2.Models;

namespace SurfBoardsv2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BoardsApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public BoardsApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/BoardsApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Board>>> GetBoards()
        {
          if (_context.Boards == null)
          {
              return NotFound();
          }
            return await _context.Boards.ToListAsync();
        }

        // GET: api/BoardsApi/5
        [HttpGet("Search")]
        public async Task<ActionResult<IEnumerable<Board>>> SearchBoards([FromQuery] Guid? id = null, [FromQuery] string? name = null, [FromQuery] float? length = null, [FromQuery] float? width = null, [FromQuery] float? thickness = null, [FromQuery] float? volume = null, [FromQuery] string? type = null, [FromQuery] decimal? price = null, [FromQuery] string? equipment = null)
        {
            if (_context.Boards == null)
            {
                return NotFound();
            }

            var query = _context.Boards.AsQueryable();

            if (id.HasValue)
            {
                query = query.Where(b => b.Id == id.Value);
            }

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(b => b.Name.Contains(name));
            }

            if (length.HasValue)
            {
                query = query.Where(b => b.Length == length.Value);
            }

            if (width.HasValue)
            {
                query = query.Where(b => b.Width == width.Value);
            }

            if (thickness.HasValue)
            {
                query = query.Where(b => b.Thickness == thickness.Value);
            }

            if (volume.HasValue)
            {
                query = query.Where(b => b.Volume == volume.Value);
            }

            if (!string.IsNullOrEmpty(type))
            {
                query = query.Where(b => b.Type.Contains(type));
            }

            if (price.HasValue)
            {
                query = query.Where(b => b.Price == price.Value);
            }

            if (!string.IsNullOrEmpty(equipment))
            {
                query = query.Where(b => b.Equipment.Contains(equipment));
            }

            var boards = await query.ToListAsync();

            if (boards.Count == 0)
            {
                return NotFound();
            }

            return boards;
        }

        // PUT: api/BoardsApi/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBoard(Guid id, Board board)
        {
            if (id != board.Id)
            {
                return BadRequest();
            }

            _context.Entry(board).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BoardExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/BoardsApi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Board>> PostBoard(Board board)
        {
          if (_context.Boards == null)
          {
              return Problem("Entity set 'ApplicationDbContext.Boards'  is null.");
          }
            _context.Boards.Add(board);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBoard", new { id = board.Id }, board);
        }

        // DELETE: api/BoardsApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBoard(Guid id)
        {
            if (_context.Boards == null)
            {
                return NotFound();
            }
            var board = await _context.Boards.FindAsync(id);
            if (board == null)
            {
                return NotFound();
            }

            _context.Boards.Remove(board);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BoardExists(Guid id)
        {
            return (_context.Boards?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
