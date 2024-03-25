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
    public class ParticuliersController : ControllerBase
    {
        private readonly DataContext _context;

        public ParticuliersController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Particuliers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Particulier>>> GetParticuliers()
        {
          if (_context.Particuliers == null)
          {
              return NotFound();
          }
            return await _context.Particuliers.ToListAsync();
        }

        // GET: api/Particuliers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Particulier>> GetParticulier(int id)
        {
          if (_context.Particuliers == null)
          {
              return NotFound();
          }
            var particulier = await _context.Particuliers.FindAsync(id);

            if (particulier == null)
            {
                return NotFound();
            }

            return particulier;
        }

        // PUT: api/Particuliers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutParticulier(int id, Particulier particulier)
        {
            if (id != particulier.ProfilId)
            {
                return BadRequest();
            }

            _context.Entry(particulier).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ParticulierExists(id))
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

        // POST: api/Particuliers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Particulier>> PostParticulier(Particulier particulier)
        {
          if (_context.Particuliers == null)
          {
              return Problem("Entity set 'DataContext.Particuliers'  is null.");
          }
            _context.Particuliers.Add(particulier);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ParticulierExists(particulier.ProfilId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetParticulier", new { id = particulier.ProfilId }, particulier);
        }

        // DELETE: api/Particuliers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteParticulier(int id)
        {
            if (_context.Particuliers == null)
            {
                return NotFound();
            }
            var particulier = await _context.Particuliers.FindAsync(id);
            if (particulier == null)
            {
                return NotFound();
            }

            _context.Particuliers.Remove(particulier);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ParticulierExists(int id)
        {
            return (_context.Particuliers?.Any(e => e.ProfilId == id)).GetValueOrDefault();
        }
    }
}
