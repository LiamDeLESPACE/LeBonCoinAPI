using Microsoft.AspNetCore.Mvc;

namespace LeBonCoinAPI.Models.Repository
{
    public interface IRepositoryCommentaire<TEntity>
    {
        ActionResult<IEnumerable<TEntity>> GetAll();
        ActionResult<TEntity> GetByIds(int idReservation, int idProfil);
        ActionResult<TEntity> GetByIdProfil(int idProfil);
        ActionResult<TEntity> GetByIdReservation(int idReservation);
        void Add(TEntity entity);
        void Update(TEntity entityToUpdate, TEntity entity);
        void Delete(TEntity entity);
    }
}
