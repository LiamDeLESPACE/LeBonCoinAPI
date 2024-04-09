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
using LeBonCoinAPI.DataManager;
using NuGet.Protocol.Core.Types;
using LeBonCoinAPI.Models.Repository;

namespace LeBonCoinAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartementsController : ControllerBase
    {
        private readonly IRepositoryDepartement<Departement> repositoryDepartement;

        public DepartementsController(IRepositoryDepartement<Departement> repoDepartement)
        {
            repositoryDepartement = repoDepartement;
        }

        // GET: api/Departements
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Departement>>> GetDepartements()
        {
            var res = await repositoryDepartement.GetAll();
            if (res == null)
            {
                return NotFound();
            }
            return res;
        }

        // GET: api/Departements/Haute-Savoie
        [HttpGet("n/{nomDepartement}")]
        public async Task<ActionResult<Departement>> GetDepartement(string nomDepartement)
        {

            var departement = await repositoryDepartement.GetByString(nomDepartement);

            if (departement == null)
            {
                return NotFound();
            }

            return departement;
        }

        // GET: api/Departements/74
        [HttpGet("c/{codeDepartement}")]
        public async Task<ActionResult<Departement>> GetDepartementByCode(string codeDepartement)
        {

            var departement = await repositoryDepartement.GetByCode(codeDepartement);

            if (departement == null)
            {
                return NotFound();
            }

            return departement;
        }

        // PUT: api/Departements/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{codeDepartement}")]
        [Authorize(Policy = Policies.admin)]
        public async Task<IActionResult> PutDepartement(string codeDepartement, Departement departement)
        {
            if (codeDepartement != departement.DepartementCode)
            {
                return BadRequest();
            }

            var departementToUpdate = await repositoryDepartement.GetByCode(codeDepartement);
            if (departementToUpdate.Value == null)
            {
                return NotFound();
            }
            else
            {
                await repositoryDepartement.Update(departementToUpdate.Value, departement);
                return NoContent();
            }

        }

        // POST: api/Departements
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Policy = Policies.all)]
        public async Task<ActionResult<Departement>> PostDepartement(Departement departement)
        {
            if (repositoryDepartement == null)
            {
                return Problem("Entity set 'DataContext.Departements'  is null.");
            }
            await repositoryDepartement.Add(departement);


            return CreatedAtAction("GetDepartement", new { id = departement.DepartementCode }, departement);
        }

        // DELETE: api/Departements/5
        [HttpDelete("{codeDepartement}")]
        [Authorize(Policy = Policies.admin)]
        public async Task<IActionResult> DeleteDepartement(string codeDepartement)
        {
            if (repositoryDepartement == null)
            {
                return NotFound();
            }
            var departement = await repositoryDepartement.GetByCode(codeDepartement);
            if (departement.Value == null)
            {
                return NotFound();
            }

            await repositoryDepartement.Delete(departement.Value);

            return NoContent();
        }
    }
}
