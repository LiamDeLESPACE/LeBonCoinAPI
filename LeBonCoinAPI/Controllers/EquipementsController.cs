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
using LeBonCoinAPI.Models.Repository;

namespace LeBonCoinAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquipementsController : ControllerBase
    {
        private readonly IRepository<Equipement> repositoryEquipement;

        public EquipementsController(IRepository<Equipement> repoEquipement)
        {
            repositoryEquipement = repoEquipement;
        }

        // GET: api/Equipements
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Equipement>>> GetEquipements()
        {
            var res = await repositoryEquipement.GetAll();
            if (res == null)
            {
                return NotFound();
            }
            return res;
        }

        // GET: api/Equipements/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Equipement>> GetEquipement(int id)
        {

            var equipement = await repositoryEquipement.GetById(id);

            if (equipement == null)
            {
                return NotFound();
            }

            return equipement;
        }

        // PUT: api/Equipements/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEquipement(int id, Equipement equipement)
        {
            if (id != equipement.EquipementId)
            {
                return BadRequest();
            }

            var equipementToUpdate = await repositoryEquipement.GetById(id);
            if (equipementToUpdate == null)
            {
                return NotFound();
            }
            else
            {
                await repositoryEquipement.Update(equipementToUpdate.Value, equipement);
                return NoContent();
            }

        }

        // POST: api/Equipements
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Equipement>> PostEquipement(Equipement equipement)
        {
            if (await repositoryEquipement.GetAll() == null)
            {
                return Problem("Entity set 'DataContext.Equipements'  is null.");
            }
            await repositoryEquipement.Add(equipement);


            return CreatedAtAction("GetEquipement", new { id = equipement.EquipementId }, equipement);
        }

        // DELETE: api/Equipements/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEquipement(int id)
        {
            if (await repositoryEquipement.GetAll() == null)
            {
                return NotFound();
            }
            var equipement = await repositoryEquipement.GetById(id);
            if (equipement == null)
            {
                return NotFound();
            }

            await repositoryEquipement.Delete(equipement.Value);

            return NoContent();
        }
    }
}
