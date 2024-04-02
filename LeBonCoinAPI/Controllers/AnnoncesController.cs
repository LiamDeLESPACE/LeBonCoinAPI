using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LeBonCoinAPI.Models.EntityFramework;
using LeBonCoinAPI.Models.Auth;
using Microsoft.AspNetCore.Authorization;

namespace LeBonCoinAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnnoncesController : ControllerBase
    {
        private readonly DataContext _context;

        public AnnoncesController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Annonces
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Annonce>>> GetAnnonces()
        {
          if (_context.Annonces == null)
          {
              return NotFound();
          }
            return await _context.Annonces.ToListAsync();
        }

        // GET: api/Annonces/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<Annonce>> GetAnnonce(int id)
        {
          if (_context.Annonces == null)
          {
              return NotFound();
          }
            var annonce = await _context.Annonces.FindAsync(id);

            if (annonce == null)
            {
                return NotFound();
            }

            return annonce;
        }

        // PUT: api/Annonces/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Policy = Policies.all)]
        public async Task<IActionResult> PutAnnonce(int id, Annonce annonce)
        {
            if (id != annonce.AnnonceId)
            {
                return BadRequest();
            }

            _context.Entry(annonce).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AnnonceExists(id))
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

        // POST: api/Annonces
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Policy = Policies.all)]
        public async Task<ActionResult<Annonce>> PostAnnonce(Annonce annonce)
        {
          if (_context.Annonces == null)
          {
              return Problem("Entity set 'DataContext.Annonces'  is null.");
          }
            _context.Annonces.Add(annonce);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (AnnonceExists(annonce.AnnonceId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetAnnonce", new { id = annonce.AnnonceId }, annonce);
        }

        // DELETE: api/Annonces/5
        [HttpDelete("{id}")]
        [Authorize(Policy = Policies.all)]
        public async Task<IActionResult> DeleteAnnonce(int id)
        {
            if (_context.Annonces == null)
            {
                return NotFound();
            }
            var annonce = await _context.Annonces.FindAsync(id);
            if (annonce == null)
            {
                return NotFound();
            }

            _context.Annonces.Remove(annonce);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AnnonceExists(int id)
        {
            return (_context.Annonces?.Any(e => e.AnnonceId == id)).GetValueOrDefault();
        }
    }
}
