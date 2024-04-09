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
    public class SecteurActivitesController : ControllerBase
    {
        private readonly IRepository<SecteurActivite> repositorySecteurActivite;

        public SecteurActivitesController(IRepository<SecteurActivite> repoSecteurActivite)
        {
            repositorySecteurActivite = repoSecteurActivite;
        }

        // GET: api/SecteurActivites
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SecteurActivite>>> GetSecteurActivites()
        {
            var res = await repositorySecteurActivite.GetAll();
            if (res == null)
            {
                return NotFound();
            }
            return res;
        }

        // GET: api/SecteurActivites/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SecteurActivite>> GetSecteurActivite(int id)
        {

            var secteurActivite = await repositorySecteurActivite.GetById(id);

            if (secteurActivite == null)
            {
                return NotFound();
            }

            return secteurActivite;
        }

        // PUT: api/SecteurActivites/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSecteurActivite(int id, SecteurActivite secteurActivite)
        {
            if (id != secteurActivite.SecteurId)
            {
                return BadRequest();
            }

            var secteurActiviteToUpdate = await repositorySecteurActivite.GetById(id);
            if (secteurActiviteToUpdate == null)
            {
                return NotFound();
            }
            else
            {
                await repositorySecteurActivite.Update(secteurActiviteToUpdate.Value, secteurActivite);
                return NoContent();
            }

        }

        // POST: api/SecteurActivites
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SecteurActivite>> PostSecteurActivite(SecteurActivite secteurActivite)
        {
            if (await repositorySecteurActivite.GetAll() == null)
            {
                return Problem("Entity set 'DataContext.SecteurActivites'  is null.");
            }
            await repositorySecteurActivite.Add(secteurActivite);


            return CreatedAtAction("GetSecteurActivite", new { id = secteurActivite.SecteurId }, secteurActivite);
        }

        // DELETE: api/SecteurActivites/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSecteurActivite(int id)
        {
            if (await repositorySecteurActivite.GetAll() == null)
            {
                return NotFound();
            }
            var secteurActivite = await repositorySecteurActivite.GetById(id);
            if (secteurActivite.Value == null)
            {
                return NotFound();
            }

            await repositorySecteurActivite.Delete(secteurActivite.Value);

            return NoContent();
        }
    }
}
