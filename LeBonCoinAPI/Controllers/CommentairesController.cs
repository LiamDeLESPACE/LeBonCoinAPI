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
    public class CommentairesController : ControllerBase
    {
        private readonly IRepositoryCommentaire<Commentaire> repositoryCommentaire;

        public CommentairesController(IRepositoryCommentaire<Commentaire> repoCommentaire)
        {
            repositoryCommentaire = repoCommentaire;
        }

        // GET: api/Commentaires
        [HttpGet]
        [Authorize(Policy = Policies.admin)]
        public async Task<ActionResult<IEnumerable<Commentaire>>> GetCommentaires()
        {
            var res = await repositoryCommentaire.GetAll();
            if (res == null)
            {
                return NotFound();
            }
            return res;
        }

        // GET: api/Commentaires/5/6
        [HttpGet("{idReservation}/{idProfil}")]
        [Authorize(Policy = Policies.all)]
        public async Task<ActionResult<Commentaire>> GetCommentaire(int idReservation, int idProfil)
        {
            var commentaire = await repositoryCommentaire.GetByIds(idReservation, idProfil);

            if (commentaire == null)
            {
                return NotFound();
            }

            return commentaire;
        }

        // GET: api/Commentaires/p/5
        [HttpGet("p/{idProfil}")]
        [Authorize(Policy = Policies.all)]
        public async Task<ActionResult<IEnumerable<Commentaire>>> GetCommentaireOfProfil(int idProfil)
        {
            var commentaire = await repositoryCommentaire.GetByIdProfil(idProfil);

            if (commentaire == null)
            {
                return NotFound();
            }

            return commentaire;
        }

        // GET: api/Commentaires/r/5
        [HttpGet("r/{idReservation}")]
        [Authorize(Policy = Policies.all)]
        public async Task<ActionResult<IEnumerable<Commentaire>>> GetCommentaireOfReservation(int idReservation)
        {
            
            var commentaire = await repositoryCommentaire.GetByIdReservation(idReservation);

            if (commentaire == null)
            {
                return NotFound();
            }

            return commentaire;
        }

        // PUT: api/Commentaires/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{idProfil}/{idReservation}")]
        [Authorize(Policy = Policies.all)]
        public async Task<IActionResult> PutCommentaire(int idProfil, int idReservation, Commentaire commentaire)
        {
            if (idProfil != commentaire.ProfilId || idReservation != commentaire.ReservationId)
            {
                return BadRequest();
            }

            var commentaireToUpdate = await repositoryCommentaire.GetByIds(idReservation, idProfil);

            if (commentaireToUpdate.Value == null)
            {
                return NotFound();
            }
            else
            {
                await repositoryCommentaire.Update(commentaireToUpdate.Value, commentaire);
                return NoContent();
            }


        }

        // POST: api/Commentaires
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Policy = Policies.all)]
        public async Task<ActionResult<Commentaire>> PostCommentaire(Commentaire commentaire)
        {
          if (await repositoryCommentaire.GetAll() == null)
          {
              return Problem("Entity set 'DataContext.Commentaires'  is null.");
          }
            await repositoryCommentaire.Add(commentaire);

            return CreatedAtAction("GetCommentaire", new { idProfil = commentaire.ProfilId, idReservation = commentaire.ReservationId }, commentaire);
        }

        // DELETE: api/Commentaires/5
        [HttpDelete("{idProfil}/{idReservation}")]
        [Authorize(Policy = Policies.all)]
        public async Task<IActionResult> DeleteCommentaire(int idProfil, int idReservation)
        {
            if (await repositoryCommentaire.GetAll() == null)
            {
                return NotFound();
            }
            var commentaire = await repositoryCommentaire.GetByIds(idReservation, idProfil);
            if (commentaire.Value == null)
            {
                return NotFound();
            }

            await repositoryCommentaire.Delete(commentaire.Value);

            return NoContent();
        }

        
    }
}
