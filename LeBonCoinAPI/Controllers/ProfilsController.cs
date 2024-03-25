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
    public class ProfilsController : ControllerBase
    {
#warning suppression du contrôleur ?
        private readonly DataContext _context;

        public ProfilsController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Profils
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Profil>>> GetProfils()
        {
          if (_context.Profils == null)
          {
              return NotFound();
          }
            return await _context.Profils.ToListAsync();
        }

        // GET: api/Profils/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Profil>> GetProfil(int id)
        {
          if (_context.Profils == null)
          {
              return NotFound();
          }
            var profil = await _context.Profils.FindAsync(id);

            if (profil == null)
            {
                return NotFound();
            }

            return profil;
        }

        // PUT: api/Profils/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProfil(int id, Profil profil)
        {
            if (id != profil.ProfilId)
            {
                return BadRequest();
            }

            _context.Entry(profil).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProfilExists(id))
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

        // POST: api/Profils
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Profil>> PostProfil(Profil profil)
        {
          if (_context.Profils == null)
          {
              return Problem("Entity set 'DataContext.Profils'  is null.");
          }
            _context.Profils.Add(profil);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ProfilExists(profil.ProfilId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetProfil", new { id = profil.ProfilId }, profil);
        }

        // DELETE: api/Profils/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProfil(int id)
        {
            if (_context.Profils == null)
            {
                return NotFound();
            }
            var profil = await _context.Profils.FindAsync(id);
            if (profil == null)
            {
                return NotFound();
            }

            _context.Profils.Remove(profil);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProfilExists(int id)
        {
            return (_context.Profils?.Any(e => e.ProfilId == id)).GetValueOrDefault();
        }
    }
}
