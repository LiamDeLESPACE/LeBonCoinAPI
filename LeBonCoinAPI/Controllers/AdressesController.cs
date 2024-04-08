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
using LeBonCoinAPI.Models.Repository;

namespace LeBonCoinAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdressesController : ControllerBase
    {
        private readonly IRepositoryAdresse<Adresse> repositoryAdresse;

        public AdressesController(IRepositoryAdresse<Adresse> repoAdresse)
        {
            repositoryAdresse = repoAdresse;
        }

        // GET: api/Adresses
        [HttpGet]
        [Authorize(Policy = Policies.admin)]
        public async Task<ActionResult<IEnumerable<Adresse>>> GetAdresses()
        {
          if (_context.Adresses == null)
          {
              return NotFound();
          }
            return await _context.Adresses.ToListAsync();
        }

        // GET: api/Adresses/5
        [HttpGet("{id}")]
        [Authorize(Policy = Policies.all)]
        public async Task<ActionResult<Adresse>> GetAdresse(int id)
        {
          if (_context.Adresses == null)
          {
              return NotFound();
          }
            var adresse = await _context.Adresses.FindAsync(id);

            if (adresse == null)
            {
                return NotFound();
            }

            return adresse;
        }

        // PUT: api/Adresses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Policy = Policies.all)]
        public async Task<IActionResult> PutAdresse(int id, Adresse adresse)
        {
            if (id != adresse.AdresseId)
            {
                return BadRequest();
            }

            _context.Entry(adresse).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdresseExists(id))
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

        // POST: api/Adresses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Policy = Policies.all)]
        public async Task<ActionResult<Adresse>> PostAdresse(Adresse adresse)
        {
          if (_context.Adresses == null)
          {
              return Problem("Entity set 'DataContext.Adresses'  is null.");
          }
            _context.Adresses.Add(adresse);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (AdresseExists(adresse.AdresseId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetAdresse", new { id = adresse.AdresseId }, adresse);
        }

        // DELETE: api/Adresses/5
        [HttpDelete("{id}")]
        [Authorize(Policy = Policies.all)]
        public async Task<IActionResult> DeleteAdresse(int id)
        {
            if (_context.Adresses == null)
            {
                return NotFound();
            }
            var adresse = await _context.Adresses.FindAsync(id);
            if (adresse == null)
            {
                return NotFound();
            }

            _context.Adresses.Remove(adresse);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AdresseExists(int id)
        {
            return (_context.Adresses?.Any(e => e.AdresseId == id)).GetValueOrDefault();
        }
    }
}
