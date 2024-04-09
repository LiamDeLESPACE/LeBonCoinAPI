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

        // GET: api/Departements/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Departement>> GetDepartement(string id)
        {

            var departement = await repositoryDepartement.GetByString(id);

            if (departement == null)
            {
                return NotFound();
            }

            return departement;
        }

        // PUT: api/Departements/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Policy = Policies.admin)]
        public async Task<IActionResult> PutDepartement(string id, Departement departement)
        {
            if (id != departement.DepartementCode)
            {
                return BadRequest();
            }

            var departementToUpdate = await repositoryDepartement.GetByString(id);
            if (departementToUpdate == null)
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
            if (await repositoryDepartement.GetAll() == null)
            {
                return Problem("Entity set 'DataContext.Departements'  is null.");
            }
            await repositoryDepartement.Add(departement);


            return CreatedAtAction("GetDepartement", new { id = departement.DepartementCode }, departement);
        }

        // DELETE: api/Departements/5
        [HttpDelete("{id}")]
        [Authorize(Policy = Policies.admin)]
        public async Task<IActionResult> DeleteDepartement(string id)
        {
            if (repositoryDepartement == null)
            {
                return NotFound();
            }
            var departement = await repositoryDepartement.GetByString(id);
            if (departement == null)
            {
                return NotFound();
            }

            await repositoryDepartement.Delete(departement.Value);

            return NoContent();
        }
    }
}
