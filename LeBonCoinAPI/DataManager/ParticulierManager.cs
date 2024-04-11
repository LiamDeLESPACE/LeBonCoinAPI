using LeBonCoinAPI.Models.EntityFramework;
using LeBonCoinAPI.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace LeBonCoinAPI.DataManager
{
    public class ParticulierManager : IRepositoryParticulier<Particulier>
    {
        readonly DataContext? dataContext;
        public ParticulierManager() { }
        public ParticulierManager(DataContext context)
        {
            dataContext = context;
        }
        public async Task<ActionResult<IEnumerable<Particulier>>> GetAll()
        {
            return await dataContext.Particuliers.ToListAsync();
        }

        public async Task<ActionResult<Particulier>> GetById(int id)
        {
            return await dataContext.Particuliers.FindAsync(id);
        }
        public async Task Add(Particulier entity)
        {
            StringBuilder sb = new StringBuilder();
            byte[] hashValue = SHA512.HashData(Encoding.UTF8.GetBytes(entity.HashMotDePasse));
            foreach (byte b in hashValue)
            {
                sb.Append($"{b:X2}");
            }
            entity.HashMotDePasse = sb.ToString().ToUpper();

            dataContext.Particuliers.Add(entity);
            await dataContext.SaveChangesAsync();
        }
        public async Task Update(Particulier particulier, Particulier entity)
        {
            dataContext.Entry(particulier).State = EntityState.Modified;

            StringBuilder sb = new StringBuilder();
            byte[] hashValue = SHA512.HashData(Encoding.UTF8.GetBytes(entity.HashMotDePasse));
            foreach (byte b in hashValue)
            {
                sb.Append($"{b:X2}");
            }
            particulier.HashMotDePasse = sb.ToString().ToUpper();

            particulier.Telephone = entity.Telephone;
            particulier.Email = entity.Email;
            particulier.Civilite = entity.Civilite;
            particulier.Nom = entity.Nom;
            particulier.Prenom = entity.Prenom;
            particulier.DateNaissance = entity.DateNaissance;
            particulier.AdresseId = entity.AdresseId;

            await dataContext.SaveChangesAsync();
        }
        public async Task Delete(Particulier particulier)
        {
            dataContext.Particuliers.Remove(particulier);
            await dataContext.SaveChangesAsync();
        }

        public async Task<ActionResult<IEnumerable<Reservation>>> GetReservationFromParticulier(int id)
        {
            var res = await (from c in dataContext.Reservations where c.ProfilId == id select c).ToListAsync();
            foreach (var reservation in res)
            {
                reservation.AnnonceReservation = (await new AnnonceManager(dataContext).GetById(reservation.AnnonceId)).Value;
                if (reservation.AnnonceReservation != null)
                    reservation.AnnonceReservation.ReservationsAnnonce = null;
            }
            return res;
        }
    }
}
