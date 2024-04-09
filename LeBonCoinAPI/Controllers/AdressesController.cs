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
    public class AdressesController : ControllerBase
    {
        private readonly IRepository<Adresse> repositoryAdresse;

        public AdressesController(IRepository<Adresse> repoAdresse)
        {
            repositoryAdresse = repoAdresse;
        }

        // GET: api/Adresses
        [HttpGet]
        [Authorize(Policy = Policies.admin)]
        public async Task<ActionResult<IEnumerable<Adresse>>> GetAdresses()
        {
            var res = await repositoryAdresse.GetAll();
            if (res == null)
            {
                return NotFound();
            }
            return res;
        }

        // GET: api/Adresses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Adresse>> GetAdresse(int id)
        {

            var adresse = await repositoryAdresse.GetById(id);

            if (adresse == null)
            {
                return NotFound();
            }

            return adresse;
        }

        // PUT: api/Adresses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Policy = Policies.all)]
        public async Task<IActionResult> PutAdresse(int id, Adresse adresse)
        {
            if (id != adresse.AdresseId)
            {
                return BadRequest();
            }

            var adresseToUpdate = await repositoryAdresse.GetById(id);
            if (adresseToUpdate.Value == null)
            {
                return NotFound();
            }
            else
            {
                await repositoryAdresse.Update(adresseToUpdate.Value, adresse);
                return NoContent();
            }

        }

        // POST: api/Adresses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Policy = Policies.all)]
        public async Task<ActionResult<Adresse>> PostAdresse(Adresse adresse)
        {
            if (await repositoryAdresse.GetAll() == null)
            {
                return Problem("Entity set 'DataContext.Adresses'  is null.");
            }
            await repositoryAdresse.Add(adresse);


            return CreatedAtAction("GetAdresse", new { id = adresse.AdresseId }, adresse);
        }

        // DELETE: api/Adresses/5
        [HttpDelete("{id}")]
        [Authorize(Policy = Policies.all)]
        public async Task<IActionResult> DeleteAdresse(int id)
        {
            if (repositoryAdresse == null)
            {
                return NotFound();
            }
            var adresse = await repositoryAdresse.GetById(id);
            if (adresse.Value == null)
            {
                return NotFound();
            }

            await repositoryAdresse.Delete(adresse.Value);

            return NoContent();
        }
    }
}
