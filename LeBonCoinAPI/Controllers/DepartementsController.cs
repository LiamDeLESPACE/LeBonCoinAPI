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
        //private readonly repositoryDepartement repositoryDepartement;
        //private readonly DataContext _context;

        public DepartementsController(IRepositoryDepartement<Departement> dataRepo)
        {
            //repositoryDepartement = departmentManager;
            repositoryDepartement = dataRepo;
        }

        // GET: api/Departements
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Departement>>> GetDepartements()
        {
            /*if (_context.Departements == null)
            {
                return NotFound();
            }
              return await _context.Departements.ToListAsync();*/

            if (repositoryDepartement.GetAll() == null)
            {
                return NotFound();
            }
            return await repositoryDepartement.GetAll();
        }

        // GET: api/Departements/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<Departement>> GetDepartement(string id)
        {
          /*if (_context.Departements == null)
          {
              return NotFound();
          }*/
            var departement = await repositoryDepartement.GetByString(id);
          //var departement = await _context.Departements.FindAsync(id);

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

            var departmentToUpdate = await repositoryDepartement.GetByString(id);

            if (departmentToUpdate == null)
            {
                return NotFound();
            }
            else
            {
                await repositoryDepartement.Update(departmentToUpdate.Value, departement);
                return NoContent();
            }

            /*_context.Entry(departement).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DepartementExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();*/
        }

        // POST: api/Departements
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Policy = Policies.admin)]
        public async Task<ActionResult<Departement>> PostDepartement(Departement departement)
        {
            /*if (_context.Departements == null)
            {
                return Problem("Entity set 'DataContext.Departements'  is null.");
            }
              _context.Departements.Add(departement);
              try
              {
                  await _context.SaveChangesAsync();
              }
              catch (DbUpdateException)
              {
                  if (DepartementExists(departement.DepartementCode))
                  {
                      return Conflict();
                  }
                  else
                  {
                      throw;
                  }
                        return CreatedAtAction("GetDepartement", new { id = departement.DepartementCode }, departement);

              }*/

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await repositoryDepartement.Add(departement);
            return CreatedAtAction("GetDepartement", new { id = departement.DepartementCode }, departement);

        }

        // DELETE: api/Departements/5
        [HttpDelete("{id}")]
        [Authorize(Policy = Policies.admin)]
        public async Task<IActionResult> DeleteDepartement(string id)
        {
            var departement = await repositoryDepartement.GetByString(id);
            if (departement == null)
            {
                return NotFound();
            }
            await repositoryDepartement.Delete(departement.Value);

            return NoContent();
            /*if (_context.Departements == null)
            {
                return NotFound();
            }
            var departement = await _context.Departements.FindAsync(id);
            if (departement == null)
            {
                return NotFound();
            }

            _context.Departements.Remove(departement);
            await _context.SaveChangesAsync();

            return NoContent();*/
        }

        /*private bool DepartementExists(string id)
        {
            return (_context.Departements?.Any(e => e.DepartementCode == id)).GetValueOrDefault();
        }*/
    }
}
