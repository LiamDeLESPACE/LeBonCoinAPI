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
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace LeBonCoinAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillesController : ControllerBase
    {
        private readonly IRepositoryVille<Ville> _villeRepository;

        public VillesController(IRepositoryVille<Ville> repoVille)
        {
            _villeRepository = repoVille;
        }

        // GET: api/Villes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ville>>> GetVilles()
        {

          if (_villeRepository == null)
          {
              return NotFound();
          }
            return await _villeRepository.GetAll();
        }

        // GET: api/Villes/i/74000
        [HttpGet("i/{codeInsee}")]
        [Authorize(Policy = Policies.all)]
        public async Task<ActionResult<Ville>> GetVilleByInsee(string codeInsee)
        {
            var ville = await _villeRepository.GetByInsee(codeInsee);

          if (_villeRepository == null)
          {
              return NotFound();
          }

            if (ville == null)
            {
                return NotFound();
            }

            return ville;
        }

        // GET: api/Villes/i/Annecy
        [HttpGet("n/{nomVille}")]
        [Authorize(Policy = Policies.all)]
        public async Task<ActionResult<Ville>> GetVilleByName(string nomVille)
        {
            var ville = await _villeRepository.GetByNom(nomVille);

            if (_villeRepository == null)
            {
                return NotFound();
            }

            if (ville == null)
            {
                return NotFound();
            }

            return ville;
        }

        // PUT: api/Villes/74000
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{insee}")]
        [Authorize(Policy = Policies.admin)]
        public async Task<IActionResult> PutVille(string insee, Ville ville)
        {
            if (insee != ville.CodeInsee)
            {
                return BadRequest();
            }

            var res = await _villeRepository.GetByInsee(insee);

            if(res.Value == null)
                return NotFound();

            await _villeRepository.Update(res.Value, ville);

            return NoContent();
        }

        // POST: api/Villes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Policy = Policies.all)]
        public async Task<ActionResult<Ville>> PostVille(Ville ville)
        {
          if (_villeRepository == null)
          {
              return Problem("Entity set 'DataContext.Villes'  is null.");
          }
            await _villeRepository.Add(ville);

            return CreatedAtAction("GetVille", new { id = ville.CodeInsee }, ville);
        }

        // DELETE: api/Villes/74000
        [HttpDelete("{insee}")]
        [Authorize(Policy = Policies.admin)]
        public async Task<IActionResult> DeleteVille(string insee)
        {
            if (_villeRepository == null)
            {
                return NotFound();
            }
            var ville = await _villeRepository.GetByInsee(insee);
            if (ville.Value == null)
            {
                return NotFound();
            }

            await _villeRepository.Delete(ville.Value);

            return NoContent();
        }
        /*
        private bool VilleExists(string id)
        {
            return (_context.Villes?.Any(e => e.CodeInsee == id)).GetValueOrDefault();
        }*/
    }
}
