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
    public class DepartementsController : ControllerBase
    {
        private readonly DataContext _context;

        public DepartementsController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Departements
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Departement>>> GetDepartements()
        {
          if (_context.Departements == null)
          {
              return NotFound();
          }
            return await _context.Departements.ToListAsync();
        }

        // GET: api/Departements/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<Departement>> GetDepartement(string id)
        {
          if (_context.Departements == null)
          {
              return NotFound();
          }
            var departement = await _context.Departements.FindAsync(id);

            if (departement == null)
            {
                return NotFound();
            }

            return departement;
        }

        // PUT: api/Departements/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Policy = Policies.admin)]
        public async Task<IActionResult> PutDepartement(string id, Departement departement)
        {
            if (id != departement.DepartementCode)
            {
                return BadRequest();
            }

            _context.Entry(departement).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DepartementExists(id))
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

        // POST: api/Departements
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Policy = Policies.admin)]
        public async Task<ActionResult<Departement>> PostDepartement(Departement departement)
        {
          if (_context.Departements == null)
          {
              return Problem("Entity set 'DataContext.Departements'  is null.");
          }
            _context.Departements.Add(departement);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (DepartementExists(departement.DepartementCode))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetDepartement", new { id = departement.DepartementCode }, departement);
        }

        // DELETE: api/Departements/5
        [HttpDelete("{id}")]
        [Authorize(Policy = Policies.admin)]
        public async Task<IActionResult> DeleteDepartement(string id)
        {
            if (_context.Departements == null)
            {
                return NotFound();
            }
            var departement = await _context.Departements.FindAsync(id);
            if (departement == null)
            {
                return NotFound();
            }

            _context.Departements.Remove(departement);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DepartementExists(string id)
        {
            return (_context.Departements?.Any(e => e.DepartementCode == id)).GetValueOrDefault();
        }
    }
}
