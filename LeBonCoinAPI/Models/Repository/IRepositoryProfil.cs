using Microsoft.AspNetCore.Mvc;

namespace LeBonCoinAPI.Models.Repository
{
    public interface IRepositoryProfil<Tentity>
    {
        ActionResult<IEnumerable<Tentity>> GetAll();
        ActionResult<Tentity> GetById(int id);
        void Add(Tentity entity);
        void Update(Tentity entityToUpdate, Tentity entity);
        void Delete(Tentity entity);
    }
}
