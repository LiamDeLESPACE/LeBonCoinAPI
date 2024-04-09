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
    public class PhotosController : ControllerBase
    {
        private readonly IRepositoryPhoto<Photo> repositoryPhoto;

        public PhotosController(IRepositoryPhoto<Photo> repoPhoto)
        {
            repositoryPhoto = repoPhoto;
        }

        // GET: api/Photos
        [HttpGet]
        [Authorize(Policy = Policies.admin)]
        public async Task<ActionResult<IEnumerable<Photo>>> GetPhotos()
        {
            var res = await repositoryPhoto.GetAll();
            if (res == null)
            {
                return NotFound();
            }
            return res;
        }

        // GET: api/Photos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Photo>> GetPhoto(int id)
        {

            var photo = await repositoryPhoto.GetById(id);

            if (photo == null)
            {
                return NotFound();
            }

            return photo;
        }

        // GET: api/Photos/p/5
        [HttpGet("p/{idProfil}")]
        public async Task<ActionResult<Photo>> GetPhotoProfil(int idProfil)
        {

            var photo = await repositoryPhoto.GetByIdProfil(idProfil);

            if (photo == null)
            {
                return NotFound();
            }

            return photo;
        }

        // GET: api/Photos/a/5
        [HttpGet("a/{idAnnonce}")]
        public async Task<ActionResult<IEnumerable<Photo>>> GetPhotosAnnonce(int idAnnonce)
        {

            var photo = await repositoryPhoto.GetByIdAnnonce(idAnnonce);

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

            var photoToUpdate = await repositoryPhoto.GetById(id);
            if (photoToUpdate.Value == null)
            {
                return NotFound();
            }
            else
            {
                await repositoryPhoto.Update(photoToUpdate.Value, photo);
                return NoContent();
            }

        }

        // POST: api/Photos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Policy = Policies.all)]
        public async Task<ActionResult<Photo>> PostPhoto(Photo photo)
        {
            if (await repositoryPhoto.GetAll() == null)
            {
                return Problem("Entity set 'DataContext.Photos'  is null.");
            }
            await repositoryPhoto.Add(photo);


            return CreatedAtAction("GetPhoto", new { id = photo.PhotoId }, photo);
        }

        // DELETE: api/Photos/5
        [HttpDelete("{id}")]
        [Authorize(Policy = Policies.all)]
        public async Task<IActionResult> DeletePhoto(int id)
        {
            if (await repositoryPhoto.GetAll() == null)
            {
                return NotFound();
            }
            var photo = await repositoryPhoto.GetById(id);
            if (photo.Value == null)
            {
                return NotFound();
            }

            await repositoryPhoto.Delete(photo.Value);

            return NoContent();
        }
    }
}
