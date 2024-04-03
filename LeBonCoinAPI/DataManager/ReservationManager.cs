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
        public ActionResult<IEnumerable<Reservation>> GetAll()
        {
            return dataContext.Reservations.ToList();
        }

        public ActionResult<Reservation> GetById(int id)
        {
            return dataContext.Reservations.FirstOrDefault(u => u.ReservationId == id);
        }
        public void Add(Reservation entity)
        {
            dataContext.Reservations.Add(entity);
            dataContext.SaveChanges();
        }
        public void Update(Reservation reservation, Reservation entity)
        {
            dataContext.Entry(reservation).State = EntityState.Modified;
            reservation.ReservationId = entity.ReservationId;
            reservation.AnnonceId = entity.AnnonceId;
            reservation.ProfilId = entity.ProfilId;
            reservation.DateArrivee = entity.DateArrivee;
            reservation.DateDepart = entity.DateDepart;
            reservation.NombreVoyageur = entity.NombreVoyageur;
            reservation.Nom = entity.Nom;
            reservation.Prenom = entity.Prenom;
            reservation.Telephone = entity.Telephone;
            reservation.ReglementsReservation = entity.ReglementsReservation;
            reservation.CommentairesReservation = entity.CommentairesReservation;
            
            dataContext.SaveChanges();
        }
        public void Delete(Reservation reservation)
        {
            dataContext.Reservations.Remove(reservation);
            dataContext.SaveChanges();
        }
    }
}
