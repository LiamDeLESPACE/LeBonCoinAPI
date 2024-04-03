using Microsoft.AspNetCore.Mvc;

namespace LeBonCoinAPI.Models.Repository
{
    public interface IRepositoryPossedeEquipement<TEntity>
    {
        ActionResult<IEnumerable<TEntity>> GetAll();
        ActionResult<TEntity> GetByIds(int idAnnonce, int idEquipement);
        ActionResult<TEntity> GetByIdAnnonce(int idAnnonce);
        ActionResult<TEntity> GetByIdEquipement(int idEquipement);
        void Add(TEntity entity);
        void Update(TEntity entityToUpdate, TEntity entity);
        void Delete(TEntity entity);
    }
}
