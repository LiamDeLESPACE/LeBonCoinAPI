using Microsoft.AspNetCore.Mvc;

namespace LeBonCoinAPI.Models.Repository
{
    public interface IRepositoryCommentaire<TEntity>
    {
        Task<ActionResult<IEnumerable<TEntity>>> GetAll();
        Task<ActionResult<TEntity>> GetByIds(int idReservation, int idProfil);
        Task<ActionResult<TEntity>> GetByIdProfil(int idProfil);
        Task<ActionResult<TEntity>> GetByIdReservation(int idReservation);
        Task Add(TEntity entity);
        Task Update(TEntity entityToUpdate, TEntity entity);
        Task Delete(TEntity entity);
    }
}
