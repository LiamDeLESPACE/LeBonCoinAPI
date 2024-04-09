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
    public class ParticuliersController : ControllerBase
    {
        private readonly IRepository<Particulier> repositoryParticulier;

        public ParticuliersController(IRepository<Particulier> repoParticulier)
        {
            repositoryParticulier = repoParticulier;
        }

        // GET: api/Particuliers
        [HttpGet]
        [Authorize(Policy = Policies.admin)]
        public async Task<ActionResult<IEnumerable<Particulier>>> GetParticuliers()
        {
            var res = await repositoryParticulier.GetAll();
            if (res == null)
            {
                return NotFound();
            }
            return res;
        }

        // GET: api/Particuliers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Particulier>> GetParticulier(int id)
        {

            var particulier = await repositoryParticulier.GetById(id);

            if (particulier == null)
            {
                return NotFound();
            }

            return particulier;
        }

        // PUT: api/Particuliers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Policy = Policies.human)]
        public async Task<IActionResult> PutParticulier(int id, Particulier particulier)
        {
            if (id != particulier.ProfilId)
            {
                return BadRequest();
            }

            var particulierToUpdate = await repositoryParticulier.GetById(id);
            if (particulierToUpdate.Value == null)
            {
                return NotFound();
            }
            else
            {
                await repositoryParticulier.Update(particulierToUpdate.Value, particulier);
                return NoContent();
            }

        }

        // POST: api/Particuliers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Particulier>> PostParticulier(Particulier particulier)
        {
            if (await repositoryParticulier.GetAll() == null)
            {
                return Problem("Entity set 'DataContext.Particuliers'  is null.");
            }
            await repositoryParticulier.Add(particulier);


            return CreatedAtAction("GetParticulier", new { id = particulier.ProfilId }, particulier);
        }

        // DELETE: api/Particuliers/5
        [HttpDelete("{id}")]
        [Authorize(Policy = Policies.human)]
        public async Task<IActionResult> DeleteParticulier(int id)
        {
            if (repositoryParticulier == null)
            {
                return NotFound();
            }
            var particulier = await repositoryParticulier.GetById(id);
            if (particulier.Value == null)
            {
                return NotFound();
            }

            await repositoryParticulier.Delete(particulier.Value);

            return NoContent();
        }
    }
}
