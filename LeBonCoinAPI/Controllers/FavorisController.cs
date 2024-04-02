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
    public class FavorisController : ControllerBase
    {
        private readonly DataContext _context;

        public FavorisController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Favoris
        [HttpGet]
        [Authorize(Policy = Policies.admin)]
        public async Task<ActionResult<IEnumerable<Favoris>>> GetlesFavoris()
        {
          if (_context.lesFavoris == null)
          {
              return NotFound();
          }
            return await _context.lesFavoris.ToListAsync();
        }

        // GET: api/FavorisOfProfil/5
        [HttpGet("{idProfil}")]
        [Authorize(Policy = Policies.all)]
        public async Task<ActionResult<IEnumerable<Favoris>>> GetFavorisOfProfil(int idProfil)
        {
          if (_context.lesFavoris == null)
          {
              return NotFound();
          }
            var favoris = await (from s in _context.lesFavoris where s.ProfilId == idProfil select s).ToListAsync();

            if (favoris == null)
            {
                return NotFound();
            }

            return favoris;
        }

        // GET: api/Favoris/5
        [HttpGet("{idProfil}/{idAnnonce}")]
        [Authorize(Policy = Policies.all)]
        public async Task<ActionResult<Favoris>> GetFavoris(int idProfil, int idAnnonce)
        {
            if (_context.lesFavoris == null)
            {
                return NotFound();
            }
            var favoris = await (from s in _context.lesFavoris where s.ProfilId == idProfil && s.AnnonceId == idAnnonce select s).FirstOrDefaultAsync();

            if (favoris == null)
            {
                return NotFound();
            }

            return favoris;
        }

        // PUT: api/Favoris/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{idProfil}/{idAnnonce}")]
        [Authorize(Policy = Policies.all)]
        public async Task<IActionResult> PutFavoris(int idProfil, int idAnnonce, Favoris favoris)
        {
            if (idProfil != favoris.ProfilId || idAnnonce != favoris.AnnonceId)
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
                if (!FavorisExists(idProfil, idAnnonce))
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
        [Authorize(Policy = Policies.all)]
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
                if (FavorisExists(favoris.ProfilId, favoris.AnnonceId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetFavoris", new { idProfil = favoris.ProfilId, idAnnonce = favoris.AnnonceId }, favoris);
        }

        // DELETE: api/Favoris/5/6
        [HttpDelete("{id}")]
        [Authorize(Policy = Policies.all)]
        public async Task<IActionResult> DeleteFavoris(int idProfil, int idAnnonce)
        {
            if (_context.lesFavoris == null)
            {
                return NotFound();
            }
            var favoris = await _context.lesFavoris.FirstOrDefaultAsync(x => x.ProfilId == idProfil && x.AnnonceId == idAnnonce);
            if (favoris == null)
            {
                return NotFound();
            }

            _context.lesFavoris.Remove(favoris);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FavorisExists(int idProfil, int idAnnonce)
        {
            return (_context.lesFavoris?.Any(e => e.ProfilId == idProfil && e.AnnonceId == idAnnonce)).GetValueOrDefault();
        }
    }
}
