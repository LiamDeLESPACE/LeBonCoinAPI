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
    public class TypeLogementsController : ControllerBase
    {
        private readonly IRepository<TypeLogement> _tlRepository;

        public TypeLogementsController(IRepository<TypeLogement> repository)
        {
            _tlRepository = repository;
        }

        // GET: api/TypeLogements
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TypeLogement>>> GetTypeLogements()
        {

          if (_tlRepository == null)
          {
              return NotFound();
          }
            return await _tlRepository.GetAll();
        }

        // GET: api/TypeLogements/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TypeLogement>> GetTypeLogement(int id)
        {
          if (_tlRepository == null)
          {
              return NotFound();
          }
            var typeLogement = await _tlRepository.GetById(id);

            if (typeLogement == null)
            {
                return NotFound();
            }

            return typeLogement;
        }

        // PUT: api/TypeLogements/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTypeLogement(int id, TypeLogement typeLogement)
        {
            if (id != typeLogement.TypeLogementId)
            {
                return BadRequest();
            }

            var tl = await _tlRepository.GetById(id);

            if (tl.Value == null)
                return NotFound();

            await _tlRepository.Update(tl.Value, typeLogement);

            return NoContent();
        }

        // POST: api/TypeLogements
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TypeLogement>> PostTypeLogement(TypeLogement typeLogement)
        {
          if (_tlRepository == null)
          {
              return Problem("Repository is null.");
          }
            await _tlRepository.Add(typeLogement);

            return CreatedAtAction("GetTypeLogement", new { id = typeLogement.TypeLogementId }, typeLogement);
        }

        // DELETE: api/TypeLogements/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTypeLogement(int id)
        {
            if (_tlRepository == null)
            {
                return Problem("Repository is null.");
            }
            var typeLogement = await _tlRepository.GetById(id);
            if (typeLogement.Value == null)
            {
                return NotFound();
            }

            await _tlRepository.Delete(typeLogement.Value);

            return NoContent();
        }

        /*private bool TypeLogementExists(int id)
        {
            return (_context.TypeLogements?.Any(e => e.TypeLogementId == id)).GetValueOrDefault();
        }*/
    }
}
