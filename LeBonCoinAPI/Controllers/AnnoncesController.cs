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
    public class AnnoncesController : ControllerBase
    {
        private readonly IRepository<Annonce> repositoryAnnonce;

        public AnnoncesController(IRepository<Annonce> repoAnnonce)
        {
            repositoryAnnonce = repoAnnonce;
        }

        // GET: api/Annonces
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Annonce>>> GetAnnonces()
        {
            var res = await repositoryAnnonce.GetAll();
            if (res == null)
            {
                return NotFound();
            }
            return res;
        }

        // GET: api/Annonces/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Annonce>> GetAnnonce(int id)
        {

            var annonce = await repositoryAnnonce.GetById(id);

            if (annonce == null)
            {
                return NotFound();
            }

            return annonce;
        }

        // PUT: api/Annonces/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Policy = Policies.all)]
        public async Task<IActionResult> PutAnnonce(int id, Annonce annonce)
        {
            if (id != annonce.AnnonceId)
            {
                return BadRequest();
            }

            var annonceToUpdate = await repositoryAnnonce.GetById(id);
            if (annonceToUpdate.Value == null)
            {
                return NotFound();
            }
            else
            {
                await repositoryAnnonce.Update(annonceToUpdate.Value, annonce);
                return NoContent();
            }

        }

        // POST: api/Annonces
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Policy = Policies.all)]
        public async Task<ActionResult<Annonce>> PostAnnonce(Annonce annonce)
        {
            if (await repositoryAnnonce.GetAll() == null)
            {
                return Problem("Entity set 'DataContext.Annonces'  is null.");
            }
            await repositoryAnnonce.Add(annonce);


            return CreatedAtAction("GetAnnonce", new { id = annonce.AnnonceId }, annonce);
        }

        // DELETE: api/Annonces/5
        [HttpDelete("{id}")]
        [Authorize(Policy = Policies.all)]
        public async Task<IActionResult> DeleteAnnonce(int id)
        {
            if (repositoryAnnonce == null)
            {
                return Problem("Repository is null.");
            }
            var annonce = await repositoryAnnonce.GetById(id);
            if (annonce.Value == null)
            {
                return NotFound();
            }

            await repositoryAnnonce.Delete(annonce.Value);

            return NoContent();
        }
    }
}
