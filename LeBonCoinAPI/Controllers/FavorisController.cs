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
    public class FavorisController : ControllerBase
    {
        private readonly DataContext _context;

        public FavorisController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Favoris
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Favoris>>> GetlesFavoris()
        {
          if (_context.lesFavoris == null)
          {
              return NotFound();
          }
            return await _context.lesFavoris.ToListAsync();
        }

        // GET: api/Favoris/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Favoris>> GetFavoris(int id)
        {
          if (_context.lesFavoris == null)
          {
              return NotFound();
          }
            var favoris = await _context.lesFavoris.FindAsync(id);

            if (favoris == null)
            {
                return NotFound();
            }

            return favoris;
        }

        // PUT: api/Favoris/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFavoris(int id, Favoris favoris)
        {
            if (id != favoris.AnnonceId)
            {
                return BadRequest();
            }

            _context.Entry(favoris).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FavorisExists(id))
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

        // POST: api/Favoris
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Favoris>> PostFavoris(Favoris favoris)
        {
          if (_context.lesFavoris == null)
          {
              return Problem("Entity set 'DataContext.lesFavoris'  is null.");
          }
            _context.lesFavoris.Add(favoris);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (FavorisExists(favoris.AnnonceId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetFavoris", new { id = favoris.AnnonceId }, favoris);
        }

        // DELETE: api/Favoris/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFavoris(int id)
        {
            if (_context.lesFavoris == null)
            {
                return NotFound();
            }
            var favoris = await _context.lesFavoris.FindAsync(id);
            if (favoris == null)
            {
                return NotFound();
            }

            _context.lesFavoris.Remove(favoris);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FavorisExists(int id)
        {
            return (_context.lesFavoris?.Any(e => e.AnnonceId == id)).GetValueOrDefault();
        }
    }
}
