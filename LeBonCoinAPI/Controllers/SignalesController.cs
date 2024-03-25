using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LeBonCoinAPI.Models.EntityFramework;

namespace LeBonCoinAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SignalesController : ControllerBase
    {
        private readonly DataContext _context;

        public SignalesController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Signales
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Signale>>> GetSignales()
        {
          if (_context.Signales == null)
          {
              return NotFound();
          }
            return await _context.Signales.ToListAsync();
        }

        // GET: api/Signales/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Signale>> GetSignale(int id)
        {
          if (_context.Signales == null)
          {
              return NotFound();
          }
            var signale = await _context.Signales.FindAsync(id);

            if (signale == null)
            {
                return NotFound();
            }

            return signale;
        }

        // PUT: api/Signales/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSignale(int id, Signale signale)
        {
            if (id != signale.ProfilId)
            {
                return BadRequest();
            }

            _context.Entry(signale).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SignaleExists(id))
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

        // POST: api/Signales
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Signale>> PostSignale(Signale signale)
        {
          if (_context.Signales == null)
          {
              return Problem("Entity set 'DataContext.Signales'  is null.");
          }
            _context.Signales.Add(signale);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (SignaleExists(signale.ProfilId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetSignale", new { id = signale.ProfilId }, signale);
        }

        // DELETE: api/Signales/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSignale(int id)
        {
            if (_context.Signales == null)
            {
                return NotFound();
            }
            var signale = await _context.Signales.FindAsync(id);
            if (signale == null)
            {
                return NotFound();
            }

            _context.Signales.Remove(signale);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SignaleExists(int id)
        {
            return (_context.Signales?.Any(e => e.ProfilId == id)).GetValueOrDefault();
        }
    }
}
