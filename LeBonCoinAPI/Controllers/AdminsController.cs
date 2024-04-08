﻿using System;
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

        private readonly IRepositoryAdmin<Admin> repositoryAdmin;

        public AdminsController(IRepositoryAdmin<Admin> repoAdmin)
        {
            repositoryAdmin = repoAdmin;
        }

        // GET: api/Admins
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Admin>>> GetAdmins()
        {
          if ( repositoryAdmin.GetAll() == null)
          {
              return NotFound();
          }
            return repositoryAdmin.GetAll();
        }

        // GET: api/Admins/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Admin>> GetAdmin(int id)
        {
          if (repositoryAdmin.GetAll() == null)
          {
              return NotFound();
          }
            var admin = repositoryAdmin.GetById(id);

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

            var adminToUpdate = repositoryAdmin.GetById(id);
            if (adminToUpdate == null)
            {
                return NotFound();
            }
            else
            {
                repositoryAdmin.Update(adminToUpdate.Value, admin);
                return NoContent();
            }

            //_context.Entry(admin).State = EntityState.Modified;

            /*try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdminExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();*/
        }

        // POST: api/Admins
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Admin>> PostAdmin(Admin admin)
        {
          if (repositoryAdmin.GetAll() == null)
          {
              return Problem("Entity set 'DataContext.Admins'  is null.");
          }
            repositoryAdmin.Add(admin);
            /*try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (AdminExists(admin.ProfilId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }*/

            return CreatedAtAction("GetAdmin", new { id = admin.ProfilId }, admin);
        }

        // DELETE: api/Admins/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAdmin(int id)
        {
            if (repositoryAdmin.GetAll() == null)
            {
                return NotFound();
            }
            var admin = repositoryAdmin.GetById(id);
            if (admin == null)
            {
                return NotFound();
            }

            repositoryAdmin.Delete(admin.Value);

            return NoContent();
        }

        /*private bool AdminExists(int id)
        {
            return (_context.Admins?.Any(e => e.ProfilId == id)).GetValueOrDefault();
        }*/
    }
}
