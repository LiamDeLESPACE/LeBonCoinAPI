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
        public async Task<ActionResult<IEnumerable<Commentaire>>> GetAll()
        {
            return await dataContext.Commentaires.ToListAsync();
        }

        public async Task<ActionResult<Commentaire>> GetByIds(int idReservation, int idProfil)
        {
            return await dataContext.Commentaires.FirstOrDefaultAsync(c => c.ReservationId == idReservation && c.ProfilId == idProfil);
        }

        public async Task<ActionResult<Commentaire>> GetByIdReservation(int idReservation)
        {
            return await dataContext.Commentaires.FirstOrDefaultAsync(u => u.ReservationId == idReservation);
        }

        public async Task<ActionResult<Commentaire>> GetByIdProfil(int idProfil)
        {
            return await dataContext.Commentaires.FirstOrDefaultAsync(u => u.ProfilId == idProfil);
        }
        public async Task Add(Commentaire entity)
        {
            await dataContext.Commentaires.AddAsync(entity);
            await dataContext.SaveChangesAsync();
        }
        public async Task Update(Commentaire commentaire, Commentaire entity)
        {
            dataContext.Entry(commentaire).State = EntityState.Modified;
            commentaire.Contenu = entity.Contenu;

            await dataContext.SaveChangesAsync();
        }
        public async Task Delete(Commentaire commentaire)
        {
            dataContext.Commentaires.Remove(commentaire);
            await dataContext.SaveChangesAsync();
        }
    }
}