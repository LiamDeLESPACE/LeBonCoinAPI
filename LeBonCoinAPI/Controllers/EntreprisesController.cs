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
    public class EntreprisesController : ControllerBase
    {
        private readonly DataContext _context;

        public EntreprisesController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Entreprises
        [HttpGet]
        [Authorize(Policy = Policies.admin)]
        public async Task<ActionResult<IEnumerable<Entreprise>>> GetEntreprises()
        {
          if (_context.Entreprises == null)
          {
              return NotFound();
          }
            return await _context.Entreprises.ToListAsync();
        }

        // GET: api/Entreprises/5
        [HttpGet("{id}")]
        [Authorize(Policy = Policies.all)]
        public async Task<ActionResult<Entreprise>> GetEntreprise(int id)
        {
          if (_context.Entreprises == null)
          {
              return NotFound();
          }
            var entreprise = await _context.Entreprises.FindAsync(id);

            if (entreprise == null)
            {
                return NotFound();
            }

            return entreprise;
        }

        // PUT: api/Entreprises/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Policy = Policies.director)]
        public async Task<IActionResult> PutEntreprise(int id, Entreprise entreprise)
        {
            if (id != entreprise.ProfilId)
            {
                return BadRequest();
            }

            _context.Entry(entreprise).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EntrepriseExists(id))
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

        // POST: api/Entreprises
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Policy = Policies.director)]
        public async Task<ActionResult<Entreprise>> PostEntreprise(Entreprise entreprise)
        {
          if (_context.Entreprises == null)
          {
              return Problem("Entity set 'DataContext.Entreprises'  is null.");
          }
            _context.Entreprises.Add(entreprise);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (EntrepriseExists(entreprise.ProfilId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetEntreprise", new { id = entreprise.ProfilId }, entreprise);
        }

        // DELETE: api/Entreprises/5
        [HttpDelete("{id}")]
        [Authorize(Policy = Policies.director)]
        public async Task<IActionResult> DeleteEntreprise(int id)
        {
            if (_context.Entreprises == null)
            {
                return NotFound();
            }
            var entreprise = await _context.Entreprises.FindAsync(id);
            if (entreprise == null)
            {
                return NotFound();
            }

            _context.Entreprises.Remove(entreprise);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EntrepriseExists(int id)
        {
            return (_context.Entreprises?.Any(e => e.ProfilId == id)).GetValueOrDefault();
        }
    }
}
