using LeBonCoinAPI.Models.EntityFramework;
using LeBonCoinAPI.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LeBonCoinAPI.DataManager
{
    public class VilleManager : IRepositoryVille<Ville>
    {
        readonly DataContext? dataContext;
        public VilleManager() { }
        public VilleManager(DataContext context)
        {
            dataContext = context;
        }
        public async Task<ActionResult<IEnumerable<Ville>>> GetAll()
        {
            List<Ville> villes = await dataContext.Villes.ToListAsync();
            List<Departement> deps = (await new DepartementManager(dataContext).GetAll()).Value.ToList();
            foreach (Departement dep in deps)
            {
                dep.VillesDepartement = null;
            }
            return villes;
        }

        public async Task<ActionResult<Ville>> GetByInsee(string codeInsee)
        {
            Ville ville = await dataContext.Villes.FindAsync(codeInsee);
            ville.DepartementVille = (await new DepartementManager(dataContext).GetByCode(ville.DepartementCode)).Value;
            if(ville.DepartementVille != null)
            {
                ville.DepartementVille.VillesDepartement = null;
            }
            return ville;
        }
        public async Task<ActionResult<Ville>> GetByNom(string nom)
        {
            Ville ville = await dataContext.Villes.FirstOrDefaultAsync(v => v.Nom.ToUpper() == nom.ToUpper());
            if(ville != null)
            {
                ville.DepartementVille = (await new DepartementManager(dataContext).GetByCode(ville.DepartementCode)).Value;
                if (ville.DepartementVille != null)
                {
                    ville.DepartementVille.VillesDepartement = null;
                }
            }
            return ville;
        }
        public async Task Add(Ville entity)
        {
            await dataContext.Villes.AddAsync(entity);
            await dataContext.SaveChangesAsync();
        }
        public async Task Update(Ville ville, Ville entity)
        {
            dataContext.Entry(ville).State = EntityState.Modified;
            ville.DepartementCode = entity.DepartementCode;
            ville.Nom = entity.Nom;
            ville.CodePostal = entity.CodePostal; 
                        
            await dataContext.SaveChangesAsync();
        }
        public async Task Delete(Ville ville)
        {
            dataContext.Villes.Remove(ville);
            await dataContext.SaveChangesAsync();
        }
    }
}
