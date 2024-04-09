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
    public class FavorisController : ControllerBase
    {
        private readonly IRepositoryFavoris<Favoris> repositoryFavoris;

        public FavorisController(IRepositoryFavoris<Favoris> repoFavoris)
        {
            repositoryFavoris = repoFavoris;
        }

        // GET: api/Favoris
        [HttpGet]
        [Authorize(Policy = Policies.admin)]
        public async Task<ActionResult<IEnumerable<Favoris>>> GetFavoris()
        {
            var res = await repositoryFavoris.GetAll();
            if (res == null)
            {
                return NotFound();
            }
            return res;
        }

        // GET: api/Favoris/5/6
        [HttpGet("{idProfil}/{idAnnonce}")]
        [Authorize(Policy = Policies.all)]
        public async Task<ActionResult<Favoris>> GetFavoris(int idProfil, int idAnnonce)
        {
            var favoris = await repositoryFavoris.GetByIds(idProfil, idAnnonce);

            if (favoris == null)
            {
                return NotFound();
            }

            return favoris;
        }

        // GET: api/Favoris/5
        [HttpGet("{idProfil}")]
        [Authorize(Policy = Policies.all)]
        public async Task<ActionResult<IEnumerable<Favoris>>> GetFavorisOfProfil(int idProfil)
        {
            var favoris = await repositoryFavoris.GetByIdProfil(idProfil);

            if (favoris == null)
            {
                return NotFound();
            }

            return favoris;
        }

        /*// PUT: api/Favoris/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{idProfil}/idReservation")]
        [Authorize(Policy = Policies.all)]
        public async Task<IActionResult> PutFavoris(int idProfil, int idAnnonce, Favoris favoris)
        {
            if (idProfil != favoris.ProfilId || idAnnonce != favoris.AnnonceId)
            {
                return BadRequest();
            }

            var favorisToUpdate = await repositoryFavoris.GetByIds(idProfil, idAnnonce);

            if (favorisToUpdate.Value == null)
            {
                return NotFound();
            }
            else
            {
                await repositoryFavoris.Update(favorisToUpdate.Value, favoris);
                return NoContent();
            }


        }*/

        // POST: api/Favoris
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Policy = Policies.all)]
        public async Task<ActionResult<Favoris>> PostFavoris(Favoris favoris)
        {
            if (await repositoryFavoris.GetAll() == null)
            {
                return Problem("Entity set 'DataContext.Favoris'  is null.");
            }
            await repositoryFavoris.Add(favoris);

            return CreatedAtAction("GetFavoris", new { idProfil = favoris.ProfilId, idAnnonce = favoris.AnnonceId }, favoris);
        }

        // DELETE: api/Favoris/5/6
        [HttpDelete("{idProfil/idAnnonce}")]
        [Authorize(Policy = Policies.all)]
        public async Task<IActionResult> DeleteFavoris(int idProfil, int idAnnonce)
        {
            if (await repositoryFavoris.GetAll() == null)
            {
                return NotFound();
            }
            var favoris = await repositoryFavoris.GetByIds(idProfil, idAnnonce);
            if (favoris.Value == null)
            {
                return NotFound();
            }

            await repositoryFavoris.Delete(favoris.Value);

            return NoContent();
        }
    }
}
