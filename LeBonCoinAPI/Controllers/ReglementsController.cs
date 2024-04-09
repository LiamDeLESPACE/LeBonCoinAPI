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
    [Authorize(Policy = Policies.admin)]
    public class ReglementsController : ControllerBase
    {
        private readonly IRepositoryReglement<Reglement> repositoryReglement;

        public ReglementsController(IRepositoryReglement<Reglement> repoReglement)
        {
            repositoryReglement = repoReglement;
        }

        // GET: api/Reglements
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Reglement>>> GetReglements()
        {
            var res = await repositoryReglement.GetAll();
            if (res == null)
            {
                return NotFound();
            }
            return res;
        }

        // GET: api/Reglements/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Reglement>> GetReglement(string id)
        {

            var reglement = await repositoryReglement.GetByString(id);

            if (reglement == null)
            {
                return NotFound();
            }

            return reglement;
        }

        // PUT: api/Reglements/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReglement(string id, Reglement reglement)
        {
            if (id != reglement.ReglementId)
            {
                return BadRequest();
            }

            var reglementToUpdate = await repositoryReglement.GetByString(id);
            if (reglementToUpdate.Value == null)
            {
                return NotFound();
            }
            else
            {
                await repositoryReglement.Update(reglementToUpdate.Value, reglement);
                return NoContent();
            }

        }

        // POST: api/Reglements
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Reglement>> PostReglement(Reglement reglement)
        {
            if (repositoryReglement == null)
            {
                return Problem("Entity set 'DataContext.Reglements'  is null.");
            }
            await repositoryReglement.Add(reglement);


            return CreatedAtAction("GetReglement", new { id = reglement.ReglementId }, reglement);
        }

        // DELETE: api/Reglements/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReglement(string id)
        {
            if (repositoryReglement == null)
            {
                return NotFound();
            }
            var reglement = await repositoryReglement.GetByString(id);
            if (reglement.Value == null)
            {
                return NotFound();
            }

            await repositoryReglement.Delete(reglement.Value);

            return NoContent();
        }
    }
}
