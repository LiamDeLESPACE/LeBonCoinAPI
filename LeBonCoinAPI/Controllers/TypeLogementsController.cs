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
    public class TypeLogementsController : ControllerBase
    {
        private readonly DataContext _context;

        public TypeLogementsController(DataContext context)
        {
            _context = context;
        }

        // GET: api/TypeLogements
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TypeLogement>>> GetTypeLogements()
        {
          if (_context.TypeLogements == null)
          {
              return NotFound();
          }
            return await _context.TypeLogements.ToListAsync();
        }

        // GET: api/TypeLogements/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TypeLogement>> GetTypeLogement(int id)
        {
          if (_context.TypeLogements == null)
          {
              return NotFound();
          }
            var typeLogement = await _context.TypeLogements.FindAsync(id);

            if (typeLogement == null)
            {
                return NotFound();
            }

            return typeLogement;
        }

        // PUT: api/TypeLogements/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTypeLogement(int id, TypeLogement typeLogement)
        {
            if (id != typeLogement.TypeLogementId)
            {
                return BadRequest();
            }

            _context.Entry(typeLogement).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TypeLogementExists(id))
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

        // POST: api/TypeLogements
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TypeLogement>> PostTypeLogement(TypeLogement typeLogement)
        {
          if (_context.TypeLogements == null)
          {
              return Problem("Entity set 'DataContext.TypeLogements'  is null.");
          }
            _context.TypeLogements.Add(typeLogement);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TypeLogementExists(typeLogement.TypeLogementId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetTypeLogement", new { id = typeLogement.TypeLogementId }, typeLogement);
        }

        // DELETE: api/TypeLogements/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTypeLogement(int id)
        {
            if (_context.TypeLogements == null)
            {
                return NotFound();
            }
            var typeLogement = await _context.TypeLogements.FindAsync(id);
            if (typeLogement == null)
            {
                return NotFound();
            }

            _context.TypeLogements.Remove(typeLogement);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TypeLogementExists(int id)
        {
            return (_context.TypeLogements?.Any(e => e.TypeLogementId == id)).GetValueOrDefault();
        }
    }
}
