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
    public class EntreprisesController : ControllerBase
    {
        private readonly IRepository<Entreprise> repositoryEntreprise;

        public EntreprisesController(IRepository<Entreprise> repoEntreprise)
        {
            repositoryEntreprise = repoEntreprise;
        }

        // GET: api/Entreprises
        [HttpGet]
        [Authorize(Policy = Policies.admin)]
        public async Task<ActionResult<IEnumerable<Entreprise>>> GetEntreprises()
        {
            var res = await repositoryEntreprise.GetAll();
            if (res == null)
            {
                return NotFound();
            }
            return res;
        }

        // GET: api/Entreprises/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Entreprise>> GetEntreprise(int id)
        {

            var entreprise = await repositoryEntreprise.GetById(id);

            if (entreprise == null)
            {
                return NotFound();
            }

            return entreprise;
        }

        // PUT: api/Entreprises/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Policy = Policies.director)]
        public async Task<IActionResult> PutEntreprise(int id, Entreprise entreprise)
        {
            if (id != entreprise.ProfilId)
            {
                return BadRequest();
            }

            var entrepriseToUpdate = await repositoryEntreprise.GetById(id);
            if (entrepriseToUpdate.Value == null)
            {
                return NotFound();
            }
            else
            {
                await repositoryEntreprise.Update(entrepriseToUpdate.Value, entreprise);
                return NoContent();
            }

        }

        // POST: api/Entreprises
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Entreprise>> PostEntreprise(Entreprise entreprise)
        {
            if (repositoryEntreprise == null)
            {
                return Problem("Entity set 'DataContext.Entreprises'  is null.");
            }
            await repositoryEntreprise.Add(entreprise);


            return CreatedAtAction("GetEntreprise", new { id = entreprise.ProfilId }, entreprise);
        }

        // DELETE: api/Entreprises/5
        [HttpDelete("{id}")]
        [Authorize(Policy = Policies.director)]
        public async Task<IActionResult> DeleteEntreprise(int id)
        {
            if (repositoryEntreprise == null)
            {
                return NotFound();
            }
            var entreprise = await repositoryEntreprise.GetById(id);
            if (entreprise.Value == null)
            {
                return NotFound();
            }

            await repositoryEntreprise.Delete(entreprise.Value);

            return NoContent();
        }
    }
}
