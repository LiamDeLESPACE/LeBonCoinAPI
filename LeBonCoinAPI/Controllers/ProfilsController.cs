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
    public class ProfilsController : ControllerBase
    {

        private readonly IRepository<Profil> repositoryProfil;

        public ProfilsController(IRepository<Profil> repoProfil)
        {
            repositoryProfil = repoProfil;
        }

        // GET: api/Profils
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Profil>>> GetProfils()
        {
            var res = await repositoryProfil.GetAll();
            if (res == null)
            {
                return NotFound();
            }
            return res;
        }

        // GET: api/Profils/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Profil>> GetProfil(int id)
        {

            var profil = await repositoryProfil.GetById(id);

            if (profil == null)
            {
                return NotFound();
            }

            return profil;
        }

        // PUT: api/Profils/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProfil(int id, Profil profil)
        {
            if (id != profil.ProfilId)
            {
                return BadRequest();
            }

            var profilToUpdate = await repositoryProfil.GetById(id);
            if (profilToUpdate.Value == null)
            {
                return NotFound();
            }
            else
            {
                await repositoryProfil.Update(profilToUpdate.Value, profil);
                return NoContent();
            }

        }

        // POST: api/Profils
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Profil>> PostProfil(Profil profil)
        {
            if (await repositoryProfil.GetAll() == null)
            {
                return Problem("Entity set 'DataContext.Profils'  is null.");
            }
            await repositoryProfil.Add(profil);


            return CreatedAtAction("GetProfil", new { id = profil.ProfilId }, profil);
        }

        // DELETE: api/Profils/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProfil(int id)
        {
            if (await repositoryProfil.GetAll() == null)
            {
                return NotFound();
            }
            var profil = await repositoryProfil.GetById(id);
            if (profil.Value == null)
            {
                return NotFound();
            }

            await repositoryProfil.Delete(profil.Value);

            return NoContent();
        }
    }
}
