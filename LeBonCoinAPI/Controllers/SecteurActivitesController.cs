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
    public class SecteurActivitesController : ControllerBase
    {
        private readonly DataContext _context;

        public SecteurActivitesController(DataContext context)
        {
            _context = context;
        }

        // GET: api/SecteurActivites
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SecteurActivite>>> GetSecteurActivites()
        {
          if (_context.SecteurActivites == null)
          {
              return NotFound();
          }
            return await _context.SecteurActivites.ToListAsync();
        }

        // GET: api/SecteurActivites/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SecteurActivite>> GetSecteurActivite(int id)
        {
          if (_context.SecteurActivites == null)
          {
              return NotFound();
          }
            var secteurActivite = await _context.SecteurActivites.FindAsync(id);

            if (secteurActivite == null)
            {
                return NotFound();
            }

            return secteurActivite;
        }

        // PUT: api/SecteurActivites/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSecteurActivite(int id, SecteurActivite secteurActivite)
        {
            if (id != secteurActivite.SecteurId)
            {
                return BadRequest();
            }

            _context.Entry(secteurActivite).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SecteurActiviteExists(id))
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

        // POST: api/SecteurActivites
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SecteurActivite>> PostSecteurActivite(SecteurActivite secteurActivite)
        {
          if (_context.SecteurActivites == null)
          {
              return Problem("Entity set 'DataContext.SecteurActivites'  is null.");
          }
            _context.SecteurActivites.Add(secteurActivite);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (SecteurActiviteExists(secteurActivite.SecteurId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetSecteurActivite", new { id = secteurActivite.SecteurId }, secteurActivite);
        }

        // DELETE: api/SecteurActivites/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSecteurActivite(int id)
        {
            if (_context.SecteurActivites == null)
            {
                return NotFound();
            }
            var secteurActivite = await _context.SecteurActivites.FindAsync(id);
            if (secteurActivite == null)
            {
                return NotFound();
            }

            _context.SecteurActivites.Remove(secteurActivite);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SecteurActiviteExists(int id)
        {
            return (_context.SecteurActivites?.Any(e => e.SecteurId == id)).GetValueOrDefault();
        }
    }
}
