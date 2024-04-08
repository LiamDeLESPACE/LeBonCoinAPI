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
    public class PhotosController : ControllerBase
    {
        private readonly DataContext _context;

        public PhotosController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Photos
        [HttpGet]
        [Authorize(Policy = Policies.admin)]
        public async Task<ActionResult<IEnumerable<Photo>>> GetPhotos()
        {
          if (_context.Photos == null)
          {
              return NotFound();
          }
            return await _context.Photos.ToListAsync();
        }

        // GET: api/Photos/5
        [HttpGet("{idProfil}")]
        [Authorize(Policy = Policies.all)]
        public async Task<ActionResult<Photo>> GetPhotoProfil(int idProfil)
        {
          if (_context.Photos == null)
          {
              return NotFound();
          }
            var photo = await _context.Photos.FirstOrDefaultAsync(x => x.ProfilId == idProfil);

            if (photo == null)
            {
                return NotFound();
            }

            return photo;
        }

        // GET: api/Photos/5
        [HttpGet("{id}")]
        [Authorize(Policy = Policies.all)]
        public async Task<ActionResult<Photo>> GetPhotoP(int id)
        {
            if (_context.Photos == null)
            {
                return NotFound();
            }
            var photo = await _context.Photos.FindAsync(id);

            if (photo == null)
            {
                return NotFound();
            }

            return photo;
        }

        // GET: api/Photos/5
        [HttpGet("{idAnnonce}")]
        [Authorize(Policy = Policies.all)]
        public async Task<ActionResult<IEnumerable<Photo>>> GetPhotosAnnonce(int idAnnonce)
        {
            if (_context.Photos == null)
            {
                return NotFound();
            }
            var photo = await (from s in _context.Photos where s.AnnonceId == idAnnonce select s).ToListAsync();

            if (photo == null)
            {
                return NotFound();
            }

            return photo;
        }

        // PUT: api/Photos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Policy = Policies.all)]
        public async Task<IActionResult> PutPhoto(int id, Photo photo)
        {
            if (id != photo.PhotoId)
            {
                return BadRequest();
            }

            _context.Entry(photo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PhotoExists(id))
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

        // POST: api/Photos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Policy = Policies.all)]
        public async Task<ActionResult<Photo>> PostPhoto(Photo photo)
        {
          if (_context.Photos == null)
          {
              return Problem("Entity set 'DataContext.Photos'  is null.");
          }
            _context.Photos.Add(photo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPhoto", new { id = photo.PhotoId }, photo);
        }

        // DELETE: api/Photos/5
        [HttpDelete("{id}")]
        [Authorize(Policy = Policies.all)]
        public async Task<IActionResult> DeletePhoto(int id)
        {
            if (_context.Photos == null)
            {
                return NotFound();
            }
            var photo = await _context.Photos.FindAsync(id);
            if (photo == null)
            {
                return NotFound();
            }

            _context.Photos.Remove(photo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PhotoExists(int id)
        {
            return (_context.Photos?.Any(e => e.PhotoId == id)).GetValueOrDefault();
        }
    }
}
