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

namespace LeBonCoinAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentairesController : ControllerBase
    {
        private readonly DataContext _context;

        public CommentairesController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Commentaires
        [HttpGet]
        [Authorize(Policy = Policies.admin)]
        public async Task<ActionResult<IEnumerable<Commentaire>>> GetCommentaires()
        {
          if (_context.Commentaires == null)
          {
              return NotFound();
          }
            return await _context.Commentaires.ToListAsync();
        }

        // GET: api/Commentaires/5/6
        [HttpGet("{idReservation}/{idProfil}")]
        [Authorize(Policy = Policies.all)]
        public async Task<ActionResult<Commentaire>> GetCommentaire(int idReservation, int idProfil)
        {
          if (_context.Commentaires == null)
          {
              return NotFound();
          }
            var commentaire = await (from s in _context.Commentaires where s.ProfilId == idProfil && s.ReservationId == idReservation select s).FirstOrDefaultAsync();

            if (commentaire == null)
            {
                return NotFound();
            }

            return commentaire;
        }

        // GET: api/Commentaires/5
        [HttpGet("{idProfil}")]
        [Authorize(Policy = Policies.all)]
        public async Task<ActionResult<List<Commentaire>>> GetCommentaireOfProfil(int idProfil)
        {
            if (_context.Commentaires == null)
            {
                return NotFound();
            }
            var commentaire = await (from s in _context.Commentaires where s.ProfilId == idProfil select s).ToListAsync();

            if (commentaire == null)
            {
                return NotFound();
            }

            return commentaire;
        }

        // GET: api/Commentaires/5
        [HttpGet("{idReservation}")]
        [Authorize(Policy = Policies.all)]
        public async Task<ActionResult<List<Commentaire>>> GetCommentaireOfReservation(int idReservation)
        {
            if (_context.Commentaires == null)
            {
                return NotFound();
            }
            var commentaire = await (from s in _context.Commentaires where s.ReservationId == idReservation select s).ToListAsync();

            if (commentaire == null)
            {
                return NotFound();
            }

            return commentaire;
        }

        // PUT: api/Commentaires/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{idProfil}/idReservation")]
        [Authorize(Policy = Policies.all)]
        public async Task<IActionResult> PutCommentaire(int idProfil, int idReservation, Commentaire commentaire)
        {
            if (idProfil != commentaire.ProfilId || idReservation != commentaire.ReservationId)
            {
                return BadRequest();
            }

            _context.Entry(commentaire).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CommentaireExists(idProfil, idReservation))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Commentaires
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Policy = Policies.all)]
        public async Task<ActionResult<Commentaire>> PostCommentaire(Commentaire commentaire)
        {
          if (_context.Commentaires == null)
          {
              return Problem("Entity set 'DataContext.Commentaires'  is null.");
          }
            _context.Commentaires.Add(commentaire);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CommentaireExists(commentaire.ProfilId, commentaire.ReservationId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetCommentaire", new { idProfil = commentaire.ProfilId, idReservation = commentaire.ReservationId }, commentaire);
        }

        // DELETE: api/Commentaires/5
        [HttpDelete("{id}")]
        [Authorize(Policy = Policies.all)]
        public async Task<IActionResult> DeleteCommentaire(int idProfil, int idReservation)
        {
            if (_context.Commentaires == null)
            {
                return NotFound();
            }
            var commentaire = await _context.Commentaires.FirstOrDefaultAsync(x => x.ProfilId == idProfil && x.ReservationId == idReservation);
            if (commentaire == null)
            {
                return NotFound();
            }

            _context.Commentaires.Remove(commentaire);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CommentaireExists(int idProfil, int idReservation)
        {
            return (_context.Commentaires?.Any(e => e.ProfilId == idProfil && e.ReservationId == idReservation)).GetValueOrDefault();
        }
    }
}
