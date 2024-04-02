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
    [Authorize(Policy = Policies.admin)]
    public class TypeEquipementsController : ControllerBase
    {
        private readonly DataContext _context;

        public TypeEquipementsController(DataContext context)
        {
            _context = context;
        }

        // GET: api/TypeEquipements
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TypeEquipement>>> GetTypeEquipements()
        {
          if (_context.TypeEquipements == null)
          {
              return NotFound();
          }
            return await _context.TypeEquipements.ToListAsync();
        }

        // GET: api/TypeEquipements/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TypeEquipement>> GetTypeEquipement(int id)
        {
          if (_context.TypeEquipements == null)
          {
              return NotFound();
          }
            var typeEquipement = await _context.TypeEquipements.FindAsync(id);

            if (typeEquipement == null)
            {
                return NotFound();
            }

            return typeEquipement;
        }

        // PUT: api/TypeEquipements/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTypeEquipement(int id, TypeEquipement typeEquipement)
        {
            if (id != typeEquipement.TypeEquipementId)
            {
                return BadRequest();
            }

            _context.Entry(typeEquipement).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TypeEquipementExists(id))
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

        // POST: api/TypeEquipements
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TypeEquipement>> PostTypeEquipement(TypeEquipement typeEquipement)
        {
          if (_context.TypeEquipements == null)
          {
              return Problem("Entity set 'DataContext.TypeEquipements'  is null.");
          }
            _context.TypeEquipements.Add(typeEquipement);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TypeEquipementExists(typeEquipement.TypeEquipementId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetTypeEquipement", new { id = typeEquipement.TypeEquipementId }, typeEquipement);
        }

        // DELETE: api/TypeEquipements/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTypeEquipement(int id)
        {
            if (_context.TypeEquipements == null)
            {
                return NotFound();
            }
            var typeEquipement = await _context.TypeEquipements.FindAsync(id);
            if (typeEquipement == null)
            {
                return NotFound();
            }

            _context.TypeEquipements.Remove(typeEquipement);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TypeEquipementExists(int id)
        {
            return (_context.TypeEquipements?.Any(e => e.TypeEquipementId == id)).GetValueOrDefault();
        }
    }
}
