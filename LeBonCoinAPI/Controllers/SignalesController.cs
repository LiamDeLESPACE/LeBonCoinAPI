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
    public class SignalesController : ControllerBase
    {
        private readonly IRepositorySignale<Signale> _repositorySignale;

        public SignalesController(IRepositorySignale<Signale> repository)
        {
            _repositorySignale = repository;
        }

        // GET: api/Signales
        [HttpGet]
        [Authorize(Policy = Policies.admin)]
        public async Task<ActionResult<IEnumerable<Signale>>> GetSignales()
        {

          if (_repositorySignale == null)
          {
              return NotFound();
          }
            return await _repositorySignale.GetAll();
        }

        // GET: api/Signales/5
        [HttpGet("{idAnnonce}/{idProfil}")]
        [Authorize(Policy = Policies.admin)]
        public async Task<ActionResult<Signale>> GetSignaleByIds(int idAnnonce, int idProfil)
        {
          if (_repositorySignale == null)
          {
              return NotFound();
          }
            var signale = await _repositorySignale.GetByIds(idAnnonce, idProfil);

            if (signale == null)
            {
                return NotFound();
            }

            return signale;
        }

        // GET: api/Signales/a/5
        [HttpGet("a/{idAnnonce}")]
        [Authorize(Policy = Policies.all)]
        public async Task<ActionResult<IEnumerable<Signale>>> GetSignaleByIdAnnonce(int idAnnonce)
        {
            if (_repositorySignale == null)
            {
                return NotFound();
            }
            var signale = await _repositorySignale.GetByIdAnnonce(idAnnonce);

            if (signale == null)
            {
                return NotFound();
            }

            return signale;
        }

        // GET: api/Signales/p/5
        [HttpGet("p/{idProfil}")]
        [Authorize(Policy = Policies.all)]
        public async Task<ActionResult<IEnumerable<Signale>>> GetSignaleByIdProfil(int idProfil)
        {
            if (_repositorySignale == null)
            {
                return NotFound();
            }
            var signale = await _repositorySignale.GetByIdProfil(idProfil);

            if (signale == null)
            {
                return NotFound();
            }

            return signale;
        }

        /*
        // PUT: api/Signales/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Policy = Policies.admin)]
        public async Task<IActionResult> PutSignale(int idAnnonce, int idProfil, Signale signale)
        {
            if (idAnnonce != signale.AnnonceId || idProfil != signale.ProfilId)
            {
                return BadRequest();
            }

            _context.Entry(signale).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SignaleExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }*/

        // POST: api/Signales
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Policy = Policies.all)]
        public async Task<ActionResult<Signale>> PostSignale(Signale signale)
        {
          if (_repositorySignale == null)
          {
              return Problem("Repository is null.");
          }
            await _repositorySignale.Add(signale);

            return CreatedAtAction("GetSignale", new { id = signale.ProfilId }, signale);
        }

        // DELETE: api/Signales/5
        [HttpDelete("{idAnnonce}/{idProfil}")]
        [Authorize(Policy = Policies.admin)]
        public async Task<IActionResult> DeleteSignale(int idAnnonce, int idProfil)
        {
            if (_repositorySignale == null)
            {
                return NotFound();
            }
            var signale = await _repositorySignale.GetByIds(idAnnonce,idProfil);
            if (signale.Value == null)
            {
                return NotFound();
            }

            await _repositorySignale.Delete(signale.Value);

            return NoContent();
        }

        /*private bool SignaleExists(int id)
        {
            return (_context.Signales?.Any(e => e.ProfilId == id)).GetValueOrDefault();
        }*/
    }
}
