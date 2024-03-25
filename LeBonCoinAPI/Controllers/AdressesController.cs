﻿using System;
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
    public class AdressesController : ControllerBase
    {
        private readonly DataContext _context;

        public AdressesController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Adresses
        [HttpGet]
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
