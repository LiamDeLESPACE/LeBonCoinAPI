using LeBonCoinAPI.Models.EntityFramework;
using LeBonCoinAPI.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LeBonCoinAPI.DataManager
{
    public class ProfilManager : IRepositoryProfil<Profil>
    {
        readonly DataContext? dataContext;
        public ProfilManager() { }
        public ProfilManager(DataContext context)
        {
            dataContext = context;
        }
        public ActionResult<IEnumerable<Profil>> GetAll()
        {
            return dataContext.Profils.ToList();
        }

        public ActionResult<Profil> GetById(int id)
        {
            return dataContext.Profils.FirstOrDefault(u => u.ProfilId == id);
        }
        public void Add(Profil entity)
        {
            dataContext.Profils.Add(entity);
            dataContext.SaveChanges();
        }
        public void Update(Profil profil, Profil entity)
        {
            dataContext.Entry(profil).State = EntityState.Modified;
            profil.ProfilId = entity.ProfilId;
            profil.HashMotDePasse = entity.HashMotDePasse;
            profil.Telephone = entity.Telephone;
            profil.CartesBancairesProfil = entity.CartesBancairesProfil;
            profil.PhotosProfil = entity.PhotosProfil;
            profil.ReservationsProfil = entity.ReservationsProfil;
            profil.SignalementsProfil = entity.SignalementsProfil;
            profil.AnnoncesProfil = entity.AnnoncesProfil;
            profil.FavorisProfil = entity.FavorisProfil;
            profil.CommentairesProfil = entity.CommentairesProfil;

            dataContext.SaveChanges();
        }
        public void Delete(Profil profil)
        {
            dataContext.Profils.Remove(profil);
            dataContext.SaveChanges();
        }
    }
}
