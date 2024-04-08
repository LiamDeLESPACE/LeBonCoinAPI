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
    public class PossedeEquipementsController : ControllerBase
    {
        private readonly IRepositoryPossedeEquipement<PossedeEquipement> _repositoryPossedeEquipement;

        public PossedeEquipementsController(IRepositoryPossedeEquipement<PossedeEquipement> repository)
        {
            _repositoryPossedeEquipement = repository;
        }

        // GET: api/PossedeEquipements
        [HttpGet]
        [Authorize(Policy = Policies.admin)]
        public async Task<ActionResult<IEnumerable<PossedeEquipement>>> GetPossedeEquipements()
        {

            if (_repositoryPossedeEquipement == null)
            {
                return NotFound();
            }
            return await _repositoryPossedeEquipement.GetAll();
        }

        // GET: api/PossedeEquipements/5
        [HttpGet("{idAnnonce}/{idProfil}")]
        [Authorize(Policy = Policies.admin)]
        public async Task<ActionResult<PossedeEquipement>> GetPossedeEquipementByIds(int idAnnonce, int idEquipement)
        {
            if (_repositoryPossedeEquipement == null)
            {
                return NotFound();
            }
            var possedeEquipement = await _repositoryPossedeEquipement.GetByIds(idAnnonce, idEquipement);

            if (possedeEquipement == null)
            {
                return NotFound();
            }

            return possedeEquipement;
        }

        // GET: api/PossedeEquipements/5
        [HttpGet("{idAnnonce}")]
        [Authorize(Policy = Policies.all)]
        public async Task<ActionResult<IEnumerable<PossedeEquipement>>> GetPossedeEquipementByIdAnnonce(int idAnnonce)
        {
            if (_repositoryPossedeEquipement == null)
            {
                return NotFound();
            }
            var possedeEquipement = await _repositoryPossedeEquipement.GetByIdAnnonce(idAnnonce);

            if (possedeEquipement == null)
            {
                return NotFound();
            }

            return possedeEquipement;
        }

        // GET: api/PossedeEquipements/5
        [HttpGet("{idProfil}")]
        [Authorize(Policy = Policies.all)]
        public async Task<ActionResult<IEnumerable<PossedeEquipement>>> GetPossedeEquipementByIdEquipement(int idEquipement)
        {
            if (_repositoryPossedeEquipement == null)
            {
                return NotFound();
            }
            var possedeEquipement = await _repositoryPossedeEquipement.GetByIdEquipement(idEquipement);

            if (possedeEquipement == null)
            {
                return NotFound();
            }

            return possedeEquipement;
        }

        /*
        // PUT: api/PossedeEquipements/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Policy = Policies.admin)]
        public async Task<IActionResult> PutPossedeEquipement(int idAnnonce, int idProfil, PossedeEquipement possedeEquipement)
        {
            if (idAnnonce != possedeEquipement.AnnonceId || idProfil != possedeEquipement.ProfilId)
            {
                return BadRequest();
            }

            _context.Entry(possedeEquipement).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PossedeEquipementExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }*/

        // POST: api/PossedeEquipements
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Policy = Policies.all)]
        public async Task<ActionResult<PossedeEquipement>> PostPossedeEquipement(PossedeEquipement possedeEquipement)
        {
            if (_repositoryPossedeEquipement == null)
            {
                return Problem("Repository is null.");
            }
            await _repositoryPossedeEquipement.Add(possedeEquipement);

            return CreatedAtAction("GetPossedeEquipement", new { idAnnonce = possedeEquipement.AnnonceId, idEquipement = possedeEquipement.EquipementId }, possedeEquipement);
        }

        // DELETE: api/PossedeEquipements/5
        [HttpDelete("{idAnnonce}/{idProfil}")]
        [Authorize(Policy = Policies.admin)]
        public async Task<IActionResult> DeletePossedeEquipement(int idAnnonce, int idProfil)
        {
            if (_repositoryPossedeEquipement == null)
            {
                return NotFound();
            }
            var possedeEquipement = await _repositoryPossedeEquipement.GetByIds(idAnnonce, idProfil);
            if (possedeEquipement.Value == null)
            {
                return NotFound();
            }

            await _repositoryPossedeEquipement.Delete(possedeEquipement.Value);

            return NoContent();
        }
    }
}
