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
    public class ReglementsController : ControllerBase
    {
#warning jsp a quoi il sert / delete ?
        private readonly DataContext _context;

        public ReglementsController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Reglements
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Reglement>>> GetReglements()
        {
          if (_context.Reglements == null)
          {
              return NotFound();
          }
            return await _context.Reglements.ToListAsync();
        }

        // GET: api/Reglements/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Reglement>> GetReglement(string id)
        {
          if (_context.Reglements == null)
          {
              return NotFound();
          }
            var reglement = await _context.Reglements.FindAsync(id);

            if (reglement == null)
            {
                return NotFound();
            }

            return reglement;
        }

        // PUT: api/Reglements/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReglement(string id, Reglement reglement)
        {
            if (id != reglement.ReglementId)
            {
                return BadRequest();
            }

            _context.Entry(reglement).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReglementExists(id))
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

        // POST: api/Reglements
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Reglement>> PostReglement(Reglement reglement)
        {
          if (_context.Reglements == null)
          {
              return Problem("Entity set 'DataContext.Reglements'  is null.");
          }
            _context.Reglements.Add(reglement);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ReglementExists(reglement.ReglementId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetReglement", new { id = reglement.ReglementId }, reglement);
        }

        // DELETE: api/Reglements/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReglement(string id)
        {
            if (_context.Reglements == null)
            {
                return NotFound();
            }
            var reglement = await _context.Reglements.FindAsync(id);
            if (reglement == null)
            {
                return NotFound();
            }

            _context.Reglements.Remove(reglement);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ReglementExists(string id)
        {
            return (_context.Reglements?.Any(e => e.ReglementId == id)).GetValueOrDefault();
        }
    }
}
