using Microsoft.AspNetCore.Mvc;

namespace LeBonCoinAPI.Models.Repository
{
    public interface IRepositoryVille<Tentity>
    {
        ActionResult<IEnumerable<Tentity>> GetAll();
        ActionResult<Tentity> GetByString(string id);
        void Add(Tentity entity);
        void Update(Tentity entityToUpdate, Tentity entity);
        void Delete(Tentity entity);
    }
}
