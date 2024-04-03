using LeBonCoinAPI.Models.EntityFramework;
using LeBonCoinAPI.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LeBonCoinAPI.DataManager
{
    public class CommentaireManager : IRepositoryCommentaire<Commentaire>
    {
        readonly DataContext? dataContext;
        public CommentaireManager() { }
        public CommentaireManager(DataContext context)
        {
            dataContext = context;
        }
        public ActionResult<IEnumerable<Commentaire>> GetAll()
        {
            return dataContext.Commentaires.ToList();
        }

        public ActionResult<Commentaire> GetByIds(int idReservation, int idProfil)
        {
            return dataContext.Commentaires.FirstOrDefault(c => c.ReservationId == idReservation && c.ProfilId == idProfil);
        }

        public ActionResult<Commentaire> GetByIdReservation(int idReservation)
        {
            return dataContext.Commentaires.FirstOrDefault(u => u.ReservationId == idReservation);
        }

        public ActionResult<Commentaire> GetByIdProfil(int idProfil)
        {
            return dataContext.Commentaires.FirstOrDefault(u => u.ProfilId == idProfil);
        }
        public void Add(Commentaire entity)
        {
            dataContext.Commentaires.Add(entity);
            dataContext.SaveChanges();
        }
        public void Update(Commentaire commentaire, Commentaire entity)
        {
            dataContext.Entry(commentaire).State = EntityState.Modified;
            commentaire.ProfilId = entity.ProfilId;
            commentaire.ReservationId = entity.ReservationId;
            commentaire.Contenu = entity.Contenu;

            dataContext.SaveChanges();
        }
        public void Delete(Commentaire commentaire)
        {
            dataContext.Commentaires.Remove(commentaire);
            dataContext.SaveChanges();
        }
    }
}
