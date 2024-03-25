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
    public class PossedeEquipementsController : ControllerBase
    {
        private readonly DataContext _context;

        public PossedeEquipementsController(DataContext context)
        {
            _context = context;
        }

        // GET: api/PossedeEquipements
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PossedeEquipement>>> GetPossedeEquipements()
        {
          if (_context.PossedeEquipements == null)
          {
              return NotFound();
          }
            return await _context.PossedeEquipements.ToListAsync();
        }

        // GET: api/PossedeEquipements/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PossedeEquipement>> GetPossedeEquipement(int id)
        {
          if (_context.PossedeEquipements == null)
          {
              return NotFound();
          }
            var possedeEquipement = await _context.PossedeEquipements.FindAsync(id);

            if (possedeEquipement == null)
            {
                return NotFound();
            }

            return possedeEquipement;
        }

        // PUT: api/PossedeEquipements/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPossedeEquipement(int id, PossedeEquipement possedeEquipement)
        {
            if (id != possedeEquipement.AnnonceId)
            {
                return BadRequest();
            }

            _context.Entry(possedeEquipement).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PossedeEquipementExists(id))
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

        // POST: api/PossedeEquipements
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PossedeEquipement>> PostPossedeEquipement(PossedeEquipement possedeEquipement)
        {
          if (_context.PossedeEquipements == null)
          {
              return Problem("Entity set 'DataContext.PossedeEquipements'  is null.");
          }
            _context.PossedeEquipements.Add(possedeEquipement);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (PossedeEquipementExists(possedeEquipement.AnnonceId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetPossedeEquipement", new { id = possedeEquipement.AnnonceId }, possedeEquipement);
        }

        // DELETE: api/PossedeEquipements/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePossedeEquipement(int id)
        {
            if (_context.PossedeEquipements == null)
            {
                return NotFound();
            }
            var possedeEquipement = await _context.PossedeEquipements.FindAsync(id);
            if (possedeEquipement == null)
            {
                return NotFound();
            }

            _context.PossedeEquipements.Remove(possedeEquipement);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PossedeEquipementExists(int id)
        {
            return (_context.PossedeEquipements?.Any(e => e.AnnonceId == id)).GetValueOrDefault();
        }
    }
}
