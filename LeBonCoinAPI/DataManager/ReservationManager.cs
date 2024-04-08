using LeBonCoinAPI.Models.EntityFramework;
using LeBonCoinAPI.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LeBonCoinAPI.DataManager
{
    public class ReservationManager : IRepositoryReservation<Reservation>
    {
        readonly DataContext? dataContext;
        public ReservationManager() { }
        public ReservationManager(DataContext context)
        {
            dataContext = context;
        }
        public async Task<ActionResult<IEnumerable<Reservation>>> GetAll()
        {
            return await dataContext.Reservations.ToListAsync();
        }

        public async Task<ActionResult<Reservation>> GetById(int id)
        {
            return await dataContext.Reservations.FirstOrDefaultAsync(u => u.ReservationId == id);
        }
        public async Task Add(Reservation entity)
        {
            await dataContext.Reservations.AddAsync(entity);
            await dataContext.SaveChangesAsync();
        }
        public async Task Update(Reservation reservation, Reservation entity)
        {
            dataContext.Entry(reservation).State = EntityState.Modified;            
            reservation.AnnonceId = entity.AnnonceId;
            reservation.ProfilId = entity.ProfilId;
            reservation.DateArrivee = entity.DateArrivee;
            reservation.DateDepart = entity.DateDepart;
            reservation.NombreVoyageur = entity.NombreVoyageur;
            reservation.Nom = entity.Nom;
            reservation.Prenom = entity.Prenom;
            reservation.Telephone = entity.Telephone;            
            
            await dataContext.SaveChangesAsync();
        }
        public async Task Delete(Reservation reservation)
        {
            dataContext.Reservations.Remove(reservation);
            await dataContext.SaveChangesAsync();
        }
    }
}
