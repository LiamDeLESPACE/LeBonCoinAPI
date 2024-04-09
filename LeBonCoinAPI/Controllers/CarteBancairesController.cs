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
    public class CarteBancairesController : ControllerBase
    {
        private readonly IRepository<CarteBancaire> repositoryCarteBancaire;

        public CarteBancairesController(IRepository<CarteBancaire> repoCarteBancaire)
        {
            repositoryCarteBancaire = repoCarteBancaire;
        }

        // GET: api/CarteBancaires
        [HttpGet]
        [Authorize(Policy = Policies.admin)]
        public async Task<ActionResult<IEnumerable<CarteBancaire>>> GetCarteBancaires()
        {
            var res = await repositoryCarteBancaire.GetAll();
            if (res == null)
            {
                return NotFound();
            }
            return res;
        }

        // GET: api/CarteBancaires/5
        [HttpGet("{id}")]
        [Authorize(Policy = Policies.human)]
        public async Task<ActionResult<CarteBancaire>> GetCarteBancaire(int id)
        {

            var carteBancaire = await repositoryCarteBancaire.GetById(id);

            if (carteBancaire == null)
            {
                return NotFound();
            }

            return carteBancaire;
        }

        // PUT: api/CarteBancaires/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Policy = Policies.human)]
        public async Task<IActionResult> PutCarteBancaire(int id, CarteBancaire carteBancaire)
        {
            if (id != carteBancaire.CarteId)
            {
                return BadRequest();
            }

            var carteBancaireToUpdate = await repositoryCarteBancaire.GetById(id);
            if (carteBancaireToUpdate.Value == null)
            {
                return NotFound();
            }
            else
            {
                await repositoryCarteBancaire.Update(carteBancaireToUpdate.Value, carteBancaire);
                return NoContent();
            }

        }

        // POST: api/CarteBancaires
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Policy = Policies.human)]
        public async Task<ActionResult<CarteBancaire>> PostCarteBancaire(CarteBancaire carteBancaire)
        {
            if (await repositoryCarteBancaire.GetAll() == null)
            {
                return Problem("Entity set 'DataContext.CarteBancaires'  is null.");
            }
            await repositoryCarteBancaire.Add(carteBancaire);


            return CreatedAtAction("GetCarteBancaire", new { id = carteBancaire.CarteId }, carteBancaire);
        }

        // DELETE: api/CarteBancaires/5
        [HttpDelete("{id}")]
        [Authorize(Policy = Policies.human)]
        public async Task<IActionResult> DeleteCarteBancaire(int id)
        {
            if (repositoryCarteBancaire == null)
            {
                return NotFound();
            }
            var carteBancaire = await repositoryCarteBancaire.GetById(id);
            if (carteBancaire.Value == null)
            {
                return NotFound();
            }

            await repositoryCarteBancaire.Delete(carteBancaire.Value);

            return NoContent();
        }
    }
}
