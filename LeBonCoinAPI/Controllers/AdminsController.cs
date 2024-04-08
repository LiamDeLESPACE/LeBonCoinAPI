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
    public class AdminsController : ControllerBase
    {

        private readonly IRepository<Admin> repositoryAdmin;

        public AdminsController(IRepository<Admin> repoAdmin)
        {
            repositoryAdmin = repoAdmin;
        }

        // GET: api/Admins
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Admin>>> GetAdmins()
        {
            var res = await repositoryAdmin.GetAll();
          if (res == null)
          {
              return NotFound();
          }
            return res;
        }

        // GET: api/Admins/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Admin>> GetAdmin(int id)
        {

            var admin = await repositoryAdmin.GetById(id);

            if (admin == null)
            {
                return NotFound();
            }

            return admin;
        }

        // PUT: api/Admins/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAdmin(int id, Admin admin)
        {
            if (id != admin.ProfilId)
            {
                return BadRequest();
            }

            var adminToUpdate = await repositoryAdmin.GetById(id);
            if (adminToUpdate == null)
            {
                return NotFound();
            }
            else
            {
                await repositoryAdmin.Update(adminToUpdate.Value, admin);
                return NoContent();
            }

        }

        // POST: api/Admins
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Admin>> PostAdmin(Admin admin)
        {
          if (await repositoryAdmin.GetAll() == null)
          {
              return Problem("Entity set 'DataContext.Admins'  is null.");
          }
            await repositoryAdmin.Add(admin);


            return CreatedAtAction("GetAdmin", new { id = admin.ProfilId }, admin);
        }

        // DELETE: api/Admins/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAdmin(int id)
        {
            if (await repositoryAdmin.GetAll() == null)
            {
                return NotFound();
            }
            var admin = await repositoryAdmin.GetById(id);
            if (admin == null)
            {
                return NotFound();
            }

            await repositoryAdmin.Delete(admin.Value);

            return NoContent();
        }

    }
}
