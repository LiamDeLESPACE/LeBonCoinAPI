using LeBonCoinAPI.Models.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace LeBonCoinAPI.Models.Repository
{
    public interface IRepositoryParticulier<TEntity>
    {
        Task<ActionResult<IEnumerable<TEntity>>> GetAll();
        Task<ActionResult<TEntity>> GetById(int id);
        Task Add(TEntity entity);
        Task Update(TEntity entityToUpdate, TEntity entity);
        Task Delete(TEntity entity);
        Task<ActionResult<IEnumerable<Reservation>>> GetReservationFromParticulier(int id);
    }
}
