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
    public class TypeEquipementsController : ControllerBase
    {
        private readonly IRepository<TypeEquipement> _teRepository;

        public TypeEquipementsController(IRepository<TypeEquipement> repository)
        {
            _teRepository = repository;
        }

        // GET: api/TypeEquipements
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TypeEquipement>>> GetTypeEquipements()
        {

          if (_teRepository == null)
          {
              return NotFound();
          }
            return await _teRepository.GetAll();
        }

        // GET: api/TypeEquipements/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TypeEquipement>> GetTypeEquipement(int id)
        {
          if (_teRepository == null)
          {
              return NotFound();
          }
            var typeEquipement = await _teRepository.GetById(id);

            if (typeEquipement == null)
            {
                return NotFound();
            }

            return typeEquipement;
        }

        // PUT: api/TypeEquipements/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTypeEquipement(int id, TypeEquipement typeEquipement)
        {
            if (id != typeEquipement.TypeEquipementId)
            {
                return BadRequest();
            }

            var te = await _teRepository.GetById(id);

            if (te.Value == null)
                return NotFound();

            await _teRepository.Update(te.Value, typeEquipement);

            return NoContent();
        }

        // POST: api/TypeEquipements
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TypeEquipement>> PostTypeEquipement(TypeEquipement typeEquipement)
        {
          if (_teRepository == null)
          {
              return Problem("Repository is null.");
          }
            
          await _teRepository.Add(typeEquipement);

            return CreatedAtAction("GetTypeEquipement", new { id = typeEquipement.TypeEquipementId }, typeEquipement);
        }

        // DELETE: api/TypeEquipements/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTypeEquipement(int id)
        {
            if (_teRepository == null)
            {
                return Problem("Repository is null.");
            }
            var typeEquipement = await _teRepository.GetById(id);
            if (typeEquipement.Value == null)
            {
                return NotFound();
            }

            await _teRepository.Delete(typeEquipement.Value);

            return NoContent();
        }

        private bool TypeEquipementExists(int id)
        {
            return (_context.TypeEquipements?.Any(e => e.TypeEquipementId == id)).GetValueOrDefault();
        }
    }
}
