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
    public class CarteBancairesController : ControllerBase
    {
        private readonly DataContext _context;

        public CarteBancairesController(DataContext context)
        {
            _context = context;
        }

        // GET: api/CarteBancaires
        [HttpGet]
        [Authorize(Policy = Policies.admin)]
        public async Task<ActionResult<IEnumerable<CarteBancaire>>> GetCarteBancaires()
        {
          if (_context.CarteBancaires == null)
          {
              return NotFound();
          }
            return await _context.CarteBancaires.ToListAsync();
        }

        // GET: api/CarteBancaires/5
        [HttpGet("{id}")]
        [Authorize(Policy = Policies.human)]
        public async Task<ActionResult<CarteBancaire>> GetCarteBancaire(int id)
        {
          if (_context.CarteBancaires == null)
          {
              return NotFound();
          }
            var carteBancaire = await _context.CarteBancaires.FindAsync(id);

            if (carteBancaire == null)
            {
                return NotFound();
            }

            return carteBancaire;
        }

        // PUT: api/CarteBancaires/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Policy = Policies.human)]
        public async Task<IActionResult> PutCarteBancaire(int id, CarteBancaire carteBancaire)
        {
            if (id != carteBancaire.CarteId)
            {
                return BadRequest();
            }

            _context.Entry(carteBancaire).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarteBancaireExists(id))
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

        // POST: api/CarteBancaires
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Policy = Policies.human)]
        public async Task<ActionResult<CarteBancaire>> PostCarteBancaire(CarteBancaire carteBancaire)
        {
          if (_context.CarteBancaires == null)
          {
              return Problem("Entity set 'DataContext.CarteBancaires'  is null.");
          }
            _context.CarteBancaires.Add(carteBancaire);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CarteBancaireExists(carteBancaire.CarteId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetCarteBancaire", new { id = carteBancaire.CarteId }, carteBancaire);
        }

        // DELETE: api/CarteBancaires/5
        [HttpDelete("{id}")]
        [Authorize(Policy = Policies.human)]
        public async Task<IActionResult> DeleteCarteBancaire(int id)
        {
            if (_context.CarteBancaires == null)
            {
                return NotFound();
            }
            var carteBancaire = await _context.CarteBancaires.FindAsync(id);
            if (carteBancaire == null)
            {
                return NotFound();
            }

            _context.CarteBancaires.Remove(carteBancaire);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CarteBancaireExists(int id)
        {
            return (_context.CarteBancaires?.Any(e => e.CarteId == id)).GetValueOrDefault();
        }
    }
}
