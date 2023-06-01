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
    public class RentsApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public RentsApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/RentsApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Rent>>> GetRents()
        {
          if (_context.Rents == null)
          {
              return NotFound();
          }
            return await _context.Rents.ToListAsync();
        }

        // GET: api/RentsApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Rent>> GetRent(Guid id)
        {
          if (_context.Rents == null)
          {
              return NotFound();
          }
            var rent = await _context.Rents.FindAsync(id);

            if (rent == null)
            {
                return NotFound();
            }

            return rent;
        }

        // PUT: api/RentsApi/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRent(Guid id, Rent rent)
        {
            if (id != rent.Id)
            {
                return BadRequest();
            }

            _context.Entry(rent).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RentExists(id))
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

        // POST: api/RentsApi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Rent>> PostRent(Rent rent)
        {
          if (_context.Rents == null)
          {
              return Problem("Entity set 'ApplicationDbContext.Rents'  is null.");
          }
            _context.Rents.Add(rent);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRent", new { id = rent.Id }, rent);
        }

        // DELETE: api/RentsApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRent(Guid id)
        {
            if (_context.Rents == null)
            {
                return NotFound();
            }
            var rent = await _context.Rents.FindAsync(id);
            if (rent == null)
            {
                return NotFound();
            }

            _context.Rents.Remove(rent);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RentExists(Guid id)
        {
            return (_context.Rents?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
