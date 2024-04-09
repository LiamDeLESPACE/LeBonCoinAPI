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
    public class ReservationsController : ControllerBase
    {
        private readonly IRepository<Reservation> repositoryReservation;

        public ReservationsController(IRepository<Reservation> repoReservation)
        {
            repositoryReservation = repoReservation;
        }

        // GET: api/Reservations
        [HttpGet]
        [Authorize(Policy = Policies.admin)]
        public async Task<ActionResult<IEnumerable<Reservation>>> GetReservations()
        {
            var res = await repositoryReservation.GetAll();
            if (res == null)
            {
                return NotFound();
            }
            return res;
        }

        // GET: api/Reservations/5
        [HttpGet("{id}")]
        [Authorize(Policy = Policies.all)]
        public async Task<ActionResult<Reservation>> GetReservation(int id)
        {

            var reservation = await repositoryReservation.GetById(id);

            if (reservation == null)
            {
                return NotFound();
            }

            return reservation;
        }

        // PUT: api/Reservations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Policy = Policies.all)]
        public async Task<IActionResult> PutReservation(int id, Reservation reservation)
        {
            if (id != reservation.ReservationId)
            {
                return BadRequest();
            }

            var reservationToUpdate = await repositoryReservation.GetById(id);
            if (reservationToUpdate.Value == null)
            {
                return NotFound();
            }
            else
            {
                await repositoryReservation.Update(reservationToUpdate.Value, reservation);
                return NoContent();
            }

        }

        // POST: api/Reservations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Policy = Policies.all)]
        public async Task<ActionResult<Reservation>> PostReservation(Reservation reservation)
        {
            if (await repositoryReservation.GetAll() == null)
            {
                return Problem("Entity set 'DataContext.Reservations'  is null.");
            }
            await repositoryReservation.Add(reservation);


            return CreatedAtAction("GetReservation", new { id = reservation.ReservationId }, reservation);
        }

        // DELETE: api/Reservations/5
        [HttpDelete("{id}")]
        [Authorize(Policy = Policies.admin)]
        public async Task<IActionResult> DeleteReservation(int id)
        {
            if (repositoryReservation == null)
            {
                return NotFound();
            }
            var reservation = await repositoryReservation.GetById(id);
            if (reservation.Value == null)
            {
                return NotFound();
            }

            await repositoryReservation.Delete(reservation.Value);

            return NoContent();
        }
    }
}
