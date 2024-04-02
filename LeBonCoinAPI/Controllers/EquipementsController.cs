using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LeBonCoinAPI.Models.EntityFramework;
using Microsoft.AspNetCore.Authorization;
using LeBonCoinAPI.Models.Auth;

namespace LeBonCoinAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquipementsController : ControllerBase
    {
        private readonly DataContext _context;

        public EquipementsController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Equipements
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Equipement>>> GetEquipements()
        {
          if (_context.Equipements == null)
          {
              return NotFound();
          }
            return await _context.Equipements.ToListAsync();
        }

        // GET: api/Equipements/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<Equipement>> GetEquipement(int id)
        {
          if (_context.Equipements == null)
          {
              return NotFound();
          }
            var equipement = await _context.Equipements.FindAsync(id);

            if (equipement == null)
            {
                return NotFound();
            }

            return equipement;
        }

        // PUT: api/Equipements/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Policy = Policies.admin)]
        public async Task<IActionResult> PutEquipement(int id, Equipement equipement)
        {
            if (id != equipement.EquipementId)
            {
                return BadRequest();
            }

            _context.Entry(equipement).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EquipementExists(id))
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

        // POST: api/Equipements
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Policy = Policies.admin)]
        public async Task<ActionResult<Equipement>> PostEquipement(Equipement equipement)
        {
          if (_context.Equipements == null)
          {
              return Problem("Entity set 'DataContext.Equipements'  is null.");
          }
            _context.Equipements.Add(equipement);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (EquipementExists(equipement.EquipementId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetEquipement", new { id = equipement.EquipementId }, equipement);
        }

        // DELETE: api/Equipements/5
        [HttpDelete("{id}")]
        [Authorize(Policy = Policies.admin)]
        public async Task<IActionResult> DeleteEquipement(int id)
        {
            if (_context.Equipements == null)
            {
                return NotFound();
            }
            var equipement = await _context.Equipements.FindAsync(id);
            if (equipement == null)
            {
                return NotFound();
            }

            _context.Equipements.Remove(equipement);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EquipementExists(int id)
        {
            return (_context.Equipements?.Any(e => e.EquipementId == id)).GetValueOrDefault();
        }
    }
}
